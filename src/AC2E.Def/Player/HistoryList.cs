namespace AC2E.Def {

    public class HistoryList : IPackage {

        public PackageType packageType => PackageType.HistoryList;

        public RList<IPackage> history; // m_history
        public uint size; // m_size
        public int index; // m_index

        public HistoryList(AC2Reader data) {
            data.ReadPkg<RList<IPackage>>(v => history = v);
            size = data.ReadUInt32();
            index = data.ReadInt32();
        }
    }
}
