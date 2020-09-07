using AC2E.Def;
using AC2E.RenderCommon;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Render {

    public class RenderResourceManager : IDisposable {

        private static readonly Dictionary<PixelFormat, TextureFormat> PIXEL_TO_TEXTURE_FORMAT = new Dictionary<PixelFormat, TextureFormat> {
            { PixelFormat.CUSTOM_R8G8B8A8, TextureFormat.R8G8B8A8 },
            { PixelFormat.DXT1, TextureFormat.DXT1 },
            { PixelFormat.DXT3, TextureFormat.DXT3 },
            { PixelFormat.DXT5, TextureFormat.DXT5 },
        };

        public readonly UberShaderManager uberShaderManager = new UberShaderManager();

        private readonly Dictionary<DataId, RenderMesh> meshDidToMesh = new Dictionary<DataId, RenderMesh>();
        private readonly Dictionary<DataId, List<RenderMesh>> didToMeshes = new Dictionary<DataId, List<RenderMesh>>();
        private readonly Dictionary<DataId, ITexture> surfaceDidToTexture = new Dictionary<DataId, ITexture>();

        public void Dispose() {
            foreach (RenderMesh mesh in meshDidToMesh.Values) {
                mesh.mesh.Dispose();
            }
            uberShaderManager.Dispose();
        }

        public List<RenderMesh> loadDatMeshes(IRenderer renderer, string datFileName, DataId did) {
            if (!didToMeshes.TryGetValue(did, out List<RenderMesh> meshes)) {
                if (File.Exists(datFileName)) {
                    using (DatReader datReader = new DatReader(new AC2Reader(File.OpenRead(datFileName)))) {
                        if (!datReader.contains(did)) {
                            return null;
                        }

                        DbType dbType = DbTypeDef.getType(did);
                        using (AC2Reader data = datReader.getFileReader(did)) {
                            if (dbType == DbType.MESH) {
                                meshes = new List<RenderMesh> { loadMesh(renderer, datReader, data) };
                            } else if (dbType == DbType.SETUP) {
                                meshes = loadSetup(renderer, datReader, data);
                            } else if (dbType == DbType.VISUAL_DESC) {
                                meshes = loadVisualDesc(renderer, datReader, data);
                            }
                        }
                    }
                }
                didToMeshes[did] = meshes;
            }

            return meshes;
        }

        private List<RenderMesh> loadMeshes(IRenderer renderer, DatReader datReader, List<DataId> meshDids) {
            List<RenderMesh> meshes = new List<RenderMesh>();
            foreach (DataId meshDid in meshDids) {
                using (AC2Reader data = datReader.getFileReader(meshDid)) {
                    meshes.Add(loadMesh(renderer, datReader, data));
                }
            }

            return meshes;
        }

        private RenderMesh loadMesh(IRenderer renderer, DatReader datReader, AC2Reader data) {
            DataId did = data.ReadDataId();
            if (!meshDidToMesh.TryGetValue(did, out RenderMesh mesh)) {
                CStaticMesh meshData = new CStaticMesh(data);
                mesh = loadMesh(renderer, datReader, meshData);
                meshDidToMesh[did] = mesh;
            }

            return mesh;
        }

        private ITexture loadSurface(IRenderer renderer, DatReader datReader, DataId did) {
            if (!surfaceDidToTexture.TryGetValue(did, out ITexture texture)) {
                using (AC2Reader data = datReader.getFileReader(did)) {
                    RenderSurface surface = new RenderSurface(data);
                    texture = renderer.loadTexture(surface.sourceData, surface.width, surface.height, PIXEL_TO_TEXTURE_FORMAT[surface.pixelFormat]);
                }
                surfaceDidToTexture[did] = texture;
            }

            return texture;
        }

        private List<ITexture> loadMaterial(IRenderer renderer, DatReader datReader, DataId did) {
            List<ITexture> textures = new List<ITexture>();
            using (AC2Reader data = datReader.getFileReader(did)) {
                MaterialInstance materialInstance = new MaterialInstance(data);
                // TODO: May need to take materialInstance.materialDid into account
                using (AC2Reader dataModifier = datReader.getFileReader(materialInstance.modifierDids[0])) {
                    MaterialModifier modifier = new MaterialModifier(dataModifier);
                    foreach (MaterialProperty property in modifier.properties) {
                        if (property.dataType == RMDataType.TEXTURE) {
                            using (AC2Reader dataTexture = datReader.getFileReader(property.valTextureDid)) {
                                RenderTexture tex = new RenderTexture(dataTexture);
                                // TODO: These textures likely need to be ordered by nameId, not just serialization order
                                // TODO: Level [0] for SOME textures needs to be loaded from highres.dat, not portal.dat
                                textures.Add(loadSurface(renderer, datReader, tex.levelSurfaceDids.Count > 1 ? tex.levelSurfaceDids[1] : tex.levelSurfaceDids[0]));
                            }
                        }
                    }
                }
            }

            return textures;
        }

        private RenderMesh loadMesh(IRenderer renderer, DatReader datReader, CBaseMesh mesh) {

            List<VertexAttribute> vertexAttributes = new List<VertexAttribute>();
            if (mesh.vertexArray.vertexFormat.hasOrigin) {
                vertexAttributes.Add(new VertexAttribute { id = UberShaderManager.POS_ATTRIB_ID, numComponents = 3, componentType = typeof(float), offset = mesh.vertexArray.vertexFormat.offsetOrigin });
            }
            if (mesh.vertexArray.vertexFormat.offsetNormal != 0) {
                vertexAttributes.Add(new VertexAttribute { id = UberShaderManager.NORMAL_ATTRIB_ID, numComponents = 3, componentType = typeof(float), offset = mesh.vertexArray.vertexFormat.offsetNormal });
            }
            if (mesh.vertexArray.vertexFormat.offsetDiffuseColor != 0) {
                vertexAttributes.Add(new VertexAttribute { id = UberShaderManager.DIFFUSE_COLOR_ATTRIB_ID, numComponents = 4, componentType = typeof(byte), normalize = true, offset = mesh.vertexArray.vertexFormat.offsetDiffuseColor });;
            }
            if (mesh.vertexArray.vertexFormat.offsetSpecularColor != 0) {
                vertexAttributes.Add(new VertexAttribute { id = UberShaderManager.SPECULAR_COLOR_ATTRIB_ID, numComponents = 4, componentType = typeof(byte), normalize = true, offset = mesh.vertexArray.vertexFormat.offsetSpecularColor });
            }
            if (mesh.vertexArray.vertexFormat.offsetVectorS != 0) {
                vertexAttributes.Add(new VertexAttribute { id = UberShaderManager.TANGENT_ATTRIB_ID, numComponents = 3, componentType = typeof(float), offset = mesh.vertexArray.vertexFormat.offsetVectorS });
            }
            if (mesh.vertexArray.vertexFormat.offsetVectorT != 0) {
                vertexAttributes.Add(new VertexAttribute { id = UberShaderManager.BITANGENT_ATTRIB_ID, numComponents = 3, componentType = typeof(float), offset = mesh.vertexArray.vertexFormat.offsetVectorT });
            }
            for (uint i = 0; i < mesh.vertexArray.vertexFormat.numTexCoordPairs; i++) {
                vertexAttributes.Add(new VertexAttribute { id = UberShaderManager.TEX_COORD_ATTRIB_ID_START + i, numComponents = 2, componentType = typeof(float), offset = mesh.vertexArray.vertexFormat.offsetTexCoordPairs[i] });
            }
            for (uint i = 0; i < mesh.vertexArray.vertexFormat.numMatrices; i++) {
                vertexAttributes.Add(new VertexAttribute { id = UberShaderManager.MATRIX_INDICES_ATTRIB_ID_START + i, numComponents = 1, componentType = typeof(byte), offset = mesh.vertexArray.vertexFormat.offsetMatrices + i });
                vertexAttributes.Add(new VertexAttribute { id = UberShaderManager.MATRIX_WEIGHTS_ATTRIB_ID_START + i, numComponents = 1, componentType = typeof(float), offset = mesh.vertexArray.vertexFormat.offsetMatrixWeights + i * 4 });
            }

            ushort[] indices = mesh.geometry.indices.ToArray();
            byte[] indexData = new byte[indices.Length * sizeof(ushort)];
            Buffer.BlockCopy(indices, 0, indexData, 0, indexData.Length);

            List<ITexture> textures = new List<ITexture>();
            foreach (DataId materialInstanceDid in mesh.materialInstanceDids) {
                textures.AddRange(loadMaterial(renderer, datReader, materialInstanceDid));
            }

            return new RenderMesh {
                mesh = renderer.loadMesh(mesh.vertexArray.vertexData, vertexAttributes, mesh.vertexArray.vertexFormat.vertexSize, indexData, typeof(ushort)),
                vertexFormat = mesh.vertexArray.vertexFormat,
                textures = textures,
            };
        }

        private List<RenderMesh> loadSetups(IRenderer renderer, DatReader datReader, List<DataId> setupDids) {
            List<RenderMesh> meshes = new List<RenderMesh>();
            foreach (DataId setupDid in setupDids) {
                using (AC2Reader data = datReader.getFileReader(setupDid)) {
                    meshes.AddRange(loadSetup(renderer, datReader, data));
                }
            }

            return meshes;
        }

        private List<RenderMesh> loadSetup(IRenderer renderer, DatReader datReader, AC2Reader data) {
            CSetup setup = new CSetup(data);

            return loadMeshes(renderer, datReader, setup.meshes);
        }

        private List<RenderMesh> loadVisualDesc(IRenderer renderer, DatReader datReader, AC2Reader data) {
            VisualDesc visualDesc = new VisualDesc(data);

            List<DataId> setupDids = new List<DataId>();
            if (visualDesc.pgdDescTable != null) {
                foreach (PartGroupDataDesc pgdDesc in visualDesc.pgdDescTable.Values) {
                    setupDids.Add(pgdDesc.setupDid);
                }
            }

            return loadSetups(renderer, datReader, setupDids);
        }
    }
}
