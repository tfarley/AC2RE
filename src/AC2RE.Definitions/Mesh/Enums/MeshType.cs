namespace AC2RE.Definitions;

// Dat file 2300001E / Const - *_MESHTID
public enum MeshType : uint {
    Invalid = 0, // _ / Invalid_MESHTID
    All = 1, // _ / All_MESHTID
    Default = 2, // _ / Default_MESHTID

    Helmet = 0x41000001, // Helmet

    Feet = 0x81000001, // Feet
    Hands = 0x81000002, // Hands
    Crest = 0x81000003, // Crest
    Beard = 0x81000004, // Beard

    Head = 0x8100001B, // Head
    UpperBody = 0x8100001C, // UpperBody
    LowerBody = 0x8100001D, // LowerBody

    Tail = 0x81000024, // Tail
    Cape = 0x81000025, // Cape
    Totem = 0x81000026, // Totem
}
