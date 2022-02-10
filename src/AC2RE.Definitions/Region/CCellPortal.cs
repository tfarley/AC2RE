using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CCellPortal {

    // Const CELLPORTAL_PACK_*
    [Flags]
    public enum PackFlag : ushort {
        NONE = 0,
        EXACT_MATCH = 1 << 0, // CELLPORTAL_PACK_EXACT_MATCH 0x00000001
        NO_OTHER_CELL_ID = 1 << 1, // CELLPORTAL_PACK_NO_OTHER_CELL_ID 0x00000002
        STAB_LIST = 1 << 2, // CELLPORTAL_PACK_STAB_LIST 0x00000004
    }

    // CCellPortal
    public PackFlag packFlags;
    public ushort portalIndex; // portal_index
    public LocalCellId otherCellId; // other_cell_id
    public ushort otherPortalIndex; // other_portal_index
    public List<LocalCellId> stabList; // num_stabs + stab_list

    public CCellPortal(AC2Reader data) {
        packFlags = (PackFlag)data.ReadUInt16();
        portalIndex = data.ReadUInt16();
        otherCellId = data.ReadLocalCellId();
        otherPortalIndex = data.ReadUInt16();
        if (packFlags.HasFlag(PackFlag.STAB_LIST)) {
            stabList = data.ReadList(data.ReadLocalCellId, 2);
            data.Align(4);
        }
    }
}
