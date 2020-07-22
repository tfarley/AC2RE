using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class BTree {

        public class BTNode {

            private static readonly int MAX_NUM_CHILDREN = 62;
            private static readonly int MAX_NUM_ENTRIES = 61;
            public static readonly int FILE_SIZE = sizeof(uint) * MAX_NUM_CHILDREN + sizeof(uint) + BTEntry.FILE_SIZE * MAX_NUM_ENTRIES;

            public List<uint> childOffsets = new List<uint>(); // NextNode_
            public List<BTEntry> entries = new List<BTEntry>(); // NumEntries_ + Entry_

            public BTNode(AC2Reader data) {
                for (int i = 0; i < MAX_NUM_CHILDREN; i++) {
                    uint childOffset = data.ReadUInt32();
                    if (childOffset != 0) {
                        childOffsets.Add(childOffset);
                    }
                }

                uint numEntries = data.ReadUInt32();

                for (int i = 0; i < numEntries; i++) {
                    entries.Add(new BTEntry(data));
                }
            }
        }

        public class BTEntry {

            public static readonly int FILE_SIZE = sizeof(uint) * 4;

            public uint gid; // GID_
            public uint offset; // Offset_
            public int size; // size_
            public uint date; // date_

            public BTEntry(AC2Reader data) {
                gid = data.ReadUInt32();
                offset = data.ReadUInt32();
                size = data.ReadInt32();
                date = data.ReadUInt32();
            }
        }

        public readonly Dictionary<uint, BTNode> offsetToNode = new Dictionary<uint, BTNode>();

        public BTree(DatReader datReader) {
            Queue<uint> nodeOffsetQueue = new Queue<uint>();
            HashSet<uint> visitedOffsets = new HashSet<uint>();

            nodeOffsetQueue.Enqueue(datReader.header.fileInfo.treeRootOffset);
            while (nodeOffsetQueue.TryDequeue(out uint nodeOffset)) {
                using (AC2Reader fileData = datReader.getFileReader(nodeOffset, BTNode.FILE_SIZE)) {
                    BTNode node = new BTNode(fileData);
                    foreach (uint childOffset in node.childOffsets) {
                        if (visitedOffsets.Add(childOffset)) {
                            nodeOffsetQueue.Enqueue(childOffset);
                        }
                    }
                    offsetToNode.Add(nodeOffset, node);
                }
            }
        }
    }
}
