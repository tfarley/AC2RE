namespace AC2RE.Definitions;

// Const *_BoolStat / WSL func gmPropertyMapper::constructor
public enum BoolStat : uint {
    Undef = 0, // Undef_BoolStat
    IgnoreCollisions = 1, // IgnoreCollisions_BoolStat
    ReportCollisions = 2, // ReportCollisions_BoolStat
    Ethereal = 3, // Ethereal_BoolStat
    GravityStatus = 4, // GravityStatus_BoolStat
    LightsStatus = 5, // LightsStatus_BoolStat
    Inelastic = 6, // Inelastic_BoolStat
    AllowEdgeSlide = 7, // AllowEdgeSlide_BoolStat
    IsMobile = 8, // IsMobile_BoolStat
    Placeable = 9, // Placeable_BoolStat
    NoDraw = 10, // NoDraw_BoolStat

    DoesImpactDamage = 12, // DoesImpactDamage_BoolStat
    OBSOLETE_IsItemHousekeepingMsgPending = 13, // OBSOLETE_IsItemHousekeepingMsgPending_BoolStat
    NeverHousekeep = 14, // NeverHousekeep_BoolStat
    ReapplyPropertiesOnVersionChange = 15, // ReapplyPropertiesOnVersionChange_BoolStat

    CS_Initted = 30, // CS_Initted_BoolStat
    CS_Verified = 31, // CS_Verified_BoolStat
    SO_Initted = 32, // SO_Initted_BoolStat
    SO_Verified = 33, // SO_Verified_BoolStat

    IsCloaking = 256, // IsCloaking_BoolStat
    IsUnCloaking = 257, // IsUnCloaking_BoolStat
    IsCloaked = 258, // IsCloaked_BoolStat

    HeartbeatToggle = 270, // HeartbeatToggle_BoolStat

    Dead = 279, // Dead_BoolStat
    DestroyAllItemsOnRot = 280, // DestroyAllItemsOnRot_BoolStat
    DestroyOnCorpseRot = 281, // DestroyOnCorpseRot_BoolStat
    DestroyOnDeath = 282, // DestroyOnDeath_BoolStat
    LoseOnDeath = 283, // LoseOnDeath_BoolStat
    NeverLoseOnDeath = 284, // NeverLoseOnDeath_BoolStat
    LoseAllInventoryOnDeath = 285, // LoseAllInventoryOnDeath_BoolStat
    LootProof = 286, // LootProof_BoolStat
    UnlockAfterFirstLoot = 287, // UnlockAfterFirstLoot_BoolStat
    Death_CopyInventoryToCorpse = 288, // Death_CopyInventoryToCorpse_BoolStat
    Death_CanResurrect = 289, // Death_CanResurrect_BoolStat
    Death_NeverSayDie = 290, // Death_NeverSayDie_BoolStat
    Death_NeverLeaveCorpse = 291, // Death_NeverLeaveCorpse_BoolStat
    Death_LastKilledByPlayer = 292, // Death_LastKilledByPlayer_BoolStat
    Death_IsBeingButchered = 293, // Death_IsBeingButchered_BoolStat
    Death_MungeCorpseOverrideEntity = 294, // Death_MungeCorpseOverrideEntity_BoolStat

    Usage_UseWhenCollided = 300, // Usage_UseWhenCollided_BoolStat
    Usage_UseWhileMoving = 301, // Usage_UseWhileMoving_BoolStat
    Usage_DestroyOnUse = 302, // Usage_DestroyOnUse_BoolStat
    Usage_LockOnUse = 303, // Usage_LockOnUse_BoolStat
    Usage_AgentDestroyOnUse = 304, // Usage_AgentDestroyOnUse_BoolStat
    Usage_InventoryRequired = 305, // Usage_InventoryRequired_BoolStat
    Usage_MoveToTarget = 306, // Usage_MoveToTarget_BoolStat
    Usage_CancelsLifestoneProtection = 307, // Usage_CancelsLifestoneProtection_BoolStat
    Usage_Lockable = 308, // Usage_Lockable_BoolStat
    Usage_Locked = 309, // Usage_Locked_BoolStat
    Usage_OpenOnUnlock = 310, // Usage_OpenOnUnlock_BoolStat
    Usage_LockOnClose = 311, // Usage_LockOnClose_BoolStat
    Usage_Unopenable = 312, // Usage_Unopenable_BoolStat
    Usage_Uncloseable = 313, // Usage_Uncloseable_BoolStat
    Usage_AICanIgnoreLock = 314, // Usage_AICanIgnoreLock_BoolStat
    Usage_AICanUseDoors = 315, // Usage_AICanUseDoors_BoolStat
    Usage_LandblockFactionRequired = 316, // Usage_LandblockFactionRequired_BoolStat
    Usage_NonAllegianceOnly = 317, // Usage_NonAllegianceOnly_BoolStat
    Usage_MonarchOnly = 318, // Usage_MonarchOnly_BoolStat
    Usage_ShouldDelegateUsage = 319, // Usage_ShouldDelegateUsage_BoolStat
    Usage_ShouldApplyEffectsToTarget = 320, // Usage_ShouldApplyEffectsToTarget_BoolStat
    Usage_CrafterOnly = 321, // Usage_CrafterOnly_BoolStat
    Usage_HeroOnly = 322, // Usage_HeroOnly_BoolStat
    Usage_ShouldUnlockUserForUsageEffects = 323, // Usage_ShouldUnlockUserForUsageEffects_BoolStat
    Usage_SummonerOnly = 324, // Usage_SummonerOnly_BoolStat
    Usage_DurabilityLostOnUse = 325, // Usage_DurabilityLostOnUse_BoolStat
    Usage_LegionsExpansionOnly = 326, // Usage_LegionsExpansionOnly_BoolStat

    Weapon_Harmless = 600, // Weapon_Harmless_BoolStat
    IsCrafted = 601, // IsCrafted_BoolStat
    IsQuestItem = 602, // IsQuestItem_BoolStat
    IsRareItem = 603, // IsRareItem_BoolStat
    IsIncomparableItem = 604, // IsIncomparableItem_BoolStat
    IsExtractable = 605, // IsExtractable_BoolStat

    Effect_IsEnchantable = 620, // Effect_IsEnchantable_BoolStat

    IsUsable = 623, // IsUsable_BoolStat
    IsSelectable = 624, // IsSelectable_BoolStat
    IsTakeable = 625, // IsTakeable_BoolStat

    Stackable = 700, // Stackable_BoolStat
    Attunable = 701, // Attunable_BoolStat
    Open = 702, // Open_BoolStat
    Inventory_IgnoresAttunement = 703, // Inventory_IgnoresAttunement_BoolStat
    Inventory_IgnoresTakePermissions = 704, // Inventory_IgnoresTakePermissions_BoolStat
    IsBindOnUse = 705, // IsBindOnUse_BoolStat

    Death_LootAbsoluteOverride = 800, // Death_LootAbsoluteOverride_BoolStat
    Gen_NonPersonalizable = 801, // Gen_NonPersonalizable_BoolStat

    Gen_IsAGenerator = 810, // Gen_IsAGenerator_BoolStat

    Gen_ContainedWaitOnOpen = 812, // Gen_ContainedWaitOnOpen_BoolStat
    Gen_Managed = 813, // Gen_Managed_BoolStat
    Gen_RegenAllIfUnbound = 814, // Gen_RegenAllIfUnbound_BoolStat
    Gen_Internal = 815, // Gen_Internal_BoolStat
    Gen_EnterWorldPreserve = 816, // Gen_EnterWorldPreserve_BoolStat
    Gen_Munge = 817, // Gen_Munge_BoolStat
    Gen_DontMunge = 818, // Gen_DontMunge_BoolStat
    Gen_AbsoluteQuantity = 819, // Gen_AbsoluteQuantity_BoolStat
    Gen_Checkpoint = 820, // Gen_Checkpoint_BoolStat
    Gen_DontCheckpoint = 821, // Gen_DontCheckpoint_BoolStat
    Gen_RegenOnOpenContainer = 822, // Gen_RegenOnOpenContainer_BoolStat

    Gen_IsALinkedObject = 830, // Gen_IsALinkedObject_BoolStat

    NPC_NeverDropTesserae = 840, // _ / NPC_NeverDropTesserae
    NPC_NeverDropLodestones = 841, // _ / NPC_NeverDropLodestones

    Combat_AutomaticallyMove = 1000, // Combat_AutomaticallyMove_BoolStat
    Combat_NotAttackable = 1001, // Combat_NotAttackable_BoolStat

    Player_HasStartedCharacterSessionForTheFirstTime = 2000, // Player_HasStartedCharacterSessionForTheFirstTime_BoolStat
    Player_HasLeveledUpForTheFirstTime = 2001, // Player_HasLeveledUpForTheFirstTime_BoolStat
    Player_IsOnMount = 2002, // Player_IsOnMount_BoolStat

    Vendor_Purchases = 3000, // Vendor_Purchases_BoolStat
    Vendor_PurchasesMagic = 3001, // Vendor_PurchasesMagic_BoolStat
    Vendor_DestroyOnSell = 3002, // Vendor_DestroyOnSell_BoolStat

    IsHero = 4000, // IsHero_BoolStat

    Craft_IsCraftSkillResetting = 5000, // Craft_IsCraftSkillResetting_BoolStat
    Mine_IsInUse = 5001, // Mine_IsInUse_BoolStat

    AI_InvCreated = 9501, // AI_InvCreated_BoolStat
    AI_Teleport = 9502, // AI_Teleport_BoolStat
    AI_Wandering = 9503, // AI_Wandering_BoolStat
    AI_FreeAttacking = 9504, // AI_FreeAttacking_BoolStat
    AI_CanJoinCliques = 9505, // AI_CanJoinCliques_BoolStat
    AI_HasDetectionSpheres = 9506, // AI_HasDetectionSpheres_BoolStat
    AI_UseTargetedDetection = 9507, // AI_UseTargetedDetection_BoolStat

    AI_WieldImplements = 9510, // AI_WieldImplements_BoolStat
    AI_WeaponAndShield = 9511, // AI_WeaponAndShield_BoolStat
    AI_DualWield = 9512, // AI_DualWield_BoolStat
    AI_WieldTwoHanded = 9513, // AI_WieldTwoHanded_BoolStat

    AI_GroupMonster = 9515, // AI_GroupMonster_BoolStat
    AI_ChampionMonster = 9516, // AI_ChampionMonster_BoolStat
    AI_UniqueMonster = 9517, // AI_UniqueMonster_BoolStat
    AI_SpecialEffectMonster = 9518, // AI_SpecialEffectMonster_BoolStat
    AI_QuestMonster = 9519, // AI_QuestMonster_BoolStat
    AI_FactionBasedOnLandblock = 9520, // AI_FactionBasedOnLandblock_BoolStat
    AI_FactionOwnershipBasedOnDeath = 9521, // AI_FactionOwnershipBasedOnDeath_BoolStat
    AI_UnwieldItemsOnIdle = 9522, // AI_UnwieldItemsOnIdle_BoolStat
    AI_IdleOnly = 9523, // AI_IdleOnly_BoolStat
    Book_ShowControls = 9524, // Book_ShowControls_BoolStat
    AI_PerformingMoveTo = 9525, // AI_PerformingMoveTo_BoolStat
    Item_IsDamageModMutable = 9526, // IsDamageModMutable_BoolStat
    AICombat_MeleeNPC = 9527, // AICombat_MeleeNPC_BoolStat
    AICombat_MissileNPC = 9528, // AICombat_MissileNPC_BoolStat

    IsRepairable = 9700, // IsRepairable_BoolStat
    IsImbuable = 9701, // IsImbuable_BoolStat
}
