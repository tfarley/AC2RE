using System.Collections.Generic;
using System.IO;

namespace AC2RE.Definitions;

public class BTNode {

    public static readonly int MAX_NUM_CHILDREN = 62;

    public readonly List<uint> childOffsets = new(); // NextNode_
    public readonly List<BTEntry> entries = new(); // NumEntries_ + Entry_

    public BTNode(AC2Reader data, uint numChildren, uint numEntries) {
        for (int i = 0; i < numChildren; i++) {
            uint childOffset = data.ReadUInt32();
            if (childOffset == 0 || (childOffset & DatReader.BLOCK_FREE_FLAG) != 0) {
                numChildren = (uint)i + 1;
                break;
            }
            childOffsets.Add(childOffset);
        }

        if (numChildren < MAX_NUM_CHILDREN) {
            data.BaseStream.Seek(sizeof(uint) * (MAX_NUM_CHILDREN - numChildren), SeekOrigin.Current);
        }

        // Skip num entries
        data.BaseStream.Seek(4, SeekOrigin.Current);

        entries.Capacity += (int)numEntries;
        for (int i = 0; i < numEntries; i++) {
            entries.Add(new(data));
        }
    }
}
