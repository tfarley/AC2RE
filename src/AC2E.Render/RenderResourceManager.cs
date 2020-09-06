using AC2E.Def;
using AC2E.RenderCommon;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Render {

    public class RenderResourceManager : IDisposable {

        public static readonly uint POS_ATTRIB_ID = 0;

        private readonly Dictionary<DataId, IMesh> meshDidToMesh = new Dictionary<DataId, IMesh>();
        private readonly Dictionary<DataId, List<IMesh>> didToMeshes = new Dictionary<DataId, List<IMesh>>();

        public void Dispose() {
            foreach (IMesh mesh in meshDidToMesh.Values) {
                mesh.Dispose();
            }
        }

        public List<IMesh> loadDatMeshes(IRenderer renderer, string datFileName, DataId did) {
            if (!didToMeshes.TryGetValue(did, out List<IMesh> meshes)) {
                if (File.Exists(datFileName)) {
                    using (DatReader datReader = new DatReader(new AC2Reader(File.OpenRead(datFileName)))) {
                        DbType dbType = DbTypeDef.getType(did);
                        using (AC2Reader data = datReader.getFileReader(did)) {
                            if (dbType == DbType.MESH) {
                                meshes = new List<IMesh> { loadMesh(renderer, data) };
                            } else if (dbType == DbType.SETUP) {
                                meshes = loadSetup(renderer, datReader, data);
                            } else if (dbType == DbType.VISUAL_DESC) {
                                meshes = loadVisualDesc(renderer, datReader, data);
                            }
                        }
                    }
                }
            }

            return meshes;
        }

        private List<IMesh> loadMeshes(IRenderer renderer, DatReader datReader, List<DataId> meshDids) {
            List<IMesh> meshes = new List<IMesh>();
            foreach (DataId meshDid in meshDids) {
                using (AC2Reader data = datReader.getFileReader(meshDid)) {
                    meshes.Add(loadMesh(renderer, data));
                }
            }

            return meshes;
        }

        private IMesh loadMesh(IRenderer renderer, AC2Reader data) {
            DataId did = data.ReadDataId();
            if (!meshDidToMesh.TryGetValue(did, out IMesh mesh)) {
                CStaticMesh meshData = new CStaticMesh(data);
                mesh = loadMesh(renderer, meshData);
                meshDidToMesh[did] = mesh;
            }

            return mesh;
        }

        private IMesh loadMesh(IRenderer renderer, CBaseMesh mesh) {
            List<VertexAttribute> vertexAttributes = new List<VertexAttribute>();
            if (mesh.vertexArray.vertexFormat.hasOrigin) {
                vertexAttributes.Add(new VertexAttribute { id = POS_ATTRIB_ID, numComponents = 3, componentType = typeof(float), offset = 0 });
            }

            /*ushort[] indices = new ushort[mesh.vertexArray.vertexData.Length / mesh.vertexArray.vertexFormat.vertexSize];
            for (ushort i = 0; i < indices.Length; i++) {
                indices[i] = i;
            }*/

            ushort[] indices = mesh.geometry.indices.ToArray();

            byte[] elementData = new byte[indices.Length * sizeof(ushort)];
            Buffer.BlockCopy(indices, 0, elementData, 0, elementData.Length);

            return renderer.loadMesh(mesh.vertexArray.vertexData, vertexAttributes, mesh.vertexArray.vertexFormat.vertexSize, elementData, typeof(ushort));
        }

        private List<IMesh> loadSetups(IRenderer renderer, DatReader datReader, List<DataId> setupDids) {
            List<IMesh> meshes = new List<IMesh>();
            foreach (DataId setupDid in setupDids) {
                using (AC2Reader data = datReader.getFileReader(setupDid)) {
                    meshes.AddRange(loadSetup(renderer, datReader, data));
                }
            }

            return meshes;
        }

        private List<IMesh> loadSetup(IRenderer renderer, DatReader datReader, AC2Reader data) {
            CSetup setup = new CSetup(data);

            return loadMeshes(renderer, datReader, setup.meshes);
        }

        private List<IMesh> loadVisualDesc(IRenderer renderer, DatReader datReader, AC2Reader data) {
            VisualDesc visualDesc = new VisualDesc(data);

            List<DataId> setupDids = new List<DataId>();
            foreach (PartGroupDataDesc pgdDesc in visualDesc.pgdDescTable.Values) {
                setupDids.Add(pgdDesc.setupDid);
            }

            return loadSetups(renderer, datReader, setupDids);
        }
    }
}
