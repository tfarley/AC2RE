using System;

namespace AC2E.Interp {

    [Flags]
    public enum FuncFlag : uint {
        NONE = 0x0,
        RESTRICTED = 0x1,
        SOURCE = 0x2,
        BINDABLE = 0x4,
        REQUESTEDIT = 0x8,
        DISPLAYBIND = 0x10,
        DEFAULTBIND = 0x20,
        HIDDEN = 0x40,
        USESGETLASTERROR = 0x80,
        DEFAULTCOLLELEM = 0x100,
        UIDEFAULT = 0x200,
        NONBROWSABLE = 0x400,
        REPLACEABLE = 0x800,
        IMMEDIATEBIND = 0x1000,
    }
}
