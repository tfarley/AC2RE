namespace AC2E.Def {

    public class CMoneySystem : MoneySystem {

        public override PackageType packageType => PackageType.CMoneySystem;

        public CMoneySystem(AC2Reader data) : base(data) {

        }
    }
}
