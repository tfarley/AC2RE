using System;
using System.Numerics;

namespace AC2RE.Definitions {

    public class CPositionPack {

        // Enum CPositionPack::PackFlags
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            JUMP = 1 << 1, // 0x00000002
            CANCELMOVETO = 1 << 2, // 0x00000004
        }

        public double time; // m_time
        public PositionOffset offset; // m_offset
        public Vector3 doMotion; // m_doMotion
        public Heading heading; // m_heading
        public PackFlag packFlags; // m_cancel_moveto, m_jump
        public ushort instanceStamp; // m_instance_stamp
        public ushort teleportStamp; // m_teleport_stamp
        public ushort forcePosStamp; // m_force_position_stamp
        public ushort movetoStamp; // m_moveto_stamp
        public Vector3 jumpVel; // m_vJumpVelocity

        public CPositionPack(AC2Reader data) {
            time = data.ReadDouble();
            offset = new(data);
            (doMotion, heading) = data.ReadVectorHeadingPack();
            packFlags = (PackFlag)data.ReadUInt32();
            instanceStamp = data.ReadUInt16();
            teleportStamp = data.ReadUInt16();
            forcePosStamp = data.ReadUInt16();
            movetoStamp = data.ReadUInt16();
            if (packFlags.HasFlag(PackFlag.JUMP)) {
                jumpVel = data.ReadVector();
            }
            data.Align(4);
        }
    }
}
