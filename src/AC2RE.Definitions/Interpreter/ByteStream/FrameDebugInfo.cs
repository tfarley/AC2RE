using System.Collections.Generic;

namespace AC2RE.Definitions;

public class FrameDebugInfo {

    // Enum FrameType
    public enum FrameType : uint {
        Undef, // Undef_FrameType
        Function, // Function_FrameType
        Package, // Package_FrameType
    }

    // FrameDebugInfo
    public string name; // m_name
    public FrameType type; // m_type
    public uint size; // m_size
    public List<FrameMemberDebugInfo> members; // m_refMembers

    public FrameDebugInfo(AC2Reader data) {
        name = data.ReadString();
        type = data.ReadEnum<FrameType>();
        size = data.ReadUInt32();
        members = data.ReadList(() => new FrameMemberDebugInfo(data));
    }
}
