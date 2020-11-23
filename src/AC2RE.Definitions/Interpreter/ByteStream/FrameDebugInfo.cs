﻿using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class FrameDebugInfo {

        public enum FrameType : uint {
            UNDEF,
            FUNCTION,
            PACKAGE,
        }

        public string name; // m_name
        public FrameType type; // m_type
        public uint size; // m_size
        public List<FrameMemberDebugInfo> members; // m_refMembers

        public FrameDebugInfo(AC2Reader data) {
            name = data.ReadString();
            type = (FrameType)data.ReadUInt32();
            size = data.ReadUInt32();
            members = data.ReadList(() => new FrameMemberDebugInfo(data));
        }
    }
}