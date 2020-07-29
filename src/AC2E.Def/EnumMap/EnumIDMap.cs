using System.Collections.Generic;

namespace AC2E.Def {

    public class EnumIDMap {

        public DataId did; // m_DID
        public Dictionary<uint, uint> enumToId; // m_EnumToID
        public Dictionary<uint, uint> enumToIdInternal; // m_EnumToIDInternal
        public Dictionary<uint, string> enumToName; // m_EnumToName
        public Dictionary<uint, string> enumToNameInternal; // m_EnumToNameInternal

        public EnumIDMap(AC2Reader data) {
            did = data.ReadDataId();
            enumToId = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
            enumToName = data.ReadDictionary(data.ReadUInt32, data.ReadString);
            enumToIdInternal = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
            enumToNameInternal = data.ReadDictionary(data.ReadUInt32, data.ReadString);
        }
    }
}
