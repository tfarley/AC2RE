namespace AC2E.Def {

    public class MineTemplate : Mine {

        public override PackageType packageType => PackageType.MineTemplate;

        public MineTemplate(AC2Reader data) : base(data) {

        }
    }
}
