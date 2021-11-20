using System;

namespace AC2RE.Definitions {

    // Enum OptionalHeaderFlags
    [Flags]
    public enum OptionalHeaderFlag : uint {
        None = 0,
        Disposable = 1 << 0, // ohfDisposable 0x00000001
        Exclusive = 1 << 1, // ohfExclusive 0x00000002
        NotConn = 1 << 2, // ohfNotConn 0x00000004
        TimeSensitive = 1 << 3, // ohfTimeSensitive 0x00000008
        ShouldPiggyBack = 1 << 4, // ohfShouldPiggyBack 0x00000010
        HighPriority = 1 << 5, // ohfHighPriority 0x00000020
        CountsAsTouch = 1 << 6, // ohfCountsAsTouch 0x00000040

        Encrypted = 1 << 29, // ohfEncrypted 0x20000000
        Signed = 1 << 30, // ohfSigned 0x40000000
    }
}
