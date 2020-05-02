using System;

namespace AC2E.Def.Enums {

    // Const *_TextType
    // Dat file 230000A5 + 230000BB
    [Flags]
    public enum TextType : uint {
        UNDEF = 0,
        ERROR = 1 << 0, // 0x00000001
        COMBAT = 1 << 1, // 0x00000002
        ADMIN = 1 << 2, // 0x00000004
        STANDARD = 1 << 3, // 0x00000008
        SAY = 1 << 4, // 0x00000010
        TELL = 1 << 5, // 0x00000020
        EMOTE = 1 << 6, // 0x00000040
        LOG = 1 << 7, // 0x00000080
        BROADCAST = 1 << 8, // 0x00000100
        FELLOWSHIP = 1 << 16, // 0x00000200
        ALLEGIANCE = 1 << 17, // 0x00000400
        CHAT_ENTRY = 1 << 18, // 0x00000800
        GENERAL = 1 << 19, // 0x00001000
        TRADE = 1 << 20, // 0x00002000
        REGION = 1 << 21, // 0x00004000
        FACTION = 1 << 22, // 0x00008000
        DEVOTED = 1 << 23, // 0x00010000
        PK = 1 << 24, // 0x00020000
        ALL = uint.MaxValue, // 0xFFFFFFFF
    }
}
