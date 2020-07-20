using System;

namespace AC2E.Def {

    public class CPositionPack {

        // Enum CPositionPack::PackFlags
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,

            JUMP = 1 << 1, // 0x00000002
            CANCELMOVETO = 1 << 2, // 0x00000004
        }

        public double time; // m_time
        public PositionOffset offset; // m_offset
        public Heading heading; // m_heading
        public PackFlag packFlags;
        public bool cancelMoveTo; // m_cancel_moveto
        public bool jump; // m_jump
        public ushort instanceStamp; // m_instance_stamp
        public ushort teleportStamp; // m_teleport_stamp
        public ushort forcePositionStamp; // m_force_position_stamp
        public ushort movetoStamp; // m_moveto_stamp
        public Vector jumpVel; // m_vJumpVelocity

        public CPositionPack(AC2Reader data) {
            time = data.ReadDouble();
            offset = new PositionOffset(data);
            heading = data.ReadHeading();
            packFlags = (PackFlag)data.ReadUInt32();
            cancelMoveTo = packFlags.HasFlag(PackFlag.CANCELMOVETO);
            jump = packFlags.HasFlag(PackFlag.JUMP);
            instanceStamp = data.ReadUInt16();
            teleportStamp = data.ReadUInt16();
            forcePositionStamp = data.ReadUInt16();
            movetoStamp = data.ReadUInt16();
            if (jump) {
                jumpVel = data.ReadVector();
            }
            data.Align(4);
        }
    }
}
