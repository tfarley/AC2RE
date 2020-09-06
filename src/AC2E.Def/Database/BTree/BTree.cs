using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class BTree {

        public readonly Dictionary<uint, BTNode> offsetToNode = new Dictionary<uint, BTNode>();

        public BTree(DatReader datReader) {
            Queue<uint> nodeOffsetQueue = new Queue<uint>();
            HashSet<uint> visitedOffsets = new HashSet<uint>();

            nodeOffsetQueue.Enqueue(datReader.header.fileInfo.treeRootOffset);
            while (nodeOffsetQueue.TryDequeue(out uint nodeOffset)) {
                try {
                    using (AC2Reader fileData = datReader.getFileReaderRaw(nodeOffset, BTNode.FILE_SIZE)) {
                        BTNode node = new BTNode(fileData);
                        foreach (uint childOffset in node.childOffsets) {
                            if (visitedOffsets.Add(childOffset)) {
                                nodeOffsetQueue.Enqueue(childOffset);
                            }
                        }
                        offsetToNode.Add(nodeOffset, node);
                    }
                } catch (InvalidDataException e) {
                    // TODO: Log and/or track the bad node
                }
            }
        }
    }
}
