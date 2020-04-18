using System;

namespace AC2E.Interp {

    [Flags]
    public enum TypeFlag : uint {
        NONE = 0x0,
        APPOBJECT = 0x1,
        CANCREATE = 0x2,
        LICENSED = 0x4,
        PREDECLID = 0x8,
        HIDDEN = 0x10,
        CONTROL = 0x20,
        DUAL = 0x40,
        NONEXTENSIBLE = 0x80,
        OLEAUTOMATION = 0x100,
        RESTRICTED = 0x200,
        AGGREGATABLE = 0x400,
        REPLACEABLE = 0x800,
        DISPATCHABLE = 0x1000,
        REVERSEBIND = 0x2000,
        PROXY = 0x4000,
    }
}
