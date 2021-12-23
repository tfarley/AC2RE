namespace AC2RE.Definitions;

public class TeleportEffect : Effect {

    public override PackageType packageType => PackageType.TeleportEffect;

    public Position fromLocation; // m_fromLocation
    public Position toLocation; // m_toLocation

    public TeleportEffect(AC2Reader data) : base(data) {
        data.ReadPkg<Position>(v => fromLocation = v);
        data.ReadPkg<Position>(v => toLocation = v);
    }
}
