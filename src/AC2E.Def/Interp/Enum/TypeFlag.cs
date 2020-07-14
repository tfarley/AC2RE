using System;

namespace AC2E.Def {

    // Enum tagTYPEFLAGS
    [Flags]
    public enum TypeFlag : uint {
        NONE = 0,
        APPOBJECT = 1 << 0, // 0x00000001
        CANCREATE = 1 << 1, // 0x00000002
        LICENSED = 1 << 2, // 0x00000004
        PREDECLID = 1 << 3, // 0x00000008
        HIDDEN = 1 << 4, // 0x00000010
        CONTROL = 1 << 5, // 0x00000020
        DUAL = 1 << 6, // 0x00000040
        NONEXTENSIBLE = 1 << 7, // 0x00000080
        OLEAUTOMATION = 1 << 8, // 0x00000100
        RESTRICTED = 1 << 9, // 0x00000200
        AGGREGATABLE = 1 << 10, // 0x00000400
        REPLACEABLE = 1 << 11, // 0x00000800
        DISPATCHABLE = 1 << 12, // 0x00001000
        REVERSEBIND = 1 << 13, // 0x00002000
        PROXY = 1 << 14, // 0x00004000
    }
}
