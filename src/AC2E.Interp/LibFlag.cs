using System;

namespace AC2E.Interp {

    [Flags]
    public enum LibFlag : uint {
        NONE = 0x0,
        RESTRICTED = 0x1,
        CONTROL = 0x2,
        HIDDEN = 0x4,
        HASDISKIMAGE = 0x8,
    }
}
