namespace AC2E.Def {

    public class NPC : CAgent {

        public override PackageType packageType => PackageType.NPC;

        public NPC(AC2Reader data) : base(data) {

        }
    }
}
