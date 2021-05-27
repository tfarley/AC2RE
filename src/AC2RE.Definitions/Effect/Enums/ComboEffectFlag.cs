using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum ComboEffectFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        IGNORE_CONSIDERATION = 1 << 4, // 0x00000010, ComboEffect::IsIgnoreConsideration
    }
}
