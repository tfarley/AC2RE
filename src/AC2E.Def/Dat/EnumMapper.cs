using System.Collections.Generic;

namespace AC2E.Def {

    public class EnumMapper : DbObj {

        public DataId baseEnumMapperDid; // m_base_emp_did
        public Dictionary<uint, string> idToString; // m_id_to_string_map

        public EnumMapper(AC2Reader data) : base(data) {
            baseEnumMapperDid = data.ReadDataId();
            idToString = data.ReadDictionary(data.ReadUInt32, () => data.ReadString());
        }
    }
}
