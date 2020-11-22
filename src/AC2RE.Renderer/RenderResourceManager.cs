using AC2RE.Definitions;
using AC2RE.RenderLib;
using System;
using System.Collections.Generic;

namespace AC2RE.Renderer {

    public class RenderResourceManager : IDisposable {

        private static readonly Dictionary<PixelFormat, TextureFormat> PIXEL_TO_TEXTURE_FORMAT = new() {
            { PixelFormat.CUSTOM_R8G8B8A8, TextureFormat.R8G8B8A8 },
            { PixelFormat.DXT1, TextureFormat.DXT1 },
            { PixelFormat.DXT3, TextureFormat.DXT3 },
            { PixelFormat.DXT5, TextureFormat.DXT5 },
        };

        public readonly UberShaderManager uberShaderManager = new();

        private readonly DatReader datReader;
        private readonly Dictionary<DataId, RenderMesh> meshDidToMesh = new();
        private readonly Dictionary<DataId, List<RenderMesh>?> didToMeshes = new();
        private readonly Dictionary<DataId, ITexture> surfaceDidToTexture = new();

        public void Dispose() {
            foreach (RenderMesh mesh in meshDidToMesh.Values) {
                mesh.mesh.Dispose();
            }
            uberShaderManager.Dispose();
        }

        public RenderResourceManager(DatReader datReader) {
            this.datReader = datReader;
        }

        public List<RenderMesh>? loadDatMeshes(IRenderer renderer, DataId did) {
            if (!didToMeshes.TryGetValue(did, out List<RenderMesh>? meshes)) {
                if (!datReader.contains(did)) {
                    return null;
                }

                DbType dbType = DbTypeDef.getType(did);
                using (AC2Reader data = datReader.getFileReader(did)) {
                    if (dbType == DbType.MESH) {
                        meshes = new() { loadMesh(renderer, data) };
                    } else if (dbType == DbType.SETUP) {
                        meshes = loadSetup(renderer, data);
                    } else if (dbType == DbType.VISUAL_DESC) {
                        meshes = loadVisualDesc(renderer, data);
                    }
                }
                didToMeshes[did] = meshes;
            }

            return meshes;
        }

        private List<RenderMesh> loadMeshes(IRenderer renderer, List<DataId> meshDids) {
            List<RenderMesh> meshes = new();
            foreach (DataId meshDid in meshDids) {
                using (AC2Reader data = datReader.getFileReader(meshDid)) {
                    meshes.Add(loadMesh(renderer, data));
                }
            }

            return meshes;
        }

        private RenderMesh loadMesh(IRenderer renderer, AC2Reader data) {
            DataId did = data.ReadDataId();
            if (!meshDidToMesh.TryGetValue(did, out RenderMesh? mesh)) {
                CStaticMesh meshData = new(data);
                mesh = loadMesh(renderer, meshData);
                meshDidToMesh[did] = mesh;
            }

            return mesh;
        }

        private ITexture loadSurface(IRenderer renderer, DataId did) {
            if (!surfaceDidToTexture.TryGetValue(did, out ITexture? texture)) {
                using (AC2Reader data = datReader.getFileReader(did)) {
                    RenderSurface surface = new(data);
                    texture = renderer.loadTexture(surface.sourceData, surface.width, surface.height, PIXEL_TO_TEXTURE_FORMAT[surface.pixelFormat]);
                }
                surfaceDidToTexture[did] = texture;
            }

            return texture;
        }

        private List<ITexture> loadMaterial(IRenderer renderer, DataId did) {
            List<ITexture> textures = new();
            using (AC2Reader data = datReader.getFileReader(did)) {
                MaterialInstance materialInstance = new(data);
                // TODO: May need to take materialInstance.materialDid into account
                using (AC2Reader dataModifier = datReader.getFileReader(materialInstance.modifierDids[0])) {
                    MaterialModifier modifier = new(dataModifier);
                    foreach (MaterialProperty property in modifier.properties) {
                        if (property.dataType == RMDataType.TEXTURE) {
                            using (AC2Reader dataTexture = datReader.getFileReader(property.valTextureDid)) {
                                RenderTexture tex = new(dataTexture);
                                // TODO: These textures likely need to be ordered by nameId, not just serialization order
                                // TODO: Level [0] for SOME textures needs to be loaded from highres.dat, not portal.dat
                                textures.Add(loadSurface(renderer, tex.levelSurfaceDids.Count > 1 ? tex.levelSurfaceDids[1] : tex.levelSurfaceDids[0]));
                            }
                        }
                    }
                }
            }

            return textures;
        }

        private RenderMesh loadMesh(IRenderer renderer, CBaseMesh mesh) {

            List<VertexAttribute> vertexAttributes = new();
            if (mesh.vertexArray.vertexFormat.hasOrigin) {
                vertexAttributes.Add(new(UberShaderManager.POS_ATTRIB_ID, 3, typeof(float), mesh.vertexArray.vertexFormat.offsetOrigin));
            }
            if (mesh.vertexArray.vertexFormat.offsetNormal != 0) {
                vertexAttributes.Add(new(UberShaderManager.NORMAL_ATTRIB_ID, 3, typeof(float), mesh.vertexArray.vertexFormat.offsetNormal));
            }
            if (mesh.vertexArray.vertexFormat.offsetDiffuseColor != 0) {
                vertexAttributes.Add(new(UberShaderManager.DIFFUSE_COLOR_ATTRIB_ID, 4, typeof(byte), mesh.vertexArray.vertexFormat.offsetDiffuseColor, true));
            }
            if (mesh.vertexArray.vertexFormat.offsetSpecularColor != 0) {
                vertexAttributes.Add(new(UberShaderManager.SPECULAR_COLOR_ATTRIB_ID, 4, typeof(byte), mesh.vertexArray.vertexFormat.offsetSpecularColor, true));
            }
            if (mesh.vertexArray.vertexFormat.offsetVectorS != 0) {
                vertexAttributes.Add(new(UberShaderManager.TANGENT_ATTRIB_ID, 3, typeof(float), mesh.vertexArray.vertexFormat.offsetVectorS));
            }
            if (mesh.vertexArray.vertexFormat.offsetVectorT != 0) {
                vertexAttributes.Add(new(UberShaderManager.BITANGENT_ATTRIB_ID, 3, typeof(float), mesh.vertexArray.vertexFormat.offsetVectorT));
            }
            for (uint i = 0; i < mesh.vertexArray.vertexFormat.numTexCoordPairs; i++) {
                vertexAttributes.Add(new(UberShaderManager.TEX_COORD_ATTRIB_ID_START + i, 2, typeof(float), mesh.vertexArray.vertexFormat.offsetTexCoordPairs[i]));
            }
            for (uint i = 0; i < mesh.vertexArray.vertexFormat.numMatrices; i++) {
                vertexAttributes.Add(new(UberShaderManager.MATRIX_INDICES_ATTRIB_ID_START + i, 1, typeof(byte), mesh.vertexArray.vertexFormat.offsetMatrices + i));
                vertexAttributes.Add(new(UberShaderManager.MATRIX_WEIGHTS_ATTRIB_ID_START + i, 1, typeof(float), mesh.vertexArray.vertexFormat.offsetMatrixWeights + i * 4));
            }

            ushort[] indices = mesh.geometry.indices.ToArray();
            byte[] indexData = new byte[indices.Length * sizeof(ushort)];
            Buffer.BlockCopy(indices, 0, indexData, 0, indexData.Length);

            List<ITexture> textures = new();
            foreach (DataId materialInstanceDid in mesh.materialInstanceDids) {
                textures.AddRange(loadMaterial(renderer, materialInstanceDid));
            }

            IMesh loadedMesh = renderer.loadMesh(mesh.vertexArray.vertexData, vertexAttributes, mesh.vertexArray.vertexFormat.vertexSize, indexData, typeof(ushort));

            return new(loadedMesh, mesh.vertexArray.vertexFormat, textures);
        }

        private List<RenderMesh> loadSetups(IRenderer renderer, List<DataId> setupDids) {
            List<RenderMesh> meshes = new();
            foreach (DataId setupDid in setupDids) {
                using (AC2Reader data = datReader.getFileReader(setupDid)) {
                    meshes.AddRange(loadSetup(renderer, data));
                }
            }

            return meshes;
        }

        private List<RenderMesh> loadSetup(IRenderer renderer, AC2Reader data) {
            CSetup setup = new(data);

            return loadMeshes(renderer, setup.meshes);
        }

        private List<RenderMesh> loadVisualDesc(IRenderer renderer, AC2Reader data) {
            VisualDesc visualDesc = new(data);

            List<DataId> setupDids = new();
            if (visualDesc.pgdDescTable != null) {
                foreach (PartGroupDataDesc pgdDesc in visualDesc.pgdDescTable.Values) {
                    setupDids.Add(pgdDesc.setupDid);
                }
            }

            return loadSetups(renderer, setupDids);
        }
    }
}
