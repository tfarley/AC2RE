namespace AC2RE.Definitions {

    public class TravelRecallEffect : Effect {

        public override PackageType packageType => PackageType.TravelRecallEffect;

        public uint portalLink; // m_link
        public TravelRecallEffectFlag travelRecallFlags => (TravelRecallEffectFlag)flags;

        public TravelRecallEffect(AC2Reader data) : base(data) {
            portalLink = data.ReadUInt32();
        }
    }
}
