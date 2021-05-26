using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class CEnvCell {

        // Const ENVCELL_PACK_*
        [Flags]
        public enum PackFlag : ulong {
            NONE = 0,
            ALL = uint.MaxValue,

            SEEN_OUTSIDE = 1 << 0, // 0x00000001
            HAS_ENTITIES = 1 << 1, // 0x00000002
            HAS_PROPERTIES = 1 << 2, // 0x00000004
            NEVER_CULL = 1 << 3, // 0x00000008
            DRAW_SKY = 1 << 4, // 0x00000010
        }

        public PackFlag packFlags;
        public Position pos; // m_position
        public List<LocalCellId> stabList; // m_stabList
        public DataId environmentDid; // m_environmentDID
        public List<CCellPortal> portals; // m_portals
        public EntityGroupDesc entities; // m_entities
        public PropertyCollection properties; // m_properties
        public List<LocalCellId> sharedCells; // m_sharedCells

        public CEnvCell(AC2Reader data) {
            pos = new(data);
            packFlags = (PackFlag)data.ReadUInt32();
            environmentDid = data.ReadDataId();
            portals = data.ReadList(() => new CCellPortal(data));
            stabList = data.ReadList(data.ReadLocalCellId);
            data.Align(4);
            sharedCells = data.ReadList(data.ReadLocalCellId);
            data.Align(4);
            if (packFlags.HasFlag(PackFlag.HAS_ENTITIES)) {
                entities = new(data);
            }
            if (packFlags.HasFlag(PackFlag.HAS_PROPERTIES)) {
                properties = new(data);
            }
        }
    }
}
