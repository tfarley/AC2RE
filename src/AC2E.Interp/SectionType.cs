﻿namespace AC2E.Interp {

    public enum SectionType : uint {
        INVALID = 0,
        OPCODE = 1,
        STRING_LIT_TABLE = 2,
        IMPORT_TABLE = 3,
        EXPORT_TABLE = 4,
        VERSION_INFO = 5,
        VTABLE_INFO = 6,
        VALID_EVENT_TABLE = 7,
        FUNCTION_LOC_DEBUG = 256,
        SOURCE_FILE_DEBUG = 257,
        LINE_NUM_DEBUG = 258,
        FRAME_DEBUG_INFO = 259,
    }
}
