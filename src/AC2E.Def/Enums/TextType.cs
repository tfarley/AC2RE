using System;

namespace AC2E.Def.Enums {

    // Dat file 230000A5 + 230000BB
    [Flags]
    public enum TextType : uint {
        UNDEF = 0,
        ERROR = 1 << 0,
        COMBAT = 1 << 1,
        ADMIN = 1 << 2,
        STANDARD = 1 << 3,
        SAY = 1 << 4,
        TELL = 1 << 5,
        EMOTE = 1 << 6,
        LOG = 1 << 7,
        BROADCAST = 1 << 8,
        FELLOWSHIP = 1 << 16,
        ALLEGIANCE = 1 << 17,
        CHAT_ENTRY = 1 << 18,
        GENERAL = 1 << 19,
        TRADE = 1 << 20,
        REGION = 1 << 21,
        FACTION = 1 << 22,
        DEVOTED = 1 << 23,
        PK = 1 << 24,
        ALL = 0xFFFFFFFF,
    }
}
