using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CLandBlockInfo {

    public DataId did; // m_DID
    public uint version; // m_version
    public EntityGroupDesc entities; // m_entities
    public PropertyCollection properties; // m_properties
    public DataId lightInfoDid; // m_lightInfoDID
    public List<CellId> outsideStabList; // m_aOutsideStabList
    public uint numCells; // m_numCells

    public CLandBlockInfo(AC2Reader data) {
        bool hasVersion = data.ReadBoolean();
        did = data.ReadDataId();
        if (hasVersion) {
            version = data.ReadUInt32();
        }
        numCells = data.ReadUInt32();
        lightInfoDid = data.ReadDataId();
        entities = new(data);
        properties = new(data);
        outsideStabList = data.ReadList(data.ReadCellId);
    }
}
