using AC2RE.Definitions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AC2RE.Server {

    internal class NetBlobQueue {

        private readonly Dictionary<NetBlobId, NetBlob> partialBlobs = new();
        private readonly Queue<NetBlob> completeUnorderedBlobs = new();
        // TODO: Use PriorityQueue instead of sorting a list
        private readonly List<NetBlob> completePrivateOrderedBlobs = new();
        private bool privateOrderedBlobsSorted;
        private uint nextStamp = 1;

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
            OrderingType orderingType = blob.blobId.orderingType;
            switch (orderingType) {
                case OrderingType.UNORDERED:
                    completeUnorderedBlobs.Enqueue(blob);
                    break;
                case OrderingType.VISUAL_ORDERED:
                    Logs.NET.warn("Visual ordering of blobs from client not implemented, treating as unordered");
                    completeUnorderedBlobs.Enqueue(blob);
                    break;
                case OrderingType.PRIVATE_ORDERED:
                    completePrivateOrderedBlobs.Add(blob);
                    privateOrderedBlobsSorted = false;
                    break;
                default:
                    throw new NotImplementedException(orderingType.ToString());
            }
        }

        public bool TryDequeue([MaybeNullWhen(false)] out NetBlob result) {
            // TODO: May need to do something if queue gets blocked for too long
            if (completeUnorderedBlobs.TryDequeue(out result)) {
                return true;
            }
            if (!privateOrderedBlobsSorted) {
                completePrivateOrderedBlobs.Sort((NetBlob a, NetBlob b) => b.blobId.orderingStamp.CompareTo(a.blobId.orderingStamp));
                privateOrderedBlobsSorted = true;
            }
            if (completePrivateOrderedBlobs.Count > 0) {
                int lastIndex = completePrivateOrderedBlobs.Count - 1;
                NetBlob nextBlob = completePrivateOrderedBlobs[lastIndex];
                if (nextBlob.blobId.orderingStamp == nextStamp) {
                    result = nextBlob;
                    completePrivateOrderedBlobs.RemoveAt(lastIndex);
                    nextStamp++;
                    return true;
                }
            }
            return false;
        }
    }
}
