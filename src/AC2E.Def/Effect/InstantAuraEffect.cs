namespace AC2E.Def {

    public class InstantAuraEffect : AuraEffect {

        public override PackageType packageType => PackageType.InstantAuraEffect;

        public InstantAuraEffect(AC2Reader data) : base(data) {

        }
    }
}
