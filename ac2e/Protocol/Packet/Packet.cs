using System;
using System.Collections.Generic;
using System.IO;
using static ProtocolHeader;

class Packet {

    public readonly ProtocolHeader header = new ProtocolHeader();

    private LogonHeader _logonHeader;
    public LogonHeader logonHeader {
        get => _logonHeader;
        set {
            _logonHeader = value;
            header.flags |= Flag.LOGON_REQUEST;
        }
    }

    private ConnectHeader _connectHeader;
    public ConnectHeader connectHeader {
        get => _connectHeader;
        set {
            _connectHeader = value;
            header.flags |= Flag.CONNECT;
        }
    }

    private ulong _connectFinalizeHeader;
    public ulong connectFinalizeHeader {
        get => _connectFinalizeHeader;
        set {
            _connectFinalizeHeader = value;
            header.flags |= Flag.CONNECT_FINALIZE;
        }
    }

    private EchoRequestHeader _echoRequestHeader;
    public EchoRequestHeader echoRequestHeader {
        get => _echoRequestHeader;
        set {
            _echoRequestHeader = value;
            header.flags |= Flag.ECHO_REQUEST;
        }
    }

    private EchoResponseHeader _echoResponseHeader;
    public EchoResponseHeader echoResponseHeader {
        get => _echoResponseHeader;
        set {
            _echoResponseHeader = value;
            header.flags |= Flag.ECHO_RESPONSE;
        }
    }

    public readonly List<Fragment> frags = new List<Fragment>();

    private uint isaacXor;
    private bool hasIsaacXor;

    public Packet() {

    }

    public Packet(BinaryReader data) {
        header = new ProtocolHeader(data);
        Flag flags = header.flags;

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
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.DISCONNECT)) {
            throw new NotImplementedException();
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
                frags.Add(new Fragment(data));
            }
        }
    }

    public int write(PacketWriter writer, ISAAC isaac) {
        long packetStart = writer.data.BaseStream.Position;

        if (frags.Count > 0) {
            // TODO: Just assuming that all fragments cause encryption for now - there might be cases where they don't need to or shouldn't
            header.flags |= Flag.ENCRYPTED_CHECKSUM;
            header.flags |= Flag.FRAGMENTS;
        }

        uint headerChecksum = 0;
        header.write(writer, ref headerChecksum);

        long dataStart = writer.data.BaseStream.Position;

        uint dataChecksum = 0;
        Flag flags = header.flags;
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
            throw new NotImplementedException();
        }
        if (flags.HasFlag(Flag.DISCONNECT)) {
            throw new NotImplementedException();
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
            _connectHeader.write(writer, ref dataChecksum);
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
            _echoResponseHeader.write(writer, ref dataChecksum);
        }
        if (flags.HasFlag(Flag.FLOW)) {
            throw new NotImplementedException();
        }

        if (flags.HasFlag(Flag.FRAGMENTS)) {
            foreach (Fragment frag in frags) {
                frag.write(writer, ref dataChecksum);
            }
        }

        if (flags.HasFlag(Flag.ENCRYPTED_CHECKSUM)) {
            if (!hasIsaacXor) {
                isaacXor = isaac.Next();
                hasIsaacXor = true;
            }
            dataChecksum ^= isaacXor;
        }

        long packetEnd = writer.data.BaseStream.Position;
        ushort contentLength = (ushort)(packetEnd - dataStart);

        // Replace the content length and also update the checksum
        BitConverter.GetBytes(contentLength).CopyTo(writer.rawData, 16);
        headerChecksum += contentLength;

        // Replace the checksum
        BitConverter.GetBytes(headerChecksum + dataChecksum).CopyTo(writer.rawData, 8);

        return (int)(packetEnd - packetStart);
    }
}
