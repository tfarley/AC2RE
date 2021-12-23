using System;

namespace AC2RE.Definitions;

// Enum FileArrayFlags
[Flags]
public enum FileArrayFlag : uint {
    None = 0,
    EngineOnly = 1 << 0, // fafEngineOnly 0x00000001
    NukeDIDName = 1 << 1, // fafNukeDIDName 0x00000002
    ReadOnly = 1 << 2, // fafReadOnly 0x00000004
}
