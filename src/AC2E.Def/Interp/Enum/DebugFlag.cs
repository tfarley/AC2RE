using System;

namespace AC2E.Def {

    // Enum DebugFlagBits
    [Flags]
    public enum DebugFlag : uint {
        NONE = 0,
        OUTPUT_TO_DEBUGGER = 1 << 0, // 0x00000001
        OUTPUT_TO_FILE = 1 << 1, // 0x00000002
        OUTPUT_TO_USER = 1 << 2, // 0x00000004
        OUTPUT_ASSERT_DIALOGS = 1 << 3, // 0x00000008
        PRINTF_ENABLED = 1 << 4, // 0x00000010
        DBG_ASSERTS_ASSERT = 1 << 5, // 0x00000020
        ASSERTS_ASSERT = 1 << 6, // 0x00000040
        WSL_ASSERTS_ASSERT = 1 << 7, // 0x00000080
        PERF_ASSERTS_ASSERT = 1 << 8, // 0x00000100
        ENABLE_EXCEPTION_HANDLER = 1 << 9, // 0x00000200
        ENABLE_FLOATING_POINT_EXCEPTIONS = 1 << 10, // 0x00000400
        PLACE_LOGS_IN_APP_DIR = 1 << 11, // 0x00000800
        NO_AUTOMATIC_STACK_TRACE = 1 << 12, // 0x00001000
    }
}
