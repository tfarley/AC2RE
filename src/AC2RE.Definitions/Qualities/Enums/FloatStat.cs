namespace AC2RE.Definitions {

    // Const *_FloatStat / WSL func gmPropertyMapper::constructor
    public enum FloatStat : uint {
        Undef = 0, // Undef_FloatStat

        Translucency = 2, // Translucency_FloatStat
        Friction = 3, // Friction_FloatStat
        Elasticity = 4, // Elasticity_FloatStat
        Mass = 5, // Mass_FloatStat

        ResetCountdownTime = 50, // ResetCountdownTime_FloatStat

        HeartbeatInterval = 256, // HeartbeatInterval_FloatStat

        Health_RegenRate = 262, // Health_RegenRate_FloatStat
        Vigor_RegenRate = 263, // Vigor_RegenRate_FloatStat
        CharMinSafeVel = 264, // CharMinSafeVel_FloatStat

        CreatureFallMod = 266, // CreatureFallMod_FloatStat
        Health_CombatRegenRate = 267, // Health_CombatRegenRate_FloatStat
        Vigor_CombatRegenRate = 268, // Vigor_CombatRegenRate_FloatStat
        Focus_CostMod = 269, // Focus_CostMod_FloatStat
        ReleasedTimestamp = 270, // ReleasedTimestamp_FloatStat
        BondEquippedTreasure = 271, // BondEquippedTreasure_FloatStat
        CloseSphereRadius = 272, // CloseSphereRadius_FloatStat
        MoveItemDistance = 273, // MoveItemDistance_FloatStat

        BaseRotTime = 280, // BaseRotTime_FloatStat
        MaxAccelRotTime = 281, // MaxAccelRotTime_FloatStat
        MinAccelRotTime = 282, // MinAccelRotTime_FloatStat
        CorpseSpamCounter = 283, // CorpseSpamCounter_FloatStat
        LoseWieldedItemsChance = 284, // LoseWieldedItemsChance_FloatStat
        LootTimer = 285, // LootTimer_FloatStat
        CurrentVitae = 286, // CurrentVitae_FloatStat

        Physics_VelScale = 295, // Physics_VelScale_FloatStat
        Physics_AccScale = 296, // Physics_AccScale_FloatStat
        Physics_CombatAnimScale = 297, // Physics_CombatAnimScale_FloatStat
        Physics_Scale = 298, // Physics_Scale_FloatStat
        Physics_JumpScale = 299, // Physics_JumpScale_FloatStat
        Usage_MinPermissionCheckDist = 300, // Usage_MinPermissionCheckDist_FloatStat
        Usage_MinUsageDist = 301, // Usage_MinUsageDist_FloatStat
        Usage_UsageInterval = 302, // Usage_UsageInterval_FloatStat
        Usage_Effect1_Spellcraft = 303, // Usage_Effect1_Spellcraft_FloatStat
        Usage_Effect2_Spellcraft = 304, // Usage_Effect2_Spellcraft_FloatStat
        Usage_Effect3_Spellcraft = 305, // Usage_Effect3_Spellcraft_FloatStat
        Usage_Effect4_Spellcraft = 306, // Usage_Effect4_Spellcraft_FloatStat
        Usage_Effect5_Spellcraft = 307, // Usage_Effect5_Spellcraft_FloatStat
        Usage_UserBehaviorTimeScale = 308, // Usage_UserBehaviorTimeScale_FloatStat
        Usage_RequiredLocationRadius = 309, // Usage_RequiredLocationRadius_FloatStat
        Usage_RequiredLocationCloseProximityMsgRadius = 310, // Usage_RequiredLocationCloseProximityMsgRadius_FloatStat
        Usage_RequiredLocationMediumProximityMsgRadius = 311, // Usage_RequiredLocationMediumProximityMsgRadius_FloatStat
        Usage_RequiredLocationFarProximityMsgRadius = 312, // Usage_RequiredLocationFarProximityMsgRadius_FloatStat
        Usage_RequiredLocationVeryFarProximityMsgRadius = 313, // Usage_RequiredLocationVeryFarProximityMsgRadius_FloatStat
        Usage_UsageDuration = 314, // Usage_UsageDuration_FloatStat
        Last_Distance = 315, // Last_Distance_FloatStat

        Gen_HeartbeatInterval = 401, // Gen_HeartbeatInterval_FloatStat
        Gen_RegenInterval = 402, // Gen_RegenInterval_FloatStat
        GeneratorInitialDelay = 403, // _ / GeneratorInitialDelay

        CombatModeDelay = 500, // CombatModeDelay_FloatStat
        Combat_DamageMod = 501, // Combat_DamageMod_FloatStat
        TotalArmorMod = 502, // TotalArmorMod_FloatStat
        Combat_BaseDamageMod = 503, // Combat_BaseDamageMod_FloatStat
        Absorption_Chance = 504, // Absorption_Chance_FloatStat
        BypassableArmorMod = 505, // BypassableArmorMod_FloatStat
        CombatSpeedResistance = 506, // CombatSpeedResistance_FloatStat
        NPC_ArmorThreshold = 507, // _ / NPC_ArmorThreshold
        NPC_DamageTypeMod = 508, // _ / NPC_DamageTypeMod

        TSYS_MundaneMutationIntensity = 600, // TSYS_MundaneMutationIntensity_FloatStat

        Weapon_Variance = 1000, // Weapon_Variance_FloatStat
        CriticalHitMod = 1001, // CriticalHitMod_FloatStat
        Durability_DecayMod = 1002, // Durability_DecayMod_FloatStat
        Item_NatureDamageMod = 1003, // _ / Item_NatureDamageMod
        Item_DecayDamageMod = 1004, // _ / Item_DecayDamageMod
        Item_MartialDamageMod = 1005, // _ / Item_MartialDamageMod
        Item_ArcaneDamageMod = 1006, // _ / Item_ArcaneDamageMod

        Item_NatureDamageModCap = 1011, // _ / Item_NatureDamageModCap
        Item_DecayDamageModCap = 1012, // _ / Item_DecayDamageModCap
        Item_MartialDamageModCap = 1013, // _ / Item_MartialDamageModCap
        Item_ArcaneDamageModCap = 1014, // _ / Item_ArcaneDamageModCap
        Item_NatureDamageModGrowthRate = 1015, // _ / Item_NatureDamageModGrowthRate
        Item_DecayDamageModGrowthRate = 1016, // _ / Item_DecayDamageModGrowthRate
        Item_MartialDamageModGrowthRate = 1017, // _ / Item_MartialDamageModGrowthRate
        Item_ArcaneDamageModGrowthRate = 1018, // _ / Item_ArcaneDamageModGrowthRate

        AppearanceMutationKeyValue = 1500, // AppearanceMutationKeyValue_FloatStat

        AI_PerceptionRadius = 2000, // AI_PerceptionRadius_FloatStat
        AI_HomesickRadius = 2001, // AI_HomesickRadius_FloatStat
        AI_CliqueWeight = 2002, // AI_CliqueWeight_FloatStat

        AI_MeleeOffset = 2007, // AI_MeleeOffset_FloatStat

        AI_WanderingRange = 2020, // AI_WanderingRange_FloatStat
        AI_WanderingProb = 2021, // AI_WanderingProb_FloatStat
        AI_WanderingSpeed = 2022, // AI_WanderingSpeed_FloatStat

        AI_CurrentTargetBias = 2040, // AI_CurrentTargetBias_FloatStat
        AI_LOSTargetBias = 2041, // AI_LOSTargetBias_FloatStat
        AI_PathTargetBias = 2042, // AI_PathTargetBias_FloatStat
        AI_PathLengthTargetBias = 2043, // AI_PathLengthTargetBias_FloatStat
        AI_HighLevelTargetBias = 2044, // AI_HighLevelTargetBias_FloatStat
        AI_LowLevelTargetBias = 2045, // AI_LowLevelTargetBias_FloatStat
        AI_DamageTargetBias = 2046, // AI_DamageTargetBias_FloatStat
        AI_LoveTargetBias = 2047, // AI_LoveTargetBias_FloatStat

        AI_ImplementChoiceWindow = 2070, // AI_ImplementChoiceWindow_FloatStat
        AI_ImplementChoiceBias = 2071, // AI_ImplementChoiceBias_FloatStat

        AI_CloseRangeBias = 2074, // AI_CloseRangeBias_FloatStat
        AI_LongRangeBias = 2075, // AI_LongRangeBias_FloatStat
        AI_HealthWarningLevel = 2076, // AI_HealthWarningLevel_FloatStat
        AI_HealthAggressiveness = 2077, // AI_HealthAggressiveness_FloatStat
        AI_VigorWarningLevel = 2078, // AI_VigorWarningLevel_FloatStat
        AI_VigorAggressiveness = 2079, // AI_VigorAggressiveness_FloatStat
        AI_NearDeathLevel = 2080, // AI_NearDeathLevel_FloatStat
        AI_LowArmorBias = 2081, // AI_LowArmorBias_FloatStat
        AI_HighArmorBias = 2082, // AI_HighArmorBias_FloatStat
        AI_ManyTargetsLevel = 2083, // AI_ManyTargetsLevel_FloatStat
        AI_ManyTargetsBias = 2084, // AI_ManyTargetsBias_FloatStat
        AI_HarvestingVariance = 2085, // _ / AI_HarvestingVariance

        AI_EnchantedRandomSkewSkill = 2090, // AI_EnchantedRandomSkewSkill_FloatStat
        AICombat_TargetConsiderInterval = 2091, // _ / AICombat_TargetConsiderInterval

        AI_Pet_MaxDefendDistance = 2100, // AI_Pet_MaxDefendDistance_FloatStat
        AI_Pet_CommandSpamCounter = 2101, // AI_Pet_CommandSpamCounter_FloatStat

        AI_EnchantedRandomSkewTarget = 2200, // AI_EnchantedRandomSkewTarget_FloatStat

        Skill_ResetTimeDuration = 3000, // Skill_ResetTimeDuration_FloatStat

        Vendor_MinHousekeepTime = 4000, // Vendor_MinHousekeepTime_FloatStat
        Vendor_MaxHousekeepTime = 4001, // Vendor_MaxHousekeepTime_FloatStat
        Vendor_DefaultRegenDelay = 4002, // Vendor_DefaultRegenDelay_FloatStat
        Vendor_BuyMultiplier = 4003, // Vendor_BuyMultiplier_FloatStat
        Vendor_SellMultiplier = 4004, // Vendor_SellMultiplier_FloatStat
        Vendor_HousekeepValMean = 4005, // Vendor_HousekeepValMean_FloatStat
        Vendor_HousekeepValVariance = 4006, // Vendor_HousekeepValVariance_FloatStat

        Craft_ForgeEffectRadius = 5000, // Craft_ForgeEffectRadius_FloatStat
        Craft_MineObjectQuantityVariance = 5001, // Craft_MineObjectQuantityVariance_FloatStat
        Craft_ToolQuantityMod = 5002, // Craft_ToolQuantityMod_FloatStat
        Craft_ToolXPMod = 5003, // Craft_ToolXPMod_FloatStat
        Craft_DyePlantMod = 5004, // Craft_DyePlantMod_FloatStat

        Experience_CombatXPMod = 5500, // Experience_CombatXPMod_FloatStat
    }
}
