using System;

namespace AC2RE.Definitions {

    public class TravelRecallEffect : Effect {

        public override PackageType packageType => PackageType.TravelRecallEffect;

        // WLib TravelRecallEffect
        [Flags]
        public new enum Flag : uint {
            None = 0,

            IsIgnorePortalFlags = 1 << 9, // IsIgnorePortalFlags 0x00000200
            IsIgnorePermission = 1 << 10, // IsIgnorePermission 0x00000400
        }

        public uint portalLink; // m_link
        public Flag travelRecallFlags => (Flag)flags;

        public TravelRecallEffect(AC2Reader data) : base(data) {
            portalLink = data.ReadUInt32();
        }
    }
}
