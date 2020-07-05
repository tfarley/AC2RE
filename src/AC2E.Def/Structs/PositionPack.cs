using AC2E.Utils;
using System;
using System.IO;

namespace AC2E.Def {

    public class PositionPack {

        // Enum PositionPack::PackFlags
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,

            JUMP = 1 << 1, // 0x00000002
            CONTACT = 1 << 2, // 0x00000004
            IMPULSE = 1 << 3, // 0x00000008
        }

        public double time; // m_time
        public PositionOffset offset; // m_offset
        public Heading heading; // m_heading
        public PackFlag packFlags;
        public bool contact; // m_contact
        public bool jump; // m_jump
        public bool impulse; // m_impulse
        public ushort positionStamp; // m_position_stamp
        public ushort forcePositionStamp; // m_force_position_stamp
        public ushort teleportStamp; // m_teleport_stamp
        public Vector impulseVel; // m_impulseVel

        public PositionPack(BinaryReader data) {
            time = data.ReadDouble();
            offset = new PositionOffset(data);
            heading = data.ReadHeading();
            packFlags = (PackFlag)data.ReadUInt32();
            contact = packFlags.HasFlag(PackFlag.CONTACT);
            jump = packFlags.HasFlag(PackFlag.JUMP);
            impulse = packFlags.HasFlag(PackFlag.IMPULSE);
            positionStamp = data.ReadUInt16();
            forcePositionStamp = data.ReadUInt16();
            teleportStamp = data.ReadUInt16();
            if (impulse) {
                impulseVel = data.ReadVector();
            }
            data.Align(4);
        }
    }
}
