namespace AC2RE.Definitions;

public class CountdownEffect : Effect {

    public override PackageType packageType => PackageType.CountdownEffect;

    public SingletonPkg<Effect> resultEffect; // m_effResult

    public CountdownEffect(AC2Reader data) : base(data) {
        data.ReadPkg<Effect>(v => resultEffect = v);
    }
}
