using System.Collections.Generic;

namespace AC2RE.Definitions;

public class MaterialInstance {

    // MaterialInstance
    public DataId did; // m_DID
    public DataId materialDid; // m_materialID
    public MaterialTypeId materialType; // m_materialType
    public List<DataId> modifierDids; // m_aModifierRefs

    public MaterialInstance(AC2Reader data) {
        did = data.ReadDataId();
        materialDid = data.ReadDataId();
        materialType = data.ReadEnum<MaterialTypeId>();
        modifierDids = data.ReadList(data.ReadDataId);
    }
}
