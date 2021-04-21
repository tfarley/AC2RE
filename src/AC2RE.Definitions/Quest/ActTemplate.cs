namespace AC2RE.Definitions {

    public class ActTemplate : Act {

        public override PackageType packageType => PackageType.ActTemplate;

        public SingletonPkg<QuestGlobals> questGlobals; // questGlobals

        public ActTemplate(AC2Reader data) : base(data) {
            data.ReadPkg<QuestGlobals>(v => questGlobals = v);
        }
    }
}
