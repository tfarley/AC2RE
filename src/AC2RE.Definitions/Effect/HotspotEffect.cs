namespace AC2RE.Definitions;

public class HotspotEffect : VitalOverTimeEffect {

    public override PackageType packageType => PackageType.HotspotEffect;

    public Position staticPos; // m_posStatic
    public float radius; // m_fRadius

    public HotspotEffect(AC2Reader data) : base(data) {
        data.ReadHO<Position>(v => staticPos = v);
        radius = data.ReadSingle();
    }
}
