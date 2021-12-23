namespace AC2RE.Definitions;

public class Eff_PortalBeacon_PortalDeflectionGem : Effect {

    public override PackageType packageType => PackageType.Eff_PortalBeacon_PortalDeflectionGem;

    public StringInfo protEndedText; // m_siProtEnded

    public Eff_PortalBeacon_PortalDeflectionGem(AC2Reader data) : base(data) {
        data.ReadPkg<StringInfo>(v => protEndedText = v);
    }
}
