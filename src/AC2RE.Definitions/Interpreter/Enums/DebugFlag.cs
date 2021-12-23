using System;

namespace AC2RE.Definitions;

// Enum DebugFlagBits
[Flags]
public enum DebugFlag : uint {
    None = 0,
    DbgOutputToDebugger = 1 << 0, // DbgOutputToDebugger 0x00000001
    DbgOutputToFile = 1 << 1, // DbgOutputToFile 0x00000002
    DbgOutputToUser = 1 << 2, // DbgOutputToUser 0x00000004
    DbgOutputAssertDlgs = 1 << 3, // DbgOutputAssertDlgs 0x00000008
    DbgPrintfEnabled = 1 << 4, // DbgPrintfEnabled 0x00000010
    DbgDbgAssertsAssert = 1 << 5, // DbgDbgAssertsAssert 0x00000020
    DbgAssertsAssert = 1 << 6, // DbgAssertsAssert 0x00000040
    DbgWslAssertsAssert = 1 << 7, // DbgWslAssertsAssert 0x00000080
    DbgPerfAssertsAssert = 1 << 8, // DbgPerfAssertsAssert 0x00000100
    DbgEnableExceptionHandler = 1 << 9, // DbgEnableExceptionHandler 0x00000200
    DbgEnableFloatingPointExceptions = 1 << 10, // DbgEnableFloatingPointExceptions 0x00000400
    DbgPlaceLogsInAppDir = 1 << 11, // DbgPlaceLogsInAppDir 0x00000800
    DbgNoAutomaticStackTrace = 1 << 12, // DbgNoAutomaticStackTrace 0x00001000
}
