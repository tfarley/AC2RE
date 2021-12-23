namespace AC2RE.Definitions;

public class GameSaleProfile : SaleProfile {

    public override PackageType packageType => PackageType.GameSaleProfile;

    public uint ordinal; // m_uiOrdinal
    public bool restricted; // m_bRestricted

    public GameSaleProfile() {

    }

    public GameSaleProfile(AC2Reader data) : base(data) {
        ordinal = data.ReadUInt32();
        restricted = data.ReadBoolean();
    }

    public override void write(AC2Writer data) {
        base.write(data);
        data.Write(ordinal);
        data.Write(restricted);
    }
}
