namespace AC2RE.Definitions;

// Const *_IntStat / WSL func gmPropertyMapper::constructor
public enum IntStat : uint {
    Undef = 0, // Undef_IntStat
    Placement = 1, // Placement_IntStat
    PhysicsState = 2, // PhysicsState_IntStat
    EtherealPhysicsTypeLow = 3, // EtherealPhysicsTypeLow_IntStat
    EtherealPhysicsTypeHigh = 4, // EtherealPhysicsTypeHigh_IntStat
    EtherealPlacementTypeLow = 5, // EtherealPlacementTypeLow_IntStat
    EtherealPlacementTypeHigh = 6, // EtherealPlacementTypeHigh_IntStat
    EtherealMovementTypeLow = 7, // EtherealMovementTypeLow_IntStat
    EtherealMovementTypeHigh = 8, // EtherealMovementTypeHigh_IntStat
    DetectionReportTypeLow = 10, // DetectionReportTypeLow_IntStat
    DetectionReportTypeHigh = 11, // DetectionReportTypeHigh_IntStat

    WeenieType = 20, // WeenieType_IntStat
    SeeMeVisibilityMask = 21, // SeeMeVisibilityMask_IntStat
    CanSeeVisibilityMask = 22, // CanSeeVisibilityMask_IntStat

    Health_RawLevel = 256, // Health_RawLevel_IntStat
    Vigor_RawLevel = 257, // Vigor_RawLevel_IntStat
    Health_CurrentLevel = 258, // Health_CurrentLevel_IntStat
    Vigor_CurrentLevel = 259, // Vigor_CurrentLevel_IntStat
    PlacementPosition = 260, // PlacementPosition_IntStat
    MaxStackSize = 261, // MaxStackSize_IntStat
    Quantity = 262, // Quantity_IntStat
    Health_CachedMax = 263, // Health_CachedMax_IntStat
    Vigor_CachedMax = 264, // Vigor_CachedMax_IntStat
    LastEquippedLocation = 265, // LastEquippedLocation_IntStat
    Focus_CurrentLevel = 266, // Focus_CurrentLevel_IntStat
    Focus_Max = 267, // Focus_Max_IntStat
    ClothingPriority = 268, // ClothingPriority_IntStat
    PK_Damage = 269, // PK_Damage_IntStat
    PK_Vigorloss = 270, // PK_Vigorloss_IntStat

    WeaponLength = 272, // WeaponLength_IntStat

    CurrentEquippedLocation = 274, // CurrentEquippedLocation_IntStat
    ValidInventoryLocations = 275, // ValidInventoryLocations_IntStat
    Inv_PrimaryParentingLocation = 276, // Inv_PrimaryParentingLocation_IntStat
    Inv_SecondaryParentingLocation = 277, // Inv_SecondaryParentingLocation_IntStat
    ParentingOrientation = 278, // ParentingOrientation_IntStat

    ContainerMaxCapacity = 280, // ContainerMaxCapacity_IntStat

    PreferredInventoryLocation = 282, // PreferredInventoryLocation_IntStat
    PrecludedInventoryLocations = 283, // PrecludedInventoryLocations_IntStat
    ImplementTypePrimary = 284, // ImplementTypePrimary_IntStat
    ImplementTypeSecondary = 285, // ImplementTypeSecondary_IntStat
    Slots = 286, // Slots_IntStat

    Species = 290, // Species_IntStat
    Sex = 291, // Sex_IntStat
    Class = 292, // Class_IntStat
    NaturalArmor = 293, // NaturalArmor_IntStat
    SafeModeState = 294, // SafeModeState_IntStat
    Agent_DamageAdd = 295, // Agent_DamageAdd_IntStat
    Agent_Vigor_Cost = 296, // Agent_Vigor_Cost_IntStat
    Agent_Focus_Cost = 297, // Agent_Focus_Cost_IntStat

    OBSOLETE_TotalXP = 300, // OBSOLETE_TotalXP_IntStat
    OBSOLETE_AvailableXP = 301, // OBSOLETE_AvailableXP_IntStat
    Level = 302, // Level_IntStat

    OBSOLETE_DeathXP = 312, // OBSOLETE_DeathXP_IntStat
    ChallengeLevel = 313, // ChallengeLevel_IntStat
    DeathCount = 314, // DeathCount_IntStat
    DeathFocus = 315, // DeathFocus_IntStat
    OBSOLETE_XPToRaiseVitae = 316, // OBSOLETE_XPToRaiseVitae_IntStat
    KillerLevel = 317, // KillerLevel_IntStat
    OriginatorLevel = 318, // OriginatorLevel_IntStat

    Gen_Behavior = 370, // Gen_Behavior_IntStat
    Gen_ToggleGameEvent = 371, // Gen_ToggleGameEvent_IntStat

    Gen_MinQuantityOverride = 380, // Gen_MinQuantityOverride_IntStat
    Gen_MaxQuantityOverride = 381, // Gen_MaxQuantityOverride_IntStat
    Gen_State = 382, // Gen_State_IntStat
    Gen_ExitWorldBehavior = 383, // Gen_ExitWorldBehavior_IntStat
    Gen_Profile = 384, // Gen_Profile_IntStat
    Gen_DayProfile = 385, // Gen_DayProfile_IntStat
    Gen_NightProfile = 386, // Gen_NightProfile_IntStat
    Gen_QualityOverride = 387, // Gen_QualityOverride_IntStat
    Gen_QualityVarianceOverride = 388, // Gen_QualityVarianceOverride_IntStat
    Death_LootQualityOverride = 389, // Death_LootQualityOverride_IntStat
    Death_LootQualityVarianceOverride = 390, // Death_LootQualityVarianceOverride_IntStat

    Death_LootMinQuantityOverride = 397, // Death_LootMinQuantityOverride_IntStat
    Death_LootMaxQuantityOverride = 398, // Death_LootMaxQuantityOverride_IntStat
    Death_LootProfile = 399, // Death_LootProfile_IntStat
    TSYS_CoarseItemClass = 400, // TSYS_CoarseItemClass_IntStat

    TSYS_FineItemClass = 420, // TSYS_FineItemClass_IntStat
    Luck = 421, // Luck_IntStat
    TSYS_MundaneMutation = 422, // TSYS_MundaneMutation_IntStat

    ConfirmationToken = 450, // ConfirmationToken_IntStat
    Fellowship_ConfirmationToken = 451, // Fellowship_ConfirmationToken_IntStat

    Allegiance_Rank = 500, // Allegiance_Rank_IntStat
    OBSOLETE_Allegiance_XPPool = 501, // OBSOLETE_Allegiance_XPPool_IntStat
    Allegiance_ConfirmationToken = 502, // Allegiance_ConfirmationToken_IntStat
    OBSOLETE_Allegiance_XPInherited = 503, // OBSOLETE_Allegiance_XPInherited_IntStat
    Allegiance_RenameCredits = 504, // Allegiance_RenameCredits_IntStat

    Trade_ConfirmationToken = 525, // Trade_ConfirmationToken_IntStat

    PK_AlwaysTruePermissions = 550, // PK_AlwaysTruePermissions_IntStat
    PK_AlwaysFalsePermissions = 551, // PK_AlwaysFalsePermissions_IntStat
    PK_Rating = 552, // PK_Rating_IntStat
    PK_LastSubmittedRating = 553, // PK_LastSubmittedRating_IntStat
    PK_CreatorType = 554, // PK_CreatorType_IntStat

    Weapon_Damage = 600, // Weapon_Damage_IntStat
    Weapon_Speed = 601, // Weapon_Speed_IntStat
    Weapon_SingleWeaponStance = 602, // Weapon_SingleWeaponStance_IntStat
    Weapon_WithShieldStance = 603, // Weapon_WithShieldStance_IntStat
    Weapon_DualWieldStance = 604, // Weapon_DualWieldStance_IntStat
    Weapon_OffenseMod = 605, // Weapon_OffenseMod_IntStat

    VigorCost = 609, // VigorCost_IntStat
    ArmorLevel = 610, // ArmorLevel_IntStat
    CombatDelay = 611, // CombatDelay_IntStat
    Value = 612, // Value_IntStat
    MaxInscriptionLength = 613, // MaxInscriptionLength_IntStat
    InscribePermission = 614, // InscribePermission_IntStat
    EquipmentSlider = 615, // EquipmentSlider_IntStat
    RadarBlip = 616, // RadarBlip_IntStat
    FocusCost = 617, // FocusCost_IntStat
    Durability_MaxLevel = 618, // Durability_MaxLevel_IntStat
    Durability_CurrentLevel = 619, // Durability_CurrentLevel_IntStat

    Usage_MinLevel = 700, // Usage_MinLevel_IntStat
    Usage_MaxLevel = 701, // Usage_MaxLevel_IntStat
    Usage_RequiredSkill1 = 702, // Usage_RequiredSkill1_IntStat
    Usage_RequiredSkill2 = 703, // Usage_RequiredSkill2_IntStat
    Usage_RequiredSkill1Rating = 704, // Usage_RequiredSkill1Rating_IntStat
    Usage_RequiredSkill2Rating = 705, // Usage_RequiredSkill2Rating_IntStat
    Usage_RestrictedSkill1 = 706, // Usage_RestrictedSkill1_IntStat
    Usage_RestrictedSkill2 = 707, // Usage_RestrictedSkill2_IntStat
    Usage_RequiredRace = 708, // Usage_RequiredRace_IntStat
    Usage_RequiredFaction = 709, // Usage_RequiredFaction_IntStat
    Usage_ValidWeenieType = 710, // Usage_ValidWeenieType_IntStat
    Usage_RequiredQuest = 711, // Usage_RequiredQuest_IntStat
    Usage_RequiredQuestStatus = 712, // Usage_RequiredQuestStatus_IntStat
    Usage_MinimumRank = 713, // Usage_MinimumRank_IntStat
    Usage_MaximumRank = 714, // Usage_MaximumRank_IntStat
    Usage_RequiredQuest_Take = 715, // Usage_RequiredQuest_Take_IntStat
    Usage_RequiredQuestStatus_Take = 716, // Usage_RequiredQuestStatus_Take_IntStat

    Usage_RequiredCraftSkillRating = 718, // Usage_RequiredCraftSkillRating_IntStat

    Usage_NumberOfEntitiesUsedWith = 720, // Usage_NumberOfEntitiesUsedWith_IntStat
    Usage_TargetType = 721, // Usage_TargetType_IntStat
    Usage_ValidTargetTypes = 722, // Usage_ValidTargetTypes_IntStat
    Usage_KeyID = 723, // Usage_KeyID_IntStat
    Usage_BehaviorName = 724, // Usage_BehaviorName_IntStat
    Usage_HealthCost = 725, // Usage_HealthCost_IntStat
    Usage_VigorCost = 726, // Usage_VigorCost_IntStat
    Usage_UserBehaviorName = 727, // Usage_UserBehaviorName_IntStat
    Usage_RequiredArcaneLore = 728, // Usage_RequiredArcaneLore_IntStat
    Usage_UserBehaviorRepeatCount = 729, // Usage_UserBehaviorRepeatCount_IntStat
    Usage_RequiredLocationFeedbackType = 730, // Usage_RequiredLocationFeedbackType_IntStat
    Usage_RequiredSkillLevel = 731, // Usage_RequiredSkillLevel_IntStat
    Usage_TargetMinLevel = 732, // Usage_TargetMinLevel_IntStat
    Usage_TargetRequiredArcaneLore = 733, // Usage_TargetRequiredArcaneLore_IntStat
    Usage_TargetWeenieType = 734, // Usage_TargetWeenieType_IntStat

    Craft_PrimaryTrait = 800, // Craft_PrimaryTrait_IntStat
    Craft_PrimaryTraitAmount = 801, // Craft_PrimaryTraitAmount_IntStat

    Craft_SecondaryTrait = 803, // Craft_SecondaryTrait_IntStat
    Craft_SecondaryTraitAmount = 804, // Craft_SecondaryTraitAmount_IntStat

    Craft_RareTrait = 806, // Craft_RareTrait_IntStat
    Craft_Flags = 807, // Craft_Flags_IntStat

    Craft_MineMaxUses = 830, // Craft_MineMaxUses_IntStat
    Craft_MineUsageResetTime = 831, // Craft_MineUsageResetTime_IntStat
    Craft_MineObjectQuantity = 832, // Craft_MineObjectQuantity_IntStat

    GrooveLevel = 950, // GrooveLevel_IntStat

    Faction_Membership = 2000, // Faction_Membership_IntStat
    Faction_Status = 2001, // Faction_Status_IntStat
    Faction_Ownership = 2002, // Faction_Ownership_IntStat
    Faction_LocalStatus = 2003, // Faction_LocalStatus_IntStat

    Vendor_MinBuyValue = 3000, // Vendor_MinBuyValue_IntStat
    Vendor_MaxBuyValue = 3001, // Vendor_MaxBuyValue_IntStat

    Money = 3100, // Money_IntStat

    ImplementType = 4000, // ImplementType_IntStat
    MusicChannel = 4001, // MusicChannel_IntStat

    MaxUpkeepPoints = 4100, // MaxUpkeepPoints_IntStat
    CurrentUpkeepPoints = 4101, // CurrentUpkeepPoints_IntStat
    EnterWorldFX = 4102, // EnterWorldFX_IntStat
    AppearanceMutationKey = 4103, // AppearanceMutationKey_IntStat
    SkillTargetFlags = 4104, // SkillTargetFlags_IntStat
    NPC_DamageType = 4105, // _ / NPC_DamageType

    Quest_BestowedSceneID = 4200, // Quest_BestowedSceneID_IntStat
    Travel_PortalFlags = 4201, // Travel_PortalFlags_IntStat
    Travel_PortalScene = 4202, // Travel_PortalScene_IntStat

    AI_ItemHint = 9502, // AI_ItemHint_IntStat
    AI_DetectionTypeLow = 9503, // AI_DetectionTypeLow_IntStat
    AI_DetectionTypeHigh = 9504, // AI_DetectionTypeHigh_IntStat
    AI_MovementType = 9505, // AI_MovementType_IntStat
    AI_DetectionContext = 9506, // AI_DetectionContext_IntStat

    AI_Attackability = 9509, // AI_Attackability_IntStat
    AI_CurrentChainCategory = 9510, // AI_CurrentChainCategory_IntStat

    AI_SuperClass = 9600, // AI_SuperClass_IntStat
    AI_SubClass = 9601, // AI_SubClass_IntStat
    AI_PetFlags = 9602, // AI_PetFlags_IntStat
    AI_PetClass = 9603, // AI_PetClass_IntStat

    AI_LowArmorLevel = 9700, // AI_LowArmorLevel_IntStat
    AI_HighArmorLevel = 9701, // AI_HighArmorLevel_IntStat
    AI_LowPlayerLevel = 9702, // AI_LowPlayerLevel_IntStat
    AI_HighPlayerLevel = 9703, // AI_HighPlayerLevel_IntStat

    Skill_Resets = 9800, // Skill_Resets_IntStat
    NameChange_Credits = 9801, // NameChange_Credits_IntStat
    WorldID_MigratedTo = 9802, // WorldID_MigratedTo_IntStat
    RealTimePlayerBecameHero = 9803, // RealTimePlayerBecameHero_IntStat
    CraftSkill_Resets = 9804, // CraftSkill_Resets_IntStat
    Craft_ToolCraftSkillMod = 9805, // Craft_ToolCraftSkillMod_IntStat
    Activation_Type = 9806, // Activation_Type_IntStat
    HeroSkill_Resets = 9807, // HeroSkill_Resets_IntStat
    ExtraSales = 9808, // ExtraSales_IntStat
}
