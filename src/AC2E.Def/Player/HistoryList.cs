using System.Collections.Generic;

namespace AC2E.Def {

    public class HistoryList : IPackage {

        public PackageType packageType => PackageType.HistoryList;

        public List<IPackage> history; // m_history
        public uint size; // m_size
        public int index; // m_index

        public HistoryList(AC2Reader data) {
            data.ReadPkg<RList>(v => history = v);
            size = data.ReadUInt32();
            index = data.ReadInt32();
        }
    }
}
