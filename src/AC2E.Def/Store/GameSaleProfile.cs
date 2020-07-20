namespace AC2E.Def {

    public class GameSaleProfile : SaleProfile {

        public override PackageType packageType => PackageType.GameSaleProfile;

        public uint m_uiOrdinal;
        public bool m_bRestricted;

        public GameSaleProfile() {

        }

        public GameSaleProfile(AC2Reader data) : base(data) {
            m_uiOrdinal = data.ReadUInt32();
            m_bRestricted = data.ReadBoolean();
        }

        public override void write(AC2Writer data) {
            base.write(data);
            data.Write(m_uiOrdinal);
            data.Write(m_bRestricted);
        }
    }
}
