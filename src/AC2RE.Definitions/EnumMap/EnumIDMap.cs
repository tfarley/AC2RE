using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EnumIDMap {

    public DataId did; // m_DID
    public Dictionary<EnumId, DataId> enumToId; // m_EnumToID
    public Dictionary<EnumId, DataId> enumToIdInternal; // m_EnumToIDInternal
    public Dictionary<EnumId, string> enumToName; // m_EnumToName
    public Dictionary<EnumId, string> enumToNameInternal; // m_EnumToNameInternal

    public EnumIDMap(AC2Reader data) {
        did = data.ReadDataId();
        enumToId = data.ReadDictionary(data.ReadEnumId, data.ReadDataId);
        enumToName = data.ReadDictionary(data.ReadEnumId, data.ReadString);
        enumToIdInternal = data.ReadDictionary(data.ReadEnumId, data.ReadDataId);
        enumToNameInternal = data.ReadDictionary(data.ReadEnumId, data.ReadString);
    }
}
