namespace AC2E.Def {

    public class CMonster : Monster {

        public override PackageType packageType => PackageType.CMonster;

        public CMonster(AC2Reader data) : base(data) {

        }
    }
}
