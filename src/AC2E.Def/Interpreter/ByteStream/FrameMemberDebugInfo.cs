namespace AC2E.Def {

    public class FrameMemberDebugInfo {

        // Enum FrameMemberType
        public enum FrameMemberType : uint {
            UNDEF,
            VOID,
            INT,
            FLOAT,
            MIXED,
            TIME,
            TYPE_NAME,
            STRING,
            ENUM,
            PACKAGE,
        }

        public uint offset; // Offset
        public FrameMemberType type; // Type
        public VarFlag flags; // Flags
        public string name; // Name
        public string typeName; // TypeName

        public FrameMemberDebugInfo(AC2Reader data) {
            offset = data.ReadUInt32();
            type = (FrameMemberType)data.ReadUInt32();
            flags = (VarFlag)data.ReadUInt32();
            name = data.ReadString();
            typeName = data.ReadString();
        }
    }
}
