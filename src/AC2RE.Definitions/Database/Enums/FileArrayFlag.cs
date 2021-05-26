using System;

namespace AC2RE.Definitions {

    // Enum FileArrayFlags
    [Flags]
    public enum FileArrayFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        ENGINE_ONLY = 1 << 0, // 0x00000001
        NUKE_DID_NAME = 1 << 1, // 0x00000002
        READ_ONLY = 1 << 2, // 0x00000004
    }
}
