namespace AC2E.Def {

    public class TravelRecallEffect : Effect {

        public override PackageType packageType => PackageType.TravelRecallEffect;

        public uint portalLink; // m_link

        public TravelRecallEffect(AC2Reader data) : base(data) {
            portalLink = data.ReadUInt32();
        }
    }
}
