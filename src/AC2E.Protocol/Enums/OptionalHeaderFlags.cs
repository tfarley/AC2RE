using System;

namespace AC2E.Protocol {

    // Enum OptionalHeaderFlags
    [Flags]
    public enum OptionalHeaderFlag : uint {
        NONE = 0,
        DISPOSABLE = 1 << 0, // 0x00000001
        EXCLUSIVE = 1 << 1, // 0x00000002
        NOT_CONN = 1 << 2, // 0x00000004
        TIME_SENSITIVE = 1 << 3, // 0x00000008
        SHOULD_PIGGYBACK = 1 << 4, // 0x00000010
        HIGH_PRIORITY = 1 << 5, // 0x00000020
        COUNT_AS_TOUCH = 1 << 6, // 0x00000040

        ENCRYPTED = 1 << 29, // 0x20000000
        SIGNED = 1 << 30, // 0x40000000
    }
}
