﻿namespace AC2RE.Definitions {

    // Const *_PropertyType
    public enum PropertyType : uint {
        INVALID = 0,

        BOOL = 0x40000001,
        INTEGER = 0x40000002,
        FLOAT = 0x40000003,
        VECTOR = 0x40000004,
        COLOR = 0x40000005,
        STRING = 0x40000006,
        ENUM = 0x40000007,
        DATA_FILE = 0x40000008,
        WAVEFORM = 0x40000009,
        STRING_INFO = 0x4000000A,
        PACKAGE_ID = 0x4000000B,

        LONG_INTEGER = 0x4000000D,
        POSITION = 0x4000000E,
    }
}