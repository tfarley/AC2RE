namespace AC2E.Def {

    public class QuestGlobals : IPackage {

        public PackageType packageType => PackageType.QuestGlobals;

        public uint notApplicable; // m_uintNotApplicable

        public QuestGlobals(AC2Reader data) {
            notApplicable = data.ReadUInt32();
        }
    }
}
