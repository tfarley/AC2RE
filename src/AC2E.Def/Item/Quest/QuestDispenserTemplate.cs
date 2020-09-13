namespace AC2E.Def {

    public class QuestDispenserTemplate : IItem {

        public override PackageType packageType => PackageType.QuestDispenserTemplate;

        public QuestDispenserTemplate(AC2Reader data) : base(data) {

        }
    }
}
