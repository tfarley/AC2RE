using System;

namespace AC2RE.Definitions;

// Dat file 230000A5 + 230000BB / Const *_TextType
[Flags]
public enum TextType : uint {
    Undef = 0, // _ / Undef_TextType
    Error = 1 << 0, // Error / Error_TextType 0x00000001
    Combat = 1 << 1, // Combat / Combat_TextType 0x00000002
    Admin = 1 << 2, // Admin / Admin_TextType 0x00000004
    Standard = 1 << 3, // Standard / Standard_TextType 0x00000008
    Say = 1 << 4, // Say / Say_TextType 0x00000010
    Tell = 1 << 5, // Tell / Tell_TextType 0x00000020
    Emote = 1 << 6, // Emote / Emote_TextType 0x00000040
    Log = 1 << 7, // Log / Log_TextType 0x00000080
    Broadcast = 1 << 8, // Broadcast / Broadcast_TextType 0x00000100

    Fellowship = 1 << 16, // Fellowship / Fellowship_TextType 0x00010000
    Allegiance = 1 << 17, // Allegiance / Allegiance_TextType 0x00020000
    ChatEntry = 1 << 18, // ChatEntry / ChatEntry_TextType 0x00040000
    GeneralChannel = 1 << 19, // GeneralChannel / GeneralChannel_TextType 0x00080000
    TradeChannel = 1 << 20, // TradeChannel / TradeChannel_TextType 0x00100000
    RegionChannel = 1 << 21, // RegionChannel / RegionChannel_TextType 0x00200000
    FactionChannel = 1 << 22, // FactionChannel / FactionChannel_TextType 0x00400000
    Devoted = 1 << 23, // Devoted / _ 0x00800000
    PKChannel = 1 << 24, // PKChannel / _ 0x01000000

    All = uint.MaxValue, // All / All_TextType
}
