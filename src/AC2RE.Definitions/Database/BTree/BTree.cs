using System.Collections.Generic;
using System.IO;

namespace AC2RE.Definitions;

public class BTree {

    public readonly Dictionary<uint, BTNode> offsetToNode = new();

    public BTree(DatReader datReader) {
        Queue<uint> nodeOffsetQueue = new();
        HashSet<uint> visitedOffsets = new();

        int nodeChildrenSize = sizeof(uint) * BTNode.MAX_NUM_CHILDREN;

        nodeOffsetQueue.Enqueue(datReader.header.fileInfo.treeRootOffset);
        while (nodeOffsetQueue.TryDequeue(out uint nodeOffset)) {
            try {
                datReader.data.BaseStream.Seek(nodeOffset + sizeof(uint) + nodeChildrenSize, SeekOrigin.Begin);
                uint numEntries = datReader.data.ReadUInt32();
                uint numChildren = numEntries + 1;
                int nodeFileSize = nodeChildrenSize + sizeof(uint) + BTEntry.SIZE * (int)numEntries;
                using (AC2Reader fileData = datReader.getFileReaderRaw(nodeOffset, nodeFileSize)) {
                    BTNode node = new(fileData, numChildren, numEntries);
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
