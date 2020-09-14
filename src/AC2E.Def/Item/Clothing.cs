namespace AC2E.Def {

    public class Clothing : gmCEntity {

        public override PackageType packageType => PackageType.Clothing;

        // TODO: Double check the keys/values are not reversed in this hash
        public ADataIdHash wornAppearanceDidHash; // m_hashWornAppearanceDID

        public Clothing(AC2Reader data) : base(data) {
            data.ReadPkg<AAHash>(v => wornAppearanceDidHash = new ADataIdHash(v));
        }
    }
}
