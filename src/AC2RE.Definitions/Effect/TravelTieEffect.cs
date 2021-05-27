namespace AC2RE.Definitions {

    public class TravelTieEffect : Effect {

        public override PackageType packageType => PackageType.TravelTieEffect;

        public WeenieType weenieType; // m_wtype
        public uint portalLink; // m_link
        public TravelTieEffectFlag travelTieFlags => (TravelTieEffectFlag)flags;

        public TravelTieEffect(AC2Reader data) : base(data) {
            weenieType = (WeenieType)data.ReadUInt32();
            portalLink = data.ReadUInt32();
        }
    }
}
