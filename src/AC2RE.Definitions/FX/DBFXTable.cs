using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class DBFXTable {

        public DataId did; // m_DID
        public Dictionary<uint, List<FXData>> table; // m_table
        public List<uint> subTables; // m_sub_tables

        public DBFXTable(AC2Reader data) {
            did = data.ReadDataId();
            table = data.ReadDictionary(data.ReadUInt32, () => data.ReadList(() => new FXData(data)));
            subTables = data.ReadList(data.ReadUInt32);
        }
    }
}
