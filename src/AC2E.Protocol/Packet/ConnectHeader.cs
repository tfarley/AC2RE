using System.IO;

namespace AC2E.Protocol {

    public class ConnectHeader {

        // CConnectHeader::__unnamed
        public double serverTime; // ServerTime
        public ulong connectionAckCookie; // qwCookie
        public uint netId; // NetID
        public uint outgoingSeed; // OutgoingSeed
        public uint incomingSeed; // IncomingSeed

        public void write(BinaryWriter data) {
            data.Write(serverTime);
            data.Write(connectionAckCookie);
            data.Write(netId);
            data.Write(outgoingSeed);
            data.Write(incomingSeed);
            // TODO: Unknown value - padding?
            data.Write((uint)0);
        }
    }
}
