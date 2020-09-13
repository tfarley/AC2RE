namespace AC2E.Def {

    public class ActTemplate : Act {

        public override PackageType packageType => PackageType.ActTemplate;

        public SingletonPkg<QuestGlobals> questGlobals; // questGlobals

        public ActTemplate(AC2Reader data) : base(data) {
            data.ReadSingletonPkg(v => questGlobals = v.to<QuestGlobals>());
        }
    }
}
