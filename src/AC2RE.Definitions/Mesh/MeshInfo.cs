namespace AC2RE.Definitions {

    public class MeshInfo {

        public string meshName; // mesh_name
        public DataId meshDid; // mesh_did

        public MeshInfo(AC2Reader data) {
            meshName = data.ReadString();
            meshDid = data.ReadDataId();
        }
    }
}
