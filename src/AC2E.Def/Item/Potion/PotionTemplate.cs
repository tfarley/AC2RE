namespace AC2E.Def {

    public class PotionTemplate : Potion {

        public override PackageType packageType => PackageType.PotionTemplate;

        public PotionTemplate(AC2Reader data) : base(data) {

        }
    }
}
