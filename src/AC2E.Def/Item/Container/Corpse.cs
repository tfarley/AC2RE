namespace AC2E.Def {

    public class Corpse : GameplayContainer {

        public override PackageType packageType => PackageType.Corpse;

        public Corpse(AC2Reader data) : base(data) {

        }
    }
}
