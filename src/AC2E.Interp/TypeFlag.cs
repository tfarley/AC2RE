using System;

namespace AC2E.Interp {

    // Enum tagTYPEFLAGS
    [Flags]
    public enum TypeFlag : uint {
        NONE = 0,
        APPOBJECT = 0x1, // 0x00000001
        CANCREATE = 0x2, // 0x00000002
        LICENSED = 0x4, // 0x00000004
        PREDECLID = 0x8, // 0x00000008
        HIDDEN = 0x10, // 0x00000010
        CONTROL = 0x20, // 0x00000020
        DUAL = 0x40, // 0x00000040
        NONEXTENSIBLE = 0x80, // 0x00000080
        OLEAUTOMATION = 0x100, // 0x00000100
        RESTRICTED = 0x200, // 0x00000200
        AGGREGATABLE = 0x400, // 0x00000400
        REPLACEABLE = 0x800, // 0x00000800
        DISPATCHABLE = 0x1000, // 0x00001000
        REVERSEBIND = 0x2000, // 0x00002000
        PROXY = 0x4000, // 0x00004000
    }
}
