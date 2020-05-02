using System;

namespace AC2E.Interp {

    // Enum tagLIBFLAGS
    [Flags]
    public enum LibFlag : uint {
        NONE = 0,
        RESTRICTED = 1 << 0, // 0x00000001
        CONTROL = 1 << 1, // 0x00000002
        HIDDEN = 1 << 2, // 0x00000004
        HASDISKIMAGE = 1 << 3, // 0x00000008
    }
}
