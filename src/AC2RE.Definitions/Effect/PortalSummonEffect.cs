namespace AC2RE.Definitions {

    public class PortalSummonEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.PortalSummonEffect;

        public uint portalLink; // m_link

        public PortalSummonEffect(AC2Reader data) : base(data) {
            portalLink = data.ReadUInt32();
        }
    }
}
