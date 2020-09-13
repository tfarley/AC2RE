namespace AC2E.Def {

    public class MonsterTemplate : CMonster {

        public override PackageType packageType => PackageType.MonsterTemplate;

        public MonsterTemplate(AC2Reader data) : base(data) {

        }
    }
}
