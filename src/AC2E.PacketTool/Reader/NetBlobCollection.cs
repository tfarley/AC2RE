using AC2E.Def;
using System.Collections.Generic;

namespace AC2E.PacketTool {

    public class NetBlobCollection {

        public readonly List<NetBlobRecord> records = new List<NetBlobRecord>();
        private readonly Dictionary<NetBlobId, NetBlobRecord> netBlobIdToRecord = new Dictionary<NetBlobId, NetBlobRecord>();

        public void addPacket(NetPacket packet, bool isClientToServer, int packetNum, float timestamp) {
            foreach (NetBlobFrag frag in packet.frags) {
                if (!netBlobIdToRecord.TryGetValue(frag.blobId, out NetBlobRecord netBlobRecord)) {
                    netBlobRecord = new NetBlobRecord {
                        isClientToServer = isClientToServer,
                        startPacketNum = packetNum,
                        startTimestamp = timestamp,
                        netBlob = new NetBlob {
                            blobId = frag.blobId,
                            fragCount = frag.fragCount,
                            queueId = frag.queueId,
                        },
                    };
                    netBlobIdToRecord[frag.blobId] = netBlobRecord;
                    records.Add(netBlobRecord);
                }

                netBlobRecord.netBlob.addFragment(frag);

                if (netBlobRecord.netBlob.payload != null) {
                    netBlobRecord.endPacketNum = packetNum;
                    netBlobRecord.endTimestamp = timestamp;
                }
            }
        }
    }
}
