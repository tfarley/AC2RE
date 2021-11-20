namespace AC2RE.Definitions {

    // Const *_SectionType
    public enum SectionType : uint {
        Invalid, // Invalid_SectionType
        Opcode, // Opcode_SectionType
        StringLitTable, // StringLitTable_SectionType
        ImportTable, // ImportTable_SectionType
        ExportTable, // ExportTable_SectionType
        VersionInfo, // VersionInfo_SectionType
        VTable, // VTable_SectionType
        ValidEventTable, // ValidEventTable_SectionType

        FunctionLocDebug = 256, // FunctionLocDebug_SectionType
        SourceFileDebug = 257, // SourceFileDebug_SectionType
        LineNumDebug = 258, // LineNumDebug_SectionType
        FrameDebugInfo = 259, // FrameDebugInfo_SectionType
    }
}
