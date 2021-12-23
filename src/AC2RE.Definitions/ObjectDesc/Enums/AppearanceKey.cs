using System;

namespace AC2RE.Definitions;

// Dat file 2300001F / Const *_AppearanceKey
[Flags]
public enum AppearanceKey : uint {
    Invalid = 0, // _ / Invalid_AppearanceKey
    SkinColor = 0x1, // SkinColor / SkinColor_AppearanceKey
    ClothingColor = 0x2, // ClothingColor / ClothingColor_AppearanceKey
    HeadMesh = 0x3, // HeadMesh / HeadMesh_AppearanceKey

    HeadColor = 0xB, // HeadColor / HeadColor_AppearanceKey
    BeardMesh = 0xC, // BeardMesh / BeardMesh_AppearanceKey

    ClothingColorSecondary = 0xE, // ClothingColorSecondary / ClothingColorSecondary_AppearanceKey
    SkinTexture = 0xF, // SkinTexture / SkinTexture_AppearanceKey
    Worn = 0x10, // Worn / Worn_AppearanceKey

    None = 0x40000001, // _ / None_AppearanceKey

    Pile = 0x41000001, // Pile / Pile_AppearanceKey
    MusicPart = 0x41000002, // MusicPart / _
    Special = 0x41000003, // Special / Special_AppearanceKey
    ClothingColorTertiary = 0x41000004, // ClothingColorTertiary / ClothingColorTertiary_AppearanceKey
    ItemQuality = 0x41000005, // ItemQuality / ItemQuality_AppearanceKey

    Dng_SandStone = 0x41000007, // Dng_SandStone / Dng_SandStone_AppearanceKey
    Dng_ColdStone = 0x41000008, // Dng_ColdStone / Dng_ColdStone_AppearanceKey
    Dng_SeaShore = 0x41000009, // Dng_SeaShore / Dng_SeaShore_AppearanceKey
    Dng_GreyStone = 0x4100000A, // Dng_GreyStone / Dng_GreyStone_AppearanceKey
    Dng_CarveRock = 0x4100000B, // Dng_CarveRock / Dng_CarveRock_AppearanceKey
    Upkeep = 0x4100000C, // Upkeep / Upkeep_AppearanceKey
    MusicInstrument = 0x4100000D, // MusicInstrument / MusicInstrument_AppearanceKey
    Dng_Basic = 0x4100000E, // Dng_Basic / Dng_Basic_AppearanceKey
    Dng_Basic_Haunted = 0x4100000F, // Dng_Basic_Haunted / Dng_Basic_Haunted_AppearanceKey

    Mossy = 0x41000014, // Mossy / Mossy_AppearanceKey
    Snow = 0x41000015, // Snow / Snow_AppearanceKey
    Sand = 0x41000016, // Sand / Sand_AppearanceKey
    Level = 0x41000017, // Level / Level_AppearanceKey
    Dng_IceCave = 0x41000018, // Dng_IceCave / Dng_IceCave_AppearanceKey

    WeaponEffect = 0x4100001A, // WeaponEffect / WeaponEffect_AppearanceKey
    Hit_Sound = 0x4100001B, // Hit_Sound / Hit_Sound_AppearanceKey
    FaceTexture = 0x4100001C, // FaceTexture / FaceTexture_AppearanceKey
    Dng_SandStone_grass = 0x4100001D, // Dng_SandStone_grass / Dng_SandStone_grass_AppearanceKey
    Dng_SeaShore_swamp = 0x4100001E, // Dng_SeaShore_swamp / Dng_SeaShore_swamp_AppearanceKey
    Dng_Basic_EmpLite = 0x4100001F, // Dng_Basic_EmpLite / Dng_Basic_EmpLite_AppearanceKey
    HeadSpecial = 0x41000020, // HeadSpecial / HeadSpecial_AppearanceKey
    Decal = 0x41000021, // Decal / Decal_AppearanceKey
    MountSkin = 0x41000022, // MountSkin / MountSkin_AppearanceKey
    Dng_volcanic = 0x41000023, // Dng_volcanic / Dng_volcanic_AppearanceKey
    Hulkskin = 0x41000024, // Hulkskin / Hulkskin_AppearanceKey

    Dng_Olthoi = 0x41000026, // Dng_Olthoi / Dng_Olthoi_AppearanceKey
    HelmColor = 0x41000027, // HelmColor / HelmColor_AppearanceKey

    Dng_volcanic2 = 0x41000029, // Dng_volcanic2 / Dng_volcanic2_AppearanceKey
    Dng_Chaotic = 0x4100002A, // Dng_Chaotic / Dng_Chaotic_AppearanceKey
    Dng_Castle_Human = 0x4100002B, // Dng_Castle_Human / _
    Dng_Fancy_Broken = 0x4100002C, // Dng_Fancy_Broken / _
    Dng_Cave_Red = 0x4100002D, // Dng_Cave_Red / _
    eyes = 0x4100002E, // eyes / _
    Dng_Cave_Dark = 0x4100002F, // Dng_Cave_Dark / _

    ItemEffect = 0x81000001, // ItemEffect / ItemEffect_AppearanceKey
}
