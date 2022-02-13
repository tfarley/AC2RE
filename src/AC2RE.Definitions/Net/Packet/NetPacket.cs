using System;
using System.Collections.Generic;
using System.IO;

namespace AC2RE.Definitions;

public class NetPacket {

    public static readonly int MAX_SIZE = 484;

    [Flags]
    public enum Flag : uint {
        NONE = 0,
        // Flags
        RETRANSMITTING = 1 << 0, // 0x00000001
        ENCRYPTED_CHECKSUM = 1 << 1, // 0x00000002
        FRAGMENTS = 1 << 2, // 0x00000004

        // Optional headers, see COptionalHeaderAllocatorTemplate
        SERVER_SWITCH = 1 << 8, // 0x00000100
        LOGON_ROUTE = 1 << 9, // 0x00000200
        UNK_1 = 1 << 10, // 0x00000400 // TODO: Response to server switch?
        REFERRAL = 1 << 11, // 0x00000800
        NAK = 1 << 12, // 0x00001000
        EMPTY_ACK = 1 << 13, // 0x00002000
        PAK = 1 << 14, // 0x00004000
        LOGOFF = 1 << 15, // 0x00008000
        LOGON = 1 << 16, // 0x00010000
        WORLD_LOGON = 1 << 17, // 0x00020000
        CONNECT = 1 << 18, // 0x00040000
        CONNECT_ACK = 1 << 19, // 0x00080000
        CONNECTION_ERROR = 1 << 20, // 0x00100000
        DISCONNECT = 1 << 21, // 0x00200000
        ICMD_COMMAND = 1 << 22, // 0x00400000
        TIME_SYNC = 1 << 24, // 0x01000000
        ECHO_REQUEST = 1 << 25, // 0x02000000
        ECHO_RESPONSE = 1 << 26, // 0x04000000
        FLOW = 1 << 27, // 0x08000000
    }

    // ProtoHeader
    public uint seq; // seqID_
    public Flag flags; // header_
    public uint checksum; // checksum_
    public ushort recipientId; // recID_
    public ushort interval; // interval_
    public ushort dataLength; // datalen_
    public ushort iteration; // iteration_

    private ServerSwitchHeader _serverSwitchHeader;
    public ServerSwitchHeader serverSwitchHeader {
        get => _serverSwitchHeader;
        set {
            _serverSwitchHeader = value;
            flags |= Flag.SERVER_SWITCH;
        }
    }

    private List<uint> _nacksHeader;
    public List<uint> nacksHeader {
        get => _nacksHeader;
        set {
            _nacksHeader = value;
            flags |= Flag.NAK;
        }
    }

    private List<uint> _emptyAckHeader;
    public List<uint> emptyAckHeader {
        get => _emptyAckHeader;
        set {
            _emptyAckHeader = value;
            flags |= Flag.EMPTY_ACK;
        }
    }

    private uint _ackHeader;
    public uint ackHeader {
        get => _ackHeader;
        set {
            _ackHeader = value;
            flags |= Flag.PAK;
        }
    }

    private LogonHeader _logonHeader;
    public LogonHeader logonHeader {
        get => _logonHeader;
        set {
            _logonHeader = value;
            flags |= Flag.LOGON;
        }
    }

    private ulong _worldLogonHeader;
    public ulong worldLogonHeader {
        get => _worldLogonHeader;
        set {
            _worldLogonHeader = value;
            flags |= Flag.WORLD_LOGON;
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

    private ulong _connectAckHeader;
    public ulong connectAckHeader {
        get => _connectAckHeader;
        set {
            _connectAckHeader = value;
            flags |= Flag.CONNECT_ACK;
        }
    }

    private NetError _connectionErrorHeader;
    public NetError connectionErrorHeader {
        get => _connectionErrorHeader;
        set {
            _connectionErrorHeader = value;
            flags |= Flag.CONNECTION_ERROR;
        }
    }

    private NetError _disconnectErrorHeader;
    public NetError disconnectErrorHeader {
        get => _disconnectErrorHeader;
        set {
            _disconnectErrorHeader = value;
            flags |= Flag.DISCONNECT;
        }
    }

    private ICMDCommandHeader _icmdCommandHeader;
    public ICMDCommandHeader icmdCommandHeader {
        get => _icmdCommandHeader;
        set {
            _icmdCommandHeader = value;
            flags |= Flag.ICMD_COMMAND;
        }
    }

    private double _timeSyncHeader;
    public double timeSyncHeader {
        get => _timeSyncHeader;
        set {
            _timeSyncHeader = value;
            flags |= Flag.TIME_SYNC;
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

    private FlowHeader _flowHeader;
    public FlowHeader flowHeader {
        get => _flowHeader;
        set {
            _flowHeader = value;
            flags |= Flag.FLOW;
        }
    }

    public readonly List<NetBlobFrag> frags = new();

    public uint isaacXor;
    public bool hasIsaacXor;

    public NetPacket() {

    }

    public NetPacket(AC2Reader data) {
        seq = data.ReadUInt32();
        flags = data.ReadEnum<Flag>();
        checksum = data.ReadUInt32();
        recipientId = data.ReadUInt16();
        interval = data.ReadUInt16();
        dataLength = data.ReadUInt16();
        iteration = data.ReadUInt16();

        if (flags.HasFlag(Flag.SERVER_SWITCH)) {
            _serverSwitchHeader = new(data);
        }
        if (flags.HasFlag(Flag.LOGON_ROUTE)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.REFERRAL)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.NAK)) {
            _nacksHeader = data.ReadList(data.ReadUInt32);
        }
        if (flags.HasFlag(Flag.EMPTY_ACK)) {
            _emptyAckHeader = data.ReadList(data.ReadUInt32);
        }
        if (flags.HasFlag(Flag.PAK)) {
            _ackHeader = data.ReadUInt32();
        }
        if (flags.HasFlag(Flag.LOGON)) {
            _logonHeader = new(data);
        }
        if (flags.HasFlag(Flag.WORLD_LOGON)) {
            _worldLogonHeader = data.ReadUInt64();
        }
        if (flags.HasFlag(Flag.CONNECT)) {
            _connectHeader = new(data);
        }
        if (flags.HasFlag(Flag.CONNECT_ACK)) {
            _connectAckHeader = data.ReadUInt64();
        }
        if (flags.HasFlag(Flag.CONNECTION_ERROR)) {
            _connectionErrorHeader = new(data);
        }
        if (flags.HasFlag(Flag.DISCONNECT)) {
            _disconnectErrorHeader = new(data);
        }
        if (flags.HasFlag(Flag.ICMD_COMMAND)) {
            _icmdCommandHeader = new(data);
        }
        if (flags.HasFlag(Flag.TIME_SYNC)) {
            _timeSyncHeader = data.ReadDouble();
        }
        if (flags.HasFlag(Flag.ECHO_REQUEST)) {
            _echoRequestHeader = new(data);
        }
        if (flags.HasFlag(Flag.ECHO_RESPONSE)) {
            _echoResponseHeader = new(data);
        }
        if (flags.HasFlag(Flag.FLOW)) {
            _flowHeader = new(data);
        }

        if (flags.HasFlag(Flag.FRAGMENTS)) {
            while (data.BaseStream.Position < data.BaseStream.Length) {
                long fragStart = data.BaseStream.Position;
                NetBlobFrag frag = new(data);
                if (data.BaseStream.Position != fragStart + frag.fragSize) {
                    throw new InvalidDataException("Did not read full fragment!");
                }

                frags.Add(frag);
            }
        }
    }
    public void writeHeader(AC2Writer data) {
        data.Write(seq);
        data.WriteEnum(flags);
        data.Write(0xBADD70DD); // checksum, replaced after data written
        data.Write(recipientId);
        data.Write(interval);
        data.Write((ushort)0); // dataLength, replaced after data written
        data.Write(iteration);
    }

    public void writeOptionalHeaders(AC2Writer data, byte[] rawData, ref uint checksum) {
        if (flags.HasFlag(Flag.SERVER_SWITCH)) {
            long dataStart = data.BaseStream.Position;
            _serverSwitchHeader.write(data);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.LOGON_ROUTE)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.REFERRAL)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.NAK)) {
            long dataStart = data.BaseStream.Position;
            data.Write(_nacksHeader, data.Write);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.EMPTY_ACK)) {
            long dataStart = data.BaseStream.Position;
            data.Write(_emptyAckHeader, data.Write);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.PAK)) {
            long dataStart = data.BaseStream.Position;
            data.Write(_ackHeader);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.LOGON)) {
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.WORLD_LOGON)) {
            long dataStart = data.BaseStream.Position;
            data.Write(_worldLogonHeader);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (_connectHeader != null) {
            long dataStart = data.BaseStream.Position;
            _connectHeader.write(data);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.CONNECT_ACK)) {
            long dataStart = data.BaseStream.Position;
            data.Write(_connectAckHeader);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.CONNECTION_ERROR)) {
            long dataStart = data.BaseStream.Position;
            _connectionErrorHeader.write(data);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.DISCONNECT)) {
            long dataStart = data.BaseStream.Position;
            _disconnectErrorHeader.write(data);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.ICMD_COMMAND)) {
            long dataStart = data.BaseStream.Position;
            _icmdCommandHeader.write(data);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.TIME_SYNC)) {
            long dataStart = data.BaseStream.Position;
            data.Write(_timeSyncHeader);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (flags.HasFlag(Flag.ECHO_REQUEST)) {
            throw new NotImplementedException();
        }
        if (_echoResponseHeader != null) {
            long dataStart = data.BaseStream.Position;
            _echoResponseHeader.write(data);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
        if (_flowHeader != null) {
            long dataStart = data.BaseStream.Position;
            _flowHeader.write(data);
            checksum += AC2Crypto.calcChecksum(rawData, dataStart, data.BaseStream.Position - dataStart, true);
        }
    }

    public override string ToString() {
        return $"s {seq} f {flags} c {checksum:X8} r {recipientId} g {interval} l {dataLength} i {iteration}";
    }
}
