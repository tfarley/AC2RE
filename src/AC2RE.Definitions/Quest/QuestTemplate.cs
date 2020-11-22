namespace AC2RE.Definitions {

    public class QuestTemplate : Quest {

        public override PackageType packageType => PackageType.QuestTemplate;

        public SingletonPkg<QuestGlobals> questGlobals; // questGlobals

        public QuestTemplate(AC2Reader data) : base(data) {
            data.ReadSingletonPkg<QuestGlobals>(v => questGlobals = v);
        }
    }
}
