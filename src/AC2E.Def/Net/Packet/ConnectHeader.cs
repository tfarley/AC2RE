namespace AC2E.Def {

    public class ConnectHeader {

        // CConnectHeader::__unnamed
        public double serverTime; // ServerTime
        public ulong connectionAckCookie; // qwCookie
        public uint netId; // NetID
        public uint outgoingSeed; // OutgoingSeed
        public uint incomingSeed; // IncomingSeed

        public void write(AC2Writer data) {
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
