namespace AC2RE.Definitions;

public class FrameMemberDebugInfo {

    // Enum FrameMemberType
    public enum FrameMemberType : uint {
        Undef, // fmUndef_Type
        Void, // fmVoid_Type
        Int, // fmInt_Type
        Float, // fmFloat_Type
        Mixed, // fmMixed_Type
        Timetype, // fmTimetype_Type
        TypeName, // fmTypeName_Type
        String, // fmString_Type
        Enum, // fmEnum_Type
        Package, // fmPackage_Type
    }

    // FrameMemberDebugInfo
    public int offset; // Offset
    public FrameMemberType type; // Type
    public VarFlag flags; // Flags
    public string name; // Name
    public string typeName; // TypeName

    public FrameMemberDebugInfo(AC2Reader data) {
        offset = data.ReadInt32();
        type = (FrameMemberType)data.ReadUInt32();
        flags = (VarFlag)data.ReadUInt32();
        name = data.ReadString();
        typeName = data.ReadString();
    }
}
