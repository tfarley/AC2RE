using System.Collections.Generic;
using System.IO;

namespace AC2RE.Definitions {

    public class BTNode {

        private static readonly int MAX_NUM_CHILDREN = 62;
        private static readonly int MAX_NUM_ENTRIES = 61;
        public static readonly int FILE_SIZE = sizeof(uint) * MAX_NUM_CHILDREN + sizeof(uint) + BTEntry.FILE_SIZE * MAX_NUM_ENTRIES;

        public List<uint> childOffsets = new(); // NextNode_
        public List<BTEntry> entries = new(); // NumEntries_ + Entry_

        public BTNode(AC2Reader data) {
            for (int i = 0; i < MAX_NUM_CHILDREN; i++) {
                uint childOffset = data.ReadUInt32();
                if (childOffset == 0 || (childOffset & DatReader.BLOCK_FREE_FLAG) != 0) {
                    data.BaseStream.Seek((MAX_NUM_CHILDREN - i - 1) * 4, SeekOrigin.Current);
                    break;
                }
                childOffsets.Add(childOffset);
            }

            uint numEntries = data.ReadUInt32();

            for (int i = 0; i < numEntries; i++) {
                entries.Add(new(data));
            }
        }
    }
}
