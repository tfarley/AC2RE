using System;

namespace AC2RE.Definitions {

    public class TravelTieEffect : Effect {

        public override PackageType packageType => PackageType.TravelTieEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            IGNORE_PORTAL_FLAGS = 1 << 9, // 0x00000200, TravelTieEffectFlag::IsIgnorePortalFlags
        }

        public WeenieType weenieType; // m_wtype
        public uint portalLink; // m_link
        public Flag travelTieFlags => (Flag)flags;

        public TravelTieEffect(AC2Reader data) : base(data) {
            weenieType = (WeenieType)data.ReadUInt32();
            portalLink = data.ReadUInt32();
        }
    }
}
