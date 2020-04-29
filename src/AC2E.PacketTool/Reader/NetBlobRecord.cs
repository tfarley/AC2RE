using AC2E.Protocol.NetBlob;

namespace AC2E.PacketTool.Reader {

    public class NetBlobRecord {

        public bool isClientToServer;
        public int startPacketNum;
        public float startTimestamp;
        public int endPacketNum;
        public float endTimestamp;
        public NetBlob netBlob;
    }
}
