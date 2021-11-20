namespace AC2RE.Definitions {

    // Const *_MusicType
    public enum MusicType : uint {
        Undef = 0, // Undef_MusicType

        EncWav = 0x40000001, // EncWav_MusicType
        Script = 0x40000002, // Script_MusicType

        Segment = 0x80000001, // Segment_MusicType
        Style = 0x80000002, // Style_MusicType
        ChordMap = 0x80000003, // ChordMap_MusicType
        Band = 0x80000004, // Band_MusicType
        Template = 0x80000005, // Template_MusicType
        DLS = 0x80000006, // DLS_MusicType
        Filename = 0x80000007, // Filename_MusicType
        Motif = 0x80000008, // Motif_MusicType
        Wav = 0x80000009, // Wav_MusicType
        AudioPath = 0x8000000A, // AudioPath_MusicType
    }
}
