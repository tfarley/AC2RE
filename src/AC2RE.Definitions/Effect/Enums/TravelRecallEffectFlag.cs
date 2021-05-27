using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum TravelRecallEffectFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        IGNORE_PORTAL_FLAGS = 1 << 9, // 0x00000200, TravelRecallEffect::IsIgnorePortalFlags
        IGNORE_PERMISSION = 1 << 10, // 0x00000400, TravelRecallEffect::IsIgnorePermission
    }
}
