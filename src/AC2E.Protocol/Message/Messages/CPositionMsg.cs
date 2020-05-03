using AC2E.Def.Extensions;
using AC2E.Def.Structs;
using AC2E.Protocol.NetBlob;
using System;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class CPositionMsg : INetMessage {

        public class PositionOffset {

            public CellId cellId; // m_cid
            public Vector offset; // m_offset

            public PositionOffset(BinaryReader data) {
                cellId = data.ReadCellId();
                offset = data.ReadVector();
            }
        }

        // Enum CPositionPack::Flag
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,

            JUMP = 1 << 1,
            CANCELMOVETO = 1 << 2,
        }

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;

        public NetQueue queueId => NetQueue.EVENT;

        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CPosition_ID;

        // CPositionPack
        public double time; // m_time
        public PositionOffset offset; // m_offset
        public Heading heading; // m_heading
        public PackFlag packFlags;
        public ushort instanceStamp; // m_instance_stamp
        public ushort teleportStamp; // m_teleport_stamp
        public ushort forcePositionStamp; // m_force_position_stamp
        public ushort movetoStamp; // m_moveto_stamp
        public Vector jumpVelocity; // m_vJumpVelocity
        public Vector doMotion; // m_doMotion
        public uint unk1;

        public CPositionMsg(BinaryReader data) {
            time = data.ReadDouble();
            offset = new PositionOffset(data);
            heading = data.ReadHeading();
            packFlags = (PackFlag)data.ReadUInt32();
            instanceStamp = data.ReadUInt16();
            teleportStamp = data.ReadUInt16();
            forcePositionStamp = data.ReadUInt16();
            movetoStamp = data.ReadUInt16();
            if (packFlags.HasFlag(PackFlag.JUMP)) {
                jumpVelocity = data.ReadVector();
            }
            doMotion = data.ReadVector(); // TODO: Guessing this is what trailing zeroes are
            unk1 = data.ReadUInt32();
        }
    }
}
