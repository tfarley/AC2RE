namespace AC2E.Def {

    public class AllegianceHallBindingStoneUsageBlob : UsageBlob {

        public override PackageType packageType => PackageType.UsageBlob;

        public WPString dest; // m_strDest

        public AllegianceHallBindingStoneUsageBlob() : base() {

        }

        public AllegianceHallBindingStoneUsageBlob(AC2Reader data) : base(data) {
            data.ReadPkg<WPString>(v => dest = v);
        }

        public override void write(AC2Writer data) {
            base.write(data);
            data.WritePkg(dest);
        }
    }
}
