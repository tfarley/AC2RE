namespace AC2RE.Definitions;

public class VisualDescEffect : QualitiesEffect {

    public override PackageType packageType => PackageType.VisualDescEffect;

    public DataId visualDescDid; // m_visualDescDID

    public VisualDescEffect(AC2Reader data) : base(data) {
        visualDescDid = data.ReadDataId();
    }
}
