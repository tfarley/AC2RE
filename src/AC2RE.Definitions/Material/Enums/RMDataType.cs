namespace AC2RE.Definitions;

// Enum RMDataType
public enum RMDataType : uint {
    UNDEF = 0,

    WAVEFORM = 1000, // RMDATA_WAVEFORM
    COLOR = 2000, // RMDATA_COLOR
    TEXTURE = 3000, // RMDATA_TEXTURE
    BOOL = 4000, // RMDATA_BOOL
    INVALID = 0x7FFFFFFF, // RMDATA_INVALID
}
