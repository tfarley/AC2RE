using System;
using System.Collections.Generic;

namespace AC2E.Protocol.NetBlob {

    public static class NetBlob {

        public static readonly byte ORDERING_TYPE_WEENIE = 0x01;
        public static readonly byte ORDERING_TYPE_UNK1 = 0x02;
        public static readonly byte ORDERING_TYPE_NORMAL = 0x03;

        public static List<NetBlobFrag> fragmentize(NetBlobId.Flag blobFlags, uint blobSeq, NetQueue queueId, byte[] payload) {
            List<NetBlobFrag> frags = new List<NetBlobFrag>();
            byte orderingType = (queueId == NetQueue.NET_QUEUE_WEENIE || queueId == NetQueue.NET_QUEUE_SECUREWEENIE) ? ORDERING_TYPE_WEENIE : ORDERING_TYPE_NORMAL;
            NetBlobId blobId = new NetBlobId(blobFlags, orderingType, 0, blobSeq);
            if (payload.Length < NetBlobFrag.MAX_SIZE) {
                frags.Add(new NetBlobFrag {
                    blobId = blobId,
                    fragCount = 1,
                    fragIndex = 0,
                    queueId = queueId,
                    payload = payload,
                });
            } else {
                int remainingLength = payload.Length;
                ushort numFrags = (ushort)((payload.Length + NetBlobFrag.MAX_SIZE - 1) / NetBlobFrag.MAX_SIZE);
                while (remainingLength > 0) {
                    NetBlobFrag frag = new NetBlobFrag {
                        blobId = blobId,
                        fragCount = numFrags,
                        fragIndex = (ushort)frags.Count,
                        queueId = queueId,
                        payload = new byte[Math.Min(remainingLength, NetBlobFrag.MAX_SIZE)],
                    };
                    Buffer.BlockCopy(payload, payload.Length - remainingLength, frag.payload, 0, frag.payload.Length);
                    frags.Add(frag);
                    remainingLength -= frag.payload.Length;
                }
            }
            return frags;
        }
    }
}
