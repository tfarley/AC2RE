using System.Collections.Generic;
using System.IO;

namespace AC2RE.Definitions {

    public class BTree {

        public readonly Dictionary<uint, BTNode> offsetToNode = new();

        public BTree(DatReader datReader) {
            Queue<uint> nodeOffsetQueue = new();
            HashSet<uint> visitedOffsets = new();

            nodeOffsetQueue.Enqueue(datReader.header.fileInfo.treeRootOffset);
            while (nodeOffsetQueue.TryDequeue(out uint nodeOffset)) {
                try {
                    using (AC2Reader fileData = datReader.getFileReaderRaw(nodeOffset, BTNode.FILE_SIZE)) {
                        BTNode node = new(fileData);
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
