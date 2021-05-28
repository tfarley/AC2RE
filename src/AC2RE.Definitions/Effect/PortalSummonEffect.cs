using System;

namespace AC2RE.Definitions {

    public class PortalSummonEffect : GenesisEffect {

        public override PackageType packageType => PackageType.PortalSummonEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            IGNORE_PORTAL_FLAGS = 1 << 9, // 0x00000200, PortalSummonEffect::IsIgnorePortalFlags
        }

        public uint portalLink; // m_link
        public Flag portalSummonFlags => (Flag)flags;

        public PortalSummonEffect(AC2Reader data) : base(data) {
            portalLink = data.ReadUInt32();
        }
    }
}
