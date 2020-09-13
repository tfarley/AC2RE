namespace AC2E.Def {

    public class QuestVaultTemplate : QuestTemplate {

        public override PackageType packageType => PackageType.QuestVaultTemplate;

        public WPString stringTableName; // m_stringTableName

        public QuestVaultTemplate(AC2Reader data) : base(data) {
            data.ReadPkg<WPString>(v => stringTableName = v);
        }
    }
}
