using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EnumIdMap {

    // EnumIDMap
    public DataId did; // m_DID
    public Dictionary<EnumId, DataId> enumToId; // m_EnumToID
    public Dictionary<EnumId, DataId> enumToIdInternal; // m_EnumToIDInternal
    public Dictionary<EnumId, string> enumToName; // m_EnumToName
    public Dictionary<EnumId, string> enumToNameInternal; // m_EnumToNameInternal

    public EnumIdMap(AC2Reader data) {
        did = data.ReadDataId();
        enumToId = data.ReadDictionary(data.ReadEnumId, data.ReadDataId);
        enumToName = data.ReadDictionary(data.ReadEnumId, data.ReadString);
        enumToIdInternal = data.ReadDictionary(data.ReadEnumId, data.ReadDataId);
        enumToNameInternal = data.ReadDictionary(data.ReadEnumId, data.ReadString);
    }
}
