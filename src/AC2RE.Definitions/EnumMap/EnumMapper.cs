using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EnumMapper {

    // EnumMapper
    public DataId did; // m_DID
    public DataId baseEnumMapperDid; // m_base_emp_did
    public Dictionary<uint, string> idToString; // m_id_to_string_map

    public EnumMapper(AC2Reader data) {
        did = data.ReadDataId();
        baseEnumMapperDid = data.ReadDataId();
        idToString = data.ReadDictionary(data.ReadUInt32, data.ReadString);
    }
}
