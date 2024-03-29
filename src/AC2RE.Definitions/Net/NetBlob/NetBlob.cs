﻿using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class NetBlob {

    public NetBlobId blobId; // savedNetBlobID_
    public ushort fragCount; // numFragments_
    public NetQueue queueId; // queueID_
    public byte[] payload; // buf_

    public readonly SortedDictionary<ushort, NetBlobFrag> frags = new();

    public NetBlob() {

    }

    public NetBlob(NetBlobFrag frag) {
        blobId = frag.blobId;
        fragCount = frag.fragCount;
        queueId = frag.queueId;
        addFragment(frag);
    }

    // TODO: Use/test this frag combining for receiving
    public void addFragment(NetBlobFrag newFrag) {
        if (!frags.ContainsKey(newFrag.fragIndex)) {
            frags.Add(newFrag.fragIndex, newFrag);
            if (frags.Count == fragCount) {
                int blobSize = 0;
                foreach (NetBlobFrag frag in frags.Values) {
                    blobSize += frag.payload.Length;
                }
                payload = new byte[blobSize];
                int payloadOffset = 0;
                foreach (NetBlobFrag frag in frags.Values) {
                    Array.Copy(frag.payload, 0, payload, payloadOffset, frag.payload.Length);
                    payloadOffset += frag.payload.Length;
                }
            }
        }
    }

    public void fragmentize() {
        if (payload.Length < NetBlobFrag.MAX_SIZE) {
            fragCount = 1;
            frags.Add(0, new() {
                blobId = blobId,
                fragCount = 1,
                fragIndex = 0,
                queueId = queueId,
                payload = payload,
            });
        } else {
            int remainingLength = payload.Length;
            fragCount = (ushort)((payload.Length + NetBlobFrag.MAX_SIZE - 1) / NetBlobFrag.MAX_SIZE);
            ushort fragIndex = 0;
            while (remainingLength > 0) {
                NetBlobFrag frag = new() {
                    blobId = blobId,
                    fragCount = fragCount,
                    fragIndex = (ushort)frags.Count,
                    queueId = queueId,
                    payload = new byte[Math.Min(remainingLength, NetBlobFrag.MAX_SIZE)],
                };
                Buffer.BlockCopy(payload, payload.Length - remainingLength, frag.payload, 0, frag.payload.Length);
                frags.Add(fragIndex, frag);
                remainingLength -= frag.payload.Length;
                fragIndex++;
            }
        }
    }
}
