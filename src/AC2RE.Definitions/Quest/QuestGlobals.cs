namespace AC2RE.Definitions;

public class QuestGlobals : IPackage {

    public PackageType packageType => PackageType.QuestGlobals;

    public uint notApplicable; // m_uintNotApplicable

    public QuestGlobals(AC2Reader data) {
        notApplicable = data.ReadUInt32();
    }
}
