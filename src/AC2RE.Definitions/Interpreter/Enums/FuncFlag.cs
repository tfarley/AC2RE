using System;

namespace AC2RE.Definitions {

    // Enum tagFUNCFLAGS
    [Flags]
    public enum FuncFlag : uint {
        NONE = 0,
        RESTRICTED = 1 << 0, // 0x00000001
        SOURCE = 1 << 1, // 0x00000002
        BINDABLE = 1 << 2, // 0x00000004
        REQUESTEDIT = 1 << 3, // 0x00000008
        DISPLAYBIND = 1 << 4, // 0x00000010
        DEFAULTBIND = 1 << 5, // 0x00000020
        HIDDEN = 1 << 6, // 0x00000040
        USESGETLASTERROR = 1 << 7, // 0x00000080
        DEFAULTCOLLELEM = 1 << 8, // 0x00000100
        UIDEFAULT = 1 << 9, // 0x00000200
        NONBROWSABLE = 1 << 10, // 0x00000400
        REPLACEABLE = 1 << 11, // 0x00000800
        IMMEDIATEBIND = 1 << 12, // 0x00001000
    }
}
