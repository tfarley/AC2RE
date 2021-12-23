using System;

namespace AC2RE.Definitions;

// Dat file 2300000A / Const *_PhysiqueType
[Flags]
public enum PhysiqueType : uint {
    Undef = 0, // _ / Undef_PhysiqueType
    Build = 1 << 0, // Build / Build_PhysiqueType 0x00000001
    Height = 1 << 1, // Height / Height_PhysiqueType 0x00000002
    SkinTone = 1 << 2, // SkinTone / SkinTone_PhysiqueType 0x00000004
    SkinDetail = 1 << 3, // SkinDetail / SkinDetail_PhysiqueType 0x00000008
    HeadDetail = 1 << 4, // HeadDetail / HeadDetail_PhysiqueType 0x00000010
    HeadFrill = 1 << 5, // HeadFrill / HeadFrill_PhysiqueType 0x00000020
    FrillColor = 1 << 6, // FrillColor / FrillColor_PhysiqueType 0x00000040
    Special = 1 << 7, // Special / Special_PhysiqueType 0x00000080
    ShirtClothingColor = 1 << 8, // ShirtClothingColor / ShirtClothingColor_PhysiqueType 0x00000100
    PantsClothingColor = 1 << 9, // PantsClothingColor / PantsClothingColor_PhysiqueType 0x00000200
    BootsClothingColor = 1 << 10, // BootsClothingColor / BootsClothingColor_PhysiqueType 0x00000400
    ClothingMask = ShirtClothingColor | PantsClothingColor | BootsClothingColor, // ClothingMask / ClothingMask_PhysiqueType 0x00000700
    FaceDetail = 1 << 11, // FaceDetail / FaceDetail_PhysiqueType 0x00000800
    EyeColor = 1 << 12, // EyeColor / _ 0x00001000
    LAST, // LAST / _ 0x00001001

    ALL = uint.MaxValue, // ALL / ALL_PhysiqueType
}
