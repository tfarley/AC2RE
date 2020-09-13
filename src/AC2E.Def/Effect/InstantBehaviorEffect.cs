namespace AC2E.Def {

    public class InstantBehaviorEffect : InstantEffect {

        public override PackageType packageType => PackageType.InstantBehaviorEffect;

        public BehaviorParams behaviorParams; // m_bp

        public InstantBehaviorEffect(AC2Reader data) : base(data) {
            data.ReadPkg<BehaviorParams>(v => behaviorParams = v);
        }
    }
}
