using System.Collections.Generic;

namespace AC2E.Def {

    public class MaterialInstance {

        public DataId did; // m_DID
        public DataId materialDid; // m_materialID
        public uint materialType; // m_materialType
        public List<DataId> modifierDids; // m_aModifierRefs

        public MaterialInstance(AC2Reader data) {
            did = data.ReadDataId();
            materialDid = data.ReadDataId();
            materialType = data.ReadUInt32();
            modifierDids = data.ReadList(data.ReadDataId);
        }
    }
}
