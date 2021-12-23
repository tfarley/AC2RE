using System;

namespace AC2RE.Definitions;

// Enum tagLIBFLAGS
[Flags]
public enum LibFlag : uint {
    NONE = 0,
    RESTRICTED = 1 << 0, // LIBFLAG_FRESTRICTED 0x00000001
    CONTROL = 1 << 1, // LIBFLAG_FCONTROL 0x00000002
    HIDDEN = 1 << 2, // LIBFLAG_FHIDDEN 0x00000004
    HASDISKIMAGE = 1 << 3, // LIBFLAG_FHASDISKIMAGE 0x00000008
}
