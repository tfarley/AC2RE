using System;
using System.Collections.Generic;

public static class NetBlob {

    // TODO: Better number for max frag size?
    public static readonly int MAX_FRAG_SIZE = 450;

    public static readonly uint BLOB_ID_MASK_NORMAL = 0x23000000;
    public static readonly uint BLOB_ID_MASK_WEENIE = 0x01000000;

    public static List<NetBlobFrag> fragmentize(uint blobSeq, NetQueue queueId, byte[] payload) {
        List<NetBlobFrag> frags = new List<NetBlobFrag>();
        uint blobIdMask = (queueId == NetQueue.NET_QUEUE_WEENIE || queueId == NetQueue.NET_QUEUE_SECUREWEENIE) ? BLOB_ID_MASK_WEENIE : BLOB_ID_MASK_NORMAL;
        if (payload.Length < MAX_FRAG_SIZE) {
            frags.Add(new NetBlobFrag {
                blobSeq = blobSeq,
                blobId = blobIdMask | blobSeq,
                fragCount = 1,
                fragIndex = 0,
                queueId = queueId,
                payload = payload,
            });
        } else {
            int remainingLength = payload.Length;
            ushort numFrags = (ushort)((payload.Length + MAX_FRAG_SIZE - 1) / MAX_FRAG_SIZE);
            while (remainingLength > 0) {
                NetBlobFrag frag = new NetBlobFrag {
                    blobSeq = blobSeq,
                    blobId = blobIdMask | blobSeq,
                    fragCount = numFrags,
                    fragIndex = (ushort)frags.Count,
                    queueId = queueId,
                    payload = new byte[Math.Min(remainingLength, MAX_FRAG_SIZE)],
                };
                Buffer.BlockCopy(payload, payload.Length - remainingLength, frag.payload, 0, frag.payload.Length);
                frags.Add(frag);
                remainingLength -= frag.payload.Length;
            }
        }
        return frags;
    }
}
