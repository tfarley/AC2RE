﻿using AC2RE.Definitions;
using AC2RE.Utils;

namespace AC2RE.PacketTool;

internal class EthernetHeader {

    public byte[] dest;
    public byte[] source;
    public ushort proto;

    public EthernetHeader(AC2Reader data) {
        dest = data.ReadBytes(6);
        source = data.ReadBytes(6);
        proto = data.ReadUInt16();
    }
}

internal class IpAddress {

    public byte[] bytes;

    public IpAddress(AC2Reader data) {
        bytes = data.ReadBytes(4);
    }

    public override string ToString() {
        return bytes[0] + "." + bytes[1] + "." + bytes[2] + "." + bytes[3];
    }
}

internal class IpHeader {

    public byte verIhl;
    public byte tos;
    public ushort tLen;
    public ushort identification;
    public ushort flagsFo;
    public byte ttl;
    public byte proto;
    public ushort crc;
    public IpAddress sAddr;
    public IpAddress dAddr;

    public IpHeader(AC2Reader data) {
        verIhl = data.ReadByte();
        tos = data.ReadByte();
        tLen = data.ReadUInt16();
        identification = data.ReadUInt16();
        flagsFo = data.ReadUInt16();
        ttl = data.ReadByte();
        proto = data.ReadByte();
        crc = data.ReadUInt16();
        sAddr = new(data);
        dAddr = new(data);
    }
}

internal class UdpHeader {

    public ushort sPort;
    public ushort dPort;
    public ushort len;
    public ushort crc;

    public UdpHeader(AC2Reader data) {
        sPort = Util.byteSwapped(data.ReadUInt16());
        dPort = Util.byteSwapped(data.ReadUInt16());
        len = Util.byteSwapped(data.ReadUInt16());
        crc = Util.byteSwapped(data.ReadUInt16());
    }
}
