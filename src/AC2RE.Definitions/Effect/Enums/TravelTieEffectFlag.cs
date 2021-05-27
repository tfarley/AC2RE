using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum TravelTieEffectFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        IGNORE_PORTAL_FLAGS = 1 << 9, // 0x00000200, TravelTieEffectFlag::IsIgnorePortalFlags
    }
}
