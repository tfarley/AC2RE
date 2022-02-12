using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EnumIdMap {

    // EnumIDMap
    public DataId did; // m_DID
    public Dictionary<EnumId, DataId> enumIdToDid; // m_EnumToID
    public Dictionary<EnumId, DataId> enumIdToDidInternal; // m_EnumToIDInternal
    public Dictionary<EnumId, string> enumIdToName; // m_EnumToName
    public Dictionary<EnumId, string> enumIdToNameInternal; // m_EnumToNameInternal

    public EnumIdMap(AC2Reader data) {
        did = data.ReadDataId();
        enumIdToDid = data.ReadDictionary(data.ReadEnumId, data.ReadDataId);
        enumIdToName = data.ReadDictionary(data.ReadEnumId, data.ReadString);
        enumIdToDidInternal = data.ReadDictionary(data.ReadEnumId, data.ReadDataId);
        enumIdToNameInternal = data.ReadDictionary(data.ReadEnumId, data.ReadString);
    }
}
