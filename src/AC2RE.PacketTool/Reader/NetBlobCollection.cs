using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.PacketTool {

    internal class NetBlobCollection {

        public readonly List<NetBlobRecord> records = new();
        private readonly Dictionary<NetBlobId, NetBlobRecord> netBlobIdToRecord = new();

        public void addPacket(NetPacket packet, bool isClientToServer, int packetNum, double timestamp) {
            foreach (NetBlobFrag frag in packet.frags) {
                if (!netBlobIdToRecord.TryGetValue(frag.blobId, out NetBlobRecord? netBlobRecord)) {
                    netBlobRecord = new(new(frag)) {
                        isClientToServer = isClientToServer,
                        startPacketNum = packetNum,
                        startTimestamp = timestamp,
                    };
                    netBlobIdToRecord[frag.blobId] = netBlobRecord;
                    records.Add(netBlobRecord);
                } else {
                    netBlobRecord.netBlob.addFragment(frag);
                }

                if (netBlobRecord.netBlob.payload != null) {
                    netBlobRecord.endPacketNum = packetNum;
                    netBlobRecord.endTimestamp = timestamp;
                }
            }
        }
    }
}
