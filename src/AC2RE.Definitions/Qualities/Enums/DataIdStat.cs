namespace AC2RE.Definitions {

    // Const *_DIDStat / WSL func gmPropertyMapper::constructor
    public enum DataIdStat : uint {
        Undef = 0, // Undef_DIDStat
        PhysObj = 1, // PhysObj_DIDStat
        MotionTable = 2, // MotionTable_DIDStat
        SoundTable = 3, // SoundTable_DIDStat
        PhysicsEffectTable = 4, // PhysicsEffectTable_DIDStat

        QualityFilter = 256, // QualityFilter_DIDStat
        IconID = 257, // IconID_DIDStat
        CloakingAppearanceID = 258, // CloakingAppearanceID_DIDStat
        PileAppearanceID = 259, // PileAppearanceID_DIDStat
        Usage_ErrorMessagesTableID = 260, // Usage_ErrorMessagesTableID_DIDStat
        Usage_ItemInteractionTableID = 261, // Usage_ItemInteractionTableID_DIDStat

        MineRequiredEffect = 300, // MineRequiredEffect_DIDStat
        CraftSkill = 301, // CraftSkill_DIDStat
        MineObject = 302, // MineObject_DIDStat

        ButcheryProfile = 304, // ButcheryProfile_DIDStat

        Craft_ForgeEffect = 310, // Craft_ForgeEffect_DIDStat
        Usage_RequiredCraftSkill = 311, // Usage_RequiredCraftSkill_DIDStat
        StoreTemplate = 312, // StoreTemplate_DIDStat
        Book_Image = 313, // Book_Image_DIDStat
        StoreGroup = 314, // StoreGroup_DIDStat
        NPC_CorpseOverrideEntity = 315, // _ / NPC_CorpseOverrideEntity
    }
}
