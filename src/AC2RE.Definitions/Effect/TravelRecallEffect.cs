using System;

namespace AC2RE.Definitions {

    public class TravelRecallEffect : Effect {

        public override PackageType packageType => PackageType.TravelRecallEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            IGNORE_PORTAL_FLAGS = 1 << 9, // 0x00000200, TravelRecallEffect::IsIgnorePortalFlags
            IGNORE_PERMISSION = 1 << 10, // 0x00000400, TravelRecallEffect::IsIgnorePermission
        }

        public uint portalLink; // m_link
        public Flag travelRecallFlags => (Flag)flags;

        public TravelRecallEffect(AC2Reader data) : base(data) {
            portalLink = data.ReadUInt32();
        }
    }
}
