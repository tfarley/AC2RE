using AC2RE.Definitions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AC2RE.Server;

internal class NetBlobQueue {

    private readonly Dictionary<NetBlobId, NetBlob> partialBlobs = new();
    private readonly PriorityQueue<NetBlob, ulong> completeBlobs = new();
    private uint nextSeq;

    public void addFragment(NetBlobFrag frag) {
        // TODO: Need to track processed blob ids and discard if duplicate
        if (frag.fragCount == 1) {
            finishBlob(new(frag));
        } else {
            if (!partialBlobs.TryGetValue(frag.blobId, out NetBlob? blob)) {
                blob = new(frag);
                partialBlobs[frag.blobId] = blob;
            } else {
                blob.addFragment(frag);
            }

            if (blob.payload != null) {
                finishBlob(blob);
                partialBlobs.Remove(blob.blobId);
            }
        }
    }

    private void finishBlob(NetBlob blob) {
        // TODO: May need to do something with orderingStamp and orderingType, for now just requiring all to be ordered
        completeBlobs.Enqueue(blob, blob.blobId.sequenceId);
    }

    public bool TryDequeue([NotNullWhen(true)] out NetBlob? result) {
        // TODO: May need to do something if queue gets blocked for too long
        if (completeBlobs.Count > 0) {
            NetBlob nextBlob = completeBlobs.Peek();
            if (nextBlob.blobId.sequenceId == nextSeq) {
                result = nextBlob;
                completeBlobs.Dequeue();
                nextSeq++;
                return true;
            }
        }
        result = null;
        return false;
    }
}
