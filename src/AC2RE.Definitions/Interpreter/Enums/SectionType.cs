namespace AC2RE.Definitions {

    // Const *_SectionType
    public enum SectionType : uint {
        INVALID,
        OPCODE,
        STRING_LIT_TABLE,
        IMPORT_TABLE,
        EXPORT_TABLE,
        VERSION_INFO,
        VTABLE_INFO,
        VALID_EVENT_TABLE,

        FUNCTION_LOC_DEBUG = 256,
        SOURCE_FILE_DEBUG = 257,
        LINE_NUM_DEBUG = 258,
        FRAME_DEBUG_INFO = 259,
    }
}
