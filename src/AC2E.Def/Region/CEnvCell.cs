using System;

namespace AC2E.Def {

    public class CEnvCell {

        // Const ENVCELL_PACK_*
        [Flags]
        public enum PackFlag : ulong {
            NONE = 0,
            SEEN_OUTSIDE = 1 << 0, // 0x00000001
            HAS_ENTITIES = 1 << 1, // 0x00000002
            HAS_PROPERTIES = 1 << 2, // 0x00000004
            NEVER_CULL = 1 << 3, // 0x00000008
            DRAW_SKY = 1 << 4, // 0x00000010
        }
    }
}
