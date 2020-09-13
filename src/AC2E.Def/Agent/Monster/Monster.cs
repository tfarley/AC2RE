namespace AC2E.Def {

    public class Monster : NPC {

        public override PackageType packageType => PackageType.Monster;

        public Monster(AC2Reader data) : base(data) {

        }
    }
}
