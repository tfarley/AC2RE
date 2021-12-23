using System;
using System.Numerics;

namespace AC2RE.Definitions;

public class PositionPack {

    // Enum PositionPack::PackFlags
    [Flags]
    public enum PackFlag : uint {
        NONE = 0,

        JUMP = 1 << 1, // PF_JUMP 0x00000002
        CONTACT = 1 << 2, // PF_CONTACT 0x00000004
        IMPULSE = 1 << 3, // PF_IMPULSE 0x00000008
    }

    public double time; // m_time
    public PositionOffset offset; // m_offset
    public Vector3 doMotion; // m_doMotion
    public Heading heading; // m_heading
    public PackFlag packFlags; // m_contact, m_jump, m_impulse
    public ushort posStamp; // m_position_stamp
    public ushort forcePosStamp; // m_force_position_stamp
    public ushort teleportStamp; // m_teleport_stamp
    public Vector3 impulseVel; // m_impulseVel

    public PositionPack() {

    }

    public PositionPack(AC2Reader data) {
        time = data.ReadDouble();
        offset = new(data);
        (doMotion, heading) = data.ReadVectorHeadingPack();
        packFlags = (PackFlag)data.ReadUInt32();
        posStamp = data.ReadUInt16();
        forcePosStamp = data.ReadUInt16();
        teleportStamp = data.ReadUInt16();
        if (packFlags.HasFlag(PackFlag.JUMP) || packFlags.HasFlag(PackFlag.IMPULSE)) {
            impulseVel = data.ReadVector();
        }
        data.Align(4);
    }

    public void write(AC2Writer data) {
        data.Write(time);
        offset.write(data);
        data.Write(doMotion, heading);
        data.Write((uint)packFlags);
        data.Write(posStamp);
        data.Write(forcePosStamp);
        data.Write(teleportStamp);
        if (packFlags.HasFlag(PackFlag.JUMP) || packFlags.HasFlag(PackFlag.IMPULSE)) {
            data.Write(impulseVel);
        }
        data.Align(4);
    }
}
