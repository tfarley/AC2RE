using System;

namespace AC2RE.Definitions {

    // Enum tagINVOKEKIND
    [Flags]
    public enum InvokeKind : uint {
        NONE = 0,
        FUNC = 1 << 0, // INVOKE_FUNC 0x00000001
        PROPERTYGET = 1 << 1, // INVOKE_PROPERTYGET 0x00000002
        PROPERTYPUT = 1 << 2, // INVOKE_PROPERTYPUT 0x00000004
        PROPERTYPUTREF = 1 << 3, // INVOKE_PROPERTYPUTREF 0x00000008
    }
}
