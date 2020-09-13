namespace AC2E.Def {

    public class QuestTemplate : Quest {

        public override PackageType packageType => PackageType.QuestTemplate;

        public SingletonPkg<QuestGlobals> questGlobals; // questGlobals

        public QuestTemplate(AC2Reader data) : base(data) {
            data.ReadSingletonPkg(v => questGlobals = v.to<QuestGlobals>());
        }
    }
}
