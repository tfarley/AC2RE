using System;
using System.Collections.Generic;

public static class NetBlob {

    // TODO: Better number for max frag size?
    public static readonly int MAX_FRAG_SIZE = 450;

    // TODO: What's this? Should not be a constant?
    public static readonly uint BLOB_ID = 450;

    public static List<NetBlobFrag> fragmentize(uint blobSeq, NetQueue queueId, byte[] payload) {
        List<NetBlobFrag> frags = new List<NetBlobFrag>();
        if (payload.Length < MAX_FRAG_SIZE) {
            frags.Add(new NetBlobFrag {
                blobSeq = blobSeq,
                blobId = BLOB_ID,
                numFrags = 1,
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
                    blobId = BLOB_ID,
                    numFrags = numFrags,
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
