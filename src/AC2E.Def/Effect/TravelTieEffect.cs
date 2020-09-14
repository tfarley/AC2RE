namespace AC2E.Def {

    public class TravelTieEffect : Effect {

        public override PackageType packageType => PackageType.TravelTieEffect;

        public uint weenieType; // m_wtype
        public uint portalLink; // m_link

        public TravelTieEffect(AC2Reader data) : base(data) {
            weenieType = data.ReadUInt32();
            portalLink = data.ReadUInt32();
        }
    }
}
