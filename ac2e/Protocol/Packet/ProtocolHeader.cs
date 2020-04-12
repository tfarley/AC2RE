using System;
using System.IO;

public class ProtocolHeader {

    [Flags]
    public enum Flag : uint {
        NONE = 0,

        // Flags
        RETRANSMITTING = 1 << 0,
        ENCRYPTED_CHECKSUM = 1 << 1,
        FRAGMENTS = 1 << 2,

        // Optional headers, see COptionalHeaderAllocatorTemplate
        SERVER_SWITCH = 1 << 8,
        LOGON_SERVER_ADDR = 1 << 9,
        UNK_1 = 1 << 10, // TODO: Response to server switch?
        REFERRAL = 1 << 11,
        NACKS = 1 << 12,
        NO_RETRANSMIT = 1 << 13,
        ACK = 1 << 14,
        DISCONNECT = 1 << 15,
        LOGON_REQUEST = 1 << 16,
        WORLD_LOGON_REQUEST = 1 << 17,
        CONNECT = 1 << 18,
        CONNECT_FINALIZE = 1 << 19,
        NET_ERROR = 1 << 20,
        NET_ERROR_DISCONNECT = 1 << 21,
        ICMD_COMMAND = 1 << 22,
        TIME_SYNC = 1 << 23,
        ECHO_REQUEST = 1 << 24,
        ECHO_RESPONSE = 1 << 25,
        FLOW = 1 << 26,
    }

    public uint seqId;
    public Flag flags;
    public uint checksum;
    public ushort recipientId;
    public ushort interval;
    public ushort dataLength;
    public ushort iteration;

    public ProtocolHeader() {

    }

    public ProtocolHeader(BinaryReader data) {
        seqId = data.ReadUInt32();
        flags = (Flag)data.ReadUInt32();
        checksum = data.ReadUInt32();
        recipientId = data.ReadUInt16();
        interval = data.ReadUInt16();
        dataLength = data.ReadUInt16();
        iteration = data.ReadUInt16();
    }

    public void write(PacketWriter writer, ref uint checksum) {
        BinaryWriter data = writer.data;
        long dataStart = data.BaseStream.Position;

        data.Write(seqId);
        data.Write((uint)flags);
        data.Write(0xBADD70DD);
        data.Write(recipientId);
        data.Write(interval);
        data.Write(dataLength);
        data.Write(iteration);

        checksum += CryptoUtil.calcChecksum(writer.rawData, dataStart, data.BaseStream.Position - dataStart, true);
    }
}
