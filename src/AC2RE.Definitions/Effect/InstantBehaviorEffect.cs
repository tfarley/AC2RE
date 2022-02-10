namespace AC2RE.Definitions;

public class InstantBehaviorEffect : Effect {

    public override PackageType packageType => PackageType.InstantBehaviorEffect;

    public BehaviorParams behaviorParams; // m_bp

    public InstantBehaviorEffect(AC2Reader data) : base(data) {
        data.ReadHO<BehaviorParams>(v => behaviorParams = v);
    }
}
