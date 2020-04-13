using System;
using System.Collections.Generic;
using System.IO;

class Packet {

    [Flags]
    public enum Flag : uint {
        NONE = 0,

        // Flags
        RETRANSMITTING = 1 << 0, // 0x00000001
        ENCRYPTED_CHECKSUM = 1 << 1, // 0x00000002
        FRAGMENTS = 1 << 2, // 0x00000004

        // Optional headers, see COptionalHeaderAllocatorTemplate
        SERVER_SWITCH = 1 << 8, // 0x00000100
        LOGON_SERVER_ADDR = 1 << 9, // 0x00000200
        UNK_1 = 1 << 10, // 0x00000400 // TODO: Response to server switch?
        REFERRAL = 1 << 11, // 0x00000800
        NACKS = 1 << 12, // 0x00001000
        NO_RETRANSMIT = 1 << 13, // 0x00002000
        ACK = 1 << 14, // 0x00004000
        DISCONNECT = 1 << 15, // 0x00008000
        LOGON_REQUEST = 1 << 16, // 0x00010000
        WORLD_LOGON_REQUEST = 1 << 17, // 0x00020000
        CONNECT = 1 << 18, // 0x00040000
        CONNECT_FINALIZE = 1 << 19, // 0x00080000
        NET_ERROR = 1 << 20, // 0x00100000
        NET_ERROR_DISCONNECT = 1 << 21, // 0x00200000
        ICMD_COMMAND = 1 << 22, // 0x00400000
        TIME_SYNC = 1 << 23, // 0x00800000
        ECHO_REQUEST = 1 << 24, // 0x01000000
        ECHO_RESPONSE = 1 << 25, // 0x02000000
        FLOW = 1 << 26, // 0x04000000
    }

    public uint seq;
    public Flag flags;
    public uint checksum;
    public ushort recipientId;
    public ushort interval;
    public ushort dataLength;
    public ushort iteration;

    private uint _ackHeader;
    public uint ackHeader {
        get => _ackHeader;
        set {
            _ackHeader = value;
            flags |= Flag.ACK;
        }
    }

    private LogonHeader _logonHeader;
    public LogonHeader logonHeader {
        get => _logonHeader;
        set {
            _logonHeader = value;
            flags |= Flag.LOGON_REQUEST;
        }
    }

    private ConnectHeader _connectHeader;
    public ConnectHeader connectHeader {
        get => _connectHeader;
        set {
            _connectHeader = value;
            flags |= Flag.CONNECT;
        }
    }

    private ulong _connectFinalizeHeader;
    public ulong connectFinalizeHeader {
        get => _connectFinalizeHeader;
        set {
            _connectFinalizeHeader = value;
            flags |= Flag.CONNECT_FINALIZE;
        }
    }

    private EchoRequestHeader _echoRequestHeader;
    public EchoRequestHeader echoRequestHeader {
        get => _echoRequestHeader;
        set {
            _echoRequestHeader = value;
            flags |= Flag.ECHO_REQUEST;
        }
    }

    private EchoResponseHeader _echoResponseHeader;
    public EchoResponseHeader echoResponseHeader {
        get => _echoResponseHeader;
        set {
            _echoResponseHeader = value;
            flags |= Flag.ECHO_RESPONSE;
        }
    }

    public readonly List<NetBlobFrag> frags = new List<NetBlobFrag>();

    public uint isaacXor;
    public bool hasIsaacXor;

    public Packet() {

    }

    public Packet(BinaryReader data) {
        seq = data.ReadUInt32();
        flags = (Flag)data.ReadUInt32();
        checksum = data.ReadUInt32();
        recipientId = data.ReadUInt16();
        interval = data.ReadUInt16();
        dataLength = data.ReadUInt16();
        iteration = data.ReadUInt16();

        if (flags.HasFlag(Flag.SERVER_SWITCH)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.LOGON_SERVER_ADDR)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.UNK_1)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.REFERRAL)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.NACKS)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.NO_RETRANSMIT)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.ACK)) {
            _ackHeader = data.ReadUInt32();
        }
        if (flags.HasFlag(Flag.LOGON_REQUEST)) {
            _logonHeader = new LogonHeader(data);
        }
        if (flags.HasFlag(Flag.WORLD_LOGON_REQUEST)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.CONNECT)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.CONNECT_FINALIZE)) {
            _connectFinalizeHeader = data.ReadUInt64();
        }
        if (flags.HasFlag(Flag.NET_ERROR)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.NET_ERROR_DISCONNECT)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.ICMD_COMMAND)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.TIME_SYNC)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.ECHO_REQUEST)) {
            _echoRequestHeader = new EchoRequestHeader(data);
        }
        if (flags.HasFlag(Flag.ECHO_RESPONSE)) {
            // TODO: Anything to do here?
        }
        if (flags.HasFlag(Flag.FLOW)) {
            throw new NotImplementedException();
        }

        if (flags.HasFlag(Flag.FRAGMENTS)) {
            while (data.BaseStream.Position < data.BaseStream.Length) {
                frags.Add(new NetBlobFrag(data));
            }
        }
    }
    public void writeHeader(BinaryWriter data) {
        data.Write(seq);
        data.Write((uint)flags);
        data.Write(0xBADD70DD); // checksum, replaced after data written
        data.Write(recipientId);
        data.Write(interval);
        data.Write((ushort)0); // dataLength, replaced after data written
        data.Write(iteration);
    }

    public void writeOptionalHeaders(BinaryWriter data, byte[] rawData, ref uint checksum) {
        if (flags.HasFlag(Flag.SERVER_SWITCH)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.LOGON_SERVER_ADDR)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.UNK_1)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.REFERRAL)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.NACKS)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.NO_RETRANSMIT)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.ACK)) {
            long dataStart = data.BaseStream.Position;
            data.Write(_ackHeader);
            checksum += CryptoUtil.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.LOGON_REQUEST)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.WORLD_LOGON_REQUEST)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.WORLD_LOGON_REQUEST)) {
            throw new NotImplementedException();
        }
        if (_connectHeader != null) {
            long dataStart = data.BaseStream.Position;
            _connectHeader.write(data);
            checksum += CryptoUtil.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.CONNECT_FINALIZE)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.NET_ERROR)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.NET_ERROR_DISCONNECT)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.ICMD_COMMAND)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.TIME_SYNC)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.ECHO_REQUEST)) {
            throw new NotImplementedException();
        }
        if (_echoResponseHeader != null) {
            long dataStart = data.BaseStream.Position;
            _echoResponseHeader.write(data);
            checksum += CryptoUtil.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.FLOW)) {
            throw new NotImplementedException();
        }
    }

    public override string ToString() {
        return $"s {seq} f {flags} c {checksum:X8} r {recipientId} g {interval} l {dataLength} i {iteration}";
    }
}
