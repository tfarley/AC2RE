using System.Collections.Generic;

namespace AC2E.Def {

    public class AppearanceTable {

        public DataId did; // m_DID
        public List<uint> baseAppearance; // m_aBaseAppearance
        public Dictionary<uint, List<AtomCollection>> atoms; // m_atomData
        public Dictionary<uint, int> threshCounts; // m_threshCountHash

        public AppearanceTable(AC2Reader data) {
            did = data.ReadDataId();
            baseAppearance = data.ReadList(data.ReadUInt32);
            atoms = data.ReadDictionary(data.ReadUInt32, () => data.ReadList(() => new AtomCollection(data)));
            threshCounts = data.ReadDictionary(data.ReadUInt32, data.ReadInt32);
        }
    }
}
