﻿using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class PropertyMapper : IPackage {

        // Property stat mappings from WSL func gmPropertyMapper::constructor
        public static readonly Dictionary<PropertyName, BoolStat> PROPERTY_NAME_TO_BOOL_STAT = new() {
            { PropertyName.NeverHousekeep, BoolStat.NeverHousekeep },
            { PropertyName.GeneratorNonPersonalizable, BoolStat.Gen_NonPersonalizable },
            { PropertyName.GeneratorRegenAllIfUnbound, BoolStat.Gen_RegenAllIfUnbound },
            { PropertyName.GeneratorEnterWorldPreserve, BoolStat.Gen_EnterWorldPreserve },
            { PropertyName.GeneratorMunge, BoolStat.Gen_Munge },
            { PropertyName.GeneratorDontMunge, BoolStat.Gen_DontMunge },
            { PropertyName.GeneratorAbsoluteQuantity, BoolStat.Gen_AbsoluteQuantity },
            { PropertyName.GeneratorCheckpoint, BoolStat.Gen_Checkpoint },
            { PropertyName.GeneratorDontCheckpoint, BoolStat.Gen_DontCheckpoint },
            { PropertyName.Item_IsStackable, BoolStat.Stackable },
            { PropertyName.Item_CanUseWhileMoving, BoolStat.Usage_UseWhileMoving },
            { PropertyName.Item_CanUseOnlyInInventory, BoolStat.Usage_InventoryRequired },
            { PropertyName.Item_CanUseWhenCollided, BoolStat.Usage_UseWhenCollided },
            { PropertyName.Item_IsLockOnUse, BoolStat.Usage_LockOnUse },
            { PropertyName.Item_IsAttunable, BoolStat.Attunable },
            { PropertyName.Item_IsUsable, BoolStat.IsUsable },
            { PropertyName.Item_IsSelectable, BoolStat.IsSelectable },
            { PropertyName.Item_IsTakeable, BoolStat.IsTakeable },
            { PropertyName.Item_IsDestroyOnUse, BoolStat.Usage_DestroyOnUse },
            { PropertyName.NPC_AreAllItemsDestroyedOnRot, BoolStat.DestroyAllItemsOnRot },
            { PropertyName.NPC_IsLootProof, BoolStat.LootProof },
            { PropertyName.Usage_NonAllegianceOnly, BoolStat.Usage_NonAllegianceOnly },
            { PropertyName.Usage_MonarchOnly, BoolStat.Usage_MonarchOnly },
            { PropertyName.Usage_LandblockFactionRequired, BoolStat.Usage_LandblockFactionRequired },
            { PropertyName.Lock_Lockable, BoolStat.Usage_Lockable },
            { PropertyName.Lock_OpenOnUnlock, BoolStat.Usage_OpenOnUnlock },
            { PropertyName.Lock_LockOnClose, BoolStat.Usage_LockOnClose },
            { PropertyName.Lock_AICanIgnoreLock, BoolStat.Usage_AICanIgnoreLock },
            { PropertyName.AI_CanJoinCliques, BoolStat.AI_CanJoinCliques },
            { PropertyName.AI_FactionMembershipBasedOnLandblock, BoolStat.AI_FactionBasedOnLandblock },
            { PropertyName.AI_FactionOwnershipChangeOnDeath, BoolStat.AI_FactionOwnershipBasedOnDeath },
            { PropertyName.AI_FreeAttacking, BoolStat.AI_FreeAttacking },
            { PropertyName.AICombat_WieldImplements, BoolStat.AI_WieldImplements },
            { PropertyName.AICombat_UseWeaponAndShield, BoolStat.AI_WeaponAndShield },
            { PropertyName.AICombat_CanDualWield, BoolStat.AI_DualWield },
            { PropertyName.AICombat_CanWieldTwoHanded, BoolStat.AI_WieldTwoHanded },
            { PropertyName.Lock_IsLocked, BoolStat.Usage_Locked },
            { PropertyName.Weapon_Harmless, BoolStat.Weapon_Harmless },
            { PropertyName.AI_Wandering, BoolStat.AI_Wandering },
            { PropertyName.AI_CanUseDoors, BoolStat.Usage_AICanUseDoors },
            { PropertyName.AI_DetectionSpheres, BoolStat.AI_HasDetectionSpheres },
            { PropertyName.NPC_UnlockedAfterFirstLoot, BoolStat.UnlockAfterFirstLoot },
            { PropertyName.Placeable, BoolStat.Placeable },
            { PropertyName.NPC_IsGroupMonster, BoolStat.AI_GroupMonster },
            { PropertyName.Container_ContainedWaitOn, BoolStat.Gen_ContainedWaitOnOpen },
            { PropertyName.Container_ManagedGenerator, BoolStat.Gen_Managed },
            { PropertyName.Item_GenInternal, BoolStat.Gen_Internal },
            { PropertyName.Item_GenIsAGenerator, BoolStat.Gen_IsAGenerator },
            { PropertyName.Mobile, BoolStat.IsMobile },
            { PropertyName.IgnoreCollisions, BoolStat.IgnoreCollisions },
            { PropertyName.ReportCollisions, BoolStat.ReportCollisions },
            { PropertyName.AI_ChampionMonster, BoolStat.AI_ChampionMonster },
            { PropertyName.AI_UniqueMonster, BoolStat.AI_UniqueMonster },
            { PropertyName.AI_SpecialEffectMonster, BoolStat.AI_SpecialEffectMonster },
            { PropertyName.AI_QuestMonster, BoolStat.AI_QuestMonster },
            { PropertyName.AI_UnwieldItemsOnIdle, BoolStat.AI_UnwieldItemsOnIdle },
            { PropertyName.Item_IsQuestItem, BoolStat.IsQuestItem },
            { PropertyName.Container_IsAcceptingItems, BoolStat.Open },
            { PropertyName.Usage_CrafterOnly, BoolStat.Usage_CrafterOnly },
            { PropertyName.AI_NeverSayDie, BoolStat.Death_NeverSayDie },
            { PropertyName.Item_IsRareItem, BoolStat.IsRareItem },
            { PropertyName.NPC_NeverLeaveCorpse, BoolStat.Death_NeverLeaveCorpse },
            { PropertyName.Usage_HeroOnly, BoolStat.Usage_HeroOnly },
            { PropertyName.Usage_ShouldUnlockUserForUsageEffects, BoolStat.Usage_ShouldUnlockUserForUsageEffects },
            { PropertyName.Usage_CancelSafeModeOnUsage, BoolStat.Usage_CancelsLifestoneProtection },
            { PropertyName.Usage_SummonerOnly, BoolStat.Usage_SummonerOnly },
            { PropertyName.Item_IsExtractable, BoolStat.IsExtractable },
            { PropertyName.Usage_DurabilityLostOnUse, BoolStat.Usage_DurabilityLostOnUse },
            { PropertyName.Book_ShowControls, BoolStat.Book_ShowControls },
            { PropertyName.Usage_IsBindOnUse, BoolStat.Usage_IsBindOnUse },
            { PropertyName.Usage_LegionsExpansionOnly, BoolStat.Usage_LegionsExpansionOnly },
            { PropertyName.NPC_DeathLootAbsoluteOverride, BoolStat.Death_LootAbsoluteOverride },
            { PropertyName.NPC_MungeCorpseOverrideEntity, BoolStat.NPC_MungeCorpseOverrideEntity },
            { PropertyName.Item_IsDamageModMutable, BoolStat.Item_IsDamageModMutable },
            { PropertyName.AICombat_MeleeNPC, BoolStat.AICombat_MeleeNPC },
            { PropertyName.AICombat_MissileNPC, BoolStat.AICombat_MissileNPC },
            { PropertyName.NPC_NeverDropTesserae, BoolStat.NPC_NeverDropTesserae },
            { PropertyName.NPC_NeverDropLodestones, BoolStat.NPC_NeverDropLodestones },
        };

        public static readonly Dictionary<PropertyName, DataIdStat> PROPERTY_NAME_TO_DATA_ID_STAT = new() {
            { PropertyName.Icon, DataIdStat.IconID },
            { PropertyName.PhysObj, DataIdStat.PhysObj },
            { PropertyName.Item_PileAppearance, DataIdStat.PileAppearanceID },
            { PropertyName.Usage_ItemInteractionTable, DataIdStat.Usage_ItemInteractionTableID },
            { PropertyName.Usage_ErrorMessagesTableID, DataIdStat.Usage_ErrorMessagesTableID },
            { PropertyName.Usage_RequiredCraftSkill, DataIdStat.Usage_RequiredCraftSkill },
            { PropertyName.ForgeEffect, DataIdStat.Craft_ForgeEffect },
            { PropertyName.Mine_RequiredEffect, DataIdStat.MineRequiredEffect },
            { PropertyName.CraftSkill, DataIdStat.CraftSkill },
            { PropertyName.Mine_Object, DataIdStat.MineObject },
            { PropertyName.ButcheryProfile, DataIdStat.ButcheryProfile },
            { PropertyName.StoreTemplate, DataIdStat.StoreTemplate },
            { PropertyName.Book_Image, DataIdStat.Book_Image },
            { PropertyName.StoreGroup, DataIdStat.StoreGroup },
            { PropertyName.NPC_CorpseOverrideEntity, DataIdStat.NPC_CorpseOverrideEntity },
        };

        public static readonly Dictionary<PropertyName, FloatStat> PROPERTY_NAME_TO_FLOAT_STAT = new() {
            { PropertyName.Physics_VelScale, FloatStat.Physics_VelScale },
            { PropertyName.Physics_AccScale, FloatStat.Physics_AccScale },
            { PropertyName.Physics_CombatAnimScale, FloatStat.Physics_CombatAnimScale },
            { PropertyName.Physics_JumpScale, FloatStat.Physics_JumpScale },
            { PropertyName.GeneratorRegenPeriod, FloatStat.Gen_RegenInterval },
            { PropertyName.GeneratorInitialDelay, FloatStat.GeneratorInitialDelay },
            { PropertyName.Item_MinUseDist, FloatStat.Usage_MinUsageDist },
            { PropertyName.Item_TimeBetweenUses, FloatStat.Usage_UsageInterval },
            { PropertyName.NPC_LootTimer, FloatStat.LootTimer },
            { PropertyName.NPC_BaseRotTime, FloatStat.BaseRotTime },
            { PropertyName.NPC_MaxAcceleratedRotTime, FloatStat.MaxAccelRotTime },
            { PropertyName.NPC_MinAcceleratedRotTime, FloatStat.MinAccelRotTime },
            { PropertyName.GameplayStatistics_HealthRegenRate, FloatStat.Health_RegenRate },
            { PropertyName.GameplayStatistics_VigorRegenRate, FloatStat.Vigor_RegenRate },
            { PropertyName.AI_HomesickRadius, FloatStat.AI_HomesickRadius },
            { PropertyName.AI_PerceptionRadius, FloatStat.AI_PerceptionRadius },
            { PropertyName.AICombat_NaturalVariance, FloatStat.Weapon_Variance },
            { PropertyName.ResetInterval, FloatStat.ResetCountdownTime },
            { PropertyName.Weapon_Variance, FloatStat.Weapon_Variance },
            { PropertyName.AI_WanderingRange, FloatStat.AI_WanderingRange },
            { PropertyName.AI_WanderingProb, FloatStat.AI_WanderingProb },
            { PropertyName.Scale, FloatStat.Physics_Scale },
            { PropertyName.Item_MinPermissionCheckDist, FloatStat.Usage_MinPermissionCheckDist },
            { PropertyName.MoveItemDistance, FloatStat.MoveItemDistance },
            { PropertyName.Usage_UserAnimationTimeScale, FloatStat.Usage_UserBehaviorTimeScale },
            { PropertyName.GameplayStatistics_CombatHealthRegenRate, FloatStat.Health_CombatRegenRate },
            { PropertyName.GameplayStatistics_CombatVigorRegenRate, FloatStat.Vigor_CombatRegenRate },
            { PropertyName.Weapon_CriticalHitMod, FloatStat.CriticalHitMod },
            { PropertyName.AppearanceMutationKeyValue, FloatStat.AppearanceMutationKeyValue },
            { PropertyName.Item_Durability_DecayMod, FloatStat.Durability_DecayMod },
            { PropertyName.Usage_RequiredLocationRadius, FloatStat.Usage_RequiredLocationRadius },
            { PropertyName.Usage_RequiredLocationCloseProximityMsgRadius, FloatStat.Usage_RequiredLocationCloseProximityMsgRadius },
            { PropertyName.Usage_RequiredLocationMediumProximityMsgRadius, FloatStat.Usage_RequiredLocationMediumProximityMsgRadius },
            { PropertyName.Usage_RequiredLocationFarProximityMsgRadius, FloatStat.Usage_RequiredLocationFarProximityMsgRadius },
            { PropertyName.Usage_RequiredLocationVeryFarProximityMsgRadius, FloatStat.Usage_RequiredLocationVeryFarProximityMsgRadius },
            { PropertyName.Usage_Duration, FloatStat.Usage_UsageDuration },
            { PropertyName.ForgeEffectRadius, FloatStat.Craft_ForgeEffectRadius },
            { PropertyName.Mine_ObjectQuantityVariance, FloatStat.Craft_MineObjectQuantityVariance },
            { PropertyName.AI_HealthWarningLevel, FloatStat.AI_HealthWarningLevel },
            { PropertyName.AI_HealthAggressiveness, FloatStat.AI_HealthAggressiveness },
            { PropertyName.AI_VigorWarningLevel, FloatStat.AI_VigorWarningLevel },
            { PropertyName.AI_VigorAggressiveness, FloatStat.AI_VigorAggressiveness },
            { PropertyName.Craft_Tool_XPMod, FloatStat.Craft_ToolXPMod },
            { PropertyName.Craft_Tool_QuantityMod, FloatStat.Craft_ToolQuantityMod },
            { PropertyName.Craft_DyePlant_Mod, FloatStat.Craft_DyePlantMod },
            { PropertyName.NPC_BypassableArmorMod, FloatStat.BypassableArmorMod },
            { PropertyName.NPC_CombatSpeedResistance, FloatStat.CombatSpeedResistance },
            { PropertyName.AI_NearDeathLevel, FloatStat.AI_NearDeathLevel },
            { PropertyName.AICombat_TargetConsiderInterval, FloatStat.AICombat_TargetConsiderInterval },
            { PropertyName.NPC_ArmorThreshold, FloatStat.NPC_ArmorThreshold },
            { PropertyName.NPC_DamageTypeMod, FloatStat.NPC_DamageTypeMod },
            { PropertyName.Item_NatureDamageMod, FloatStat.Item_NatureDamageMod },
            { PropertyName.Item_DecayDamageMod, FloatStat.Item_DecayDamageMod },
            { PropertyName.Item_MartialDamageMod, FloatStat.Item_MartialDamageMod },
            { PropertyName.Item_ArcaneDamageMod, FloatStat.Item_ArcaneDamageMod },
            { PropertyName.Item_NatureDamageModCap, FloatStat.Item_NatureDamageModCap },
            { PropertyName.Item_DecayDamageModCap, FloatStat.Item_DecayDamageModCap },
            { PropertyName.Item_MartialDamageModCap, FloatStat.Item_MartialDamageModCap },
            { PropertyName.Item_ArcaneDamageModCap, FloatStat.Item_ArcaneDamageModCap },
            { PropertyName.Item_NatureDamageModGrowthRate, FloatStat.Item_NatureDamageModGrowthRate },
            { PropertyName.Item_DecayDamageModGrowthRate, FloatStat.Item_DecayDamageModGrowthRate },
            { PropertyName.Item_MartialDamageModGrowthRate, FloatStat.Item_MartialDamageModGrowthRate },
            { PropertyName.Item_ArcaneDamageModGrowthRate, FloatStat.Item_ArcaneDamageModGrowthRate },
            { PropertyName.Item_NatureDamageModBaseMutabilityChance, FloatStat.Item_NatureDamageModGrowthRate },
            { PropertyName.Item_DecayDamageModBaseMutabilityChance, FloatStat.Item_DecayDamageModGrowthRate },
            { PropertyName.Item_MartialDamageModBaseMutabilityChance, FloatStat.Item_MartialDamageModGrowthRate },
            { PropertyName.Item_ArcaneDamageModBaseMutabilityChance, FloatStat.Item_ArcaneDamageModGrowthRate },
            { PropertyName.AI_HarvestingVariance, FloatStat.AI_HarvestingVariance },
        };

        public static readonly Dictionary<PropertyName, IntStat> PROPERTY_NAME_TO_INT_STAT = new() {
            { PropertyName.Usage_MinLevel, IntStat.Usage_MinLevel },
            { PropertyName.Usage_MaxLevel, IntStat.Usage_MaxLevel },
            { PropertyName.GeneratorProfile, IntStat.Gen_Profile },
            { PropertyName.GeneratorMinQuantityOverride, IntStat.Gen_MinQuantityOverride },
            { PropertyName.GeneratorMaxQuantityOverride, IntStat.Gen_MaxQuantityOverride },
            { PropertyName.Item_MaxStackSize, IntStat.MaxStackSize },
            { PropertyName.Item_PreferredInventoryLocation, IntStat.PreferredInventoryLocation },
            { PropertyName.Item_ValidInventoryLocations, IntStat.ValidInventoryLocations },
            { PropertyName.Item_PrecludedInventoryLocations, IntStat.PrecludedInventoryLocations },
            { PropertyName.Item_PrimaryParentingLocation, IntStat.Inv_PrimaryParentingLocation },
            { PropertyName.Item_SecondaryParentingLocation, IntStat.Inv_SecondaryParentingLocation },
            { PropertyName.Item_ImplementType, IntStat.ImplementType },
            { PropertyName.Item_Value, IntStat.Value },
            { PropertyName.Item_ArmorLevel, IntStat.ArmorLevel },
            { PropertyName.Item_CombatDelay, IntStat.CombatDelay },
            { PropertyName.Item_ValidTargetTypes, IntStat.Usage_ValidTargetTypes },
            { PropertyName.NPC_ArmorLevel, IntStat.NaturalArmor },
            { PropertyName.NPC_ChallengeLevel, IntStat.ChallengeLevel },
            { PropertyName.Usage_RequiredFaction, IntStat.Usage_RequiredFaction },
            { PropertyName.Usage_RequiredRace, IntStat.Usage_RequiredRace },
            { PropertyName.Usage_RequiredQuest, IntStat.Usage_RequiredQuest },
            { PropertyName.Usage_RequiredQuestStatus, IntStat.Usage_RequiredQuestStatus },
            { PropertyName.Usage_RequiredSkill1, IntStat.Usage_RequiredSkill1 },
            { PropertyName.Usage_RequiredSkill2, IntStat.Usage_RequiredSkill2 },
            { PropertyName.Usage_RestrictedSkill1, IntStat.Usage_RestrictedSkill1 },
            { PropertyName.Usage_RestrictedSkill2, IntStat.Usage_RestrictedSkill2 },
            { PropertyName.Usage_MinRank, IntStat.Usage_MinimumRank },
            { PropertyName.Usage_MaxRank, IntStat.Usage_MaximumRank },
            { PropertyName.Lock_KeyID, IntStat.Usage_KeyID },
            { PropertyName.Quest_BestowedSceneID, IntStat.Quest_BestowedSceneID },
            { PropertyName.GameplayStatistics_MaxHealth, IntStat.Health_RawLevel },
            { PropertyName.GameplayStatistics_MaxVigor, IntStat.Vigor_RawLevel },
            { PropertyName.GameplayStatistics_Level, IntStat.Level },
            { PropertyName.AI_MovementType, IntStat.AI_MovementType },
            { PropertyName.AI_FactionMembership, IntStat.Faction_Membership },
            { PropertyName.AICombat_NaturalDamage, IntStat.Weapon_Damage },
            { PropertyName.AICombat_NaturalVigorCost, IntStat.VigorCost },
            { PropertyName.AICombat_NaturalWeaponTime, IntStat.Weapon_Speed },
            { PropertyName.AICombat_NaturalOffenseModifier, IntStat.Weapon_OffenseMod },
            { PropertyName.PortalFlags, IntStat.Travel_PortalFlags },
            { PropertyName.MaximumUpkeepPoints, IntStat.MaxUpkeepPoints },
            { PropertyName.Weapon_Damage, IntStat.Weapon_Damage },
            { PropertyName.Weapon_VigorCost, IntStat.VigorCost },
            { PropertyName.Weapon_Speed, IntStat.Weapon_Speed },
            { PropertyName.Weapon_OffenseMod, IntStat.Weapon_OffenseMod },
            { PropertyName.Weapon_SingleWeaponStance, IntStat.Weapon_SingleWeaponStance },
            { PropertyName.Weapon_WithShieldStance, IntStat.Weapon_WithShieldStance },
            { PropertyName.Weapon_DualWieldStance, IntStat.Weapon_DualWieldStance },
            { PropertyName.AI_AISubClass, IntStat.AI_SubClass },
            { PropertyName.AI_AISuperClass, IntStat.AI_SuperClass },
            { PropertyName.NPC_DeathLootProfile, IntStat.Death_LootProfile },
            { PropertyName.NPC_GrooveLevel, IntStat.GrooveLevel },
            { PropertyName.NPC_TSysCoarseItemClass, IntStat.TSYS_CoarseItemClass },
            { PropertyName.NPC_TSysFineItemClass, IntStat.TSYS_FineItemClass },
            { PropertyName.Item_ParentingOrientation, IntStat.ParentingOrientation },
            { PropertyName.Container_MaxContainerSize, IntStat.ContainerMaxCapacity },
            { PropertyName.Container_MaxItems, IntStat.Gen_MaxQuantityOverride },
            { PropertyName.Item_Quantity, IntStat.Quantity },
            { PropertyName.Item_TSysCoarseItemClass, IntStat.TSYS_CoarseItemClass },
            { PropertyName.Item_TSysFineItemClass, IntStat.TSYS_FineItemClass },
            { PropertyName.Item_UsageAnimation, IntStat.Usage_BehaviorName },
            { PropertyName.Item_UsageTargetType, IntStat.Usage_TargetType },
            { PropertyName.Item_MaxInscriptionLength, IntStat.MaxInscriptionLength },
            { PropertyName.Usage_AllowedRaces, IntStat.Usage_RequiredRace },
            { PropertyName.Usage_RequiredQuest_ForTaking, IntStat.Usage_RequiredQuest_Take },
            { PropertyName.Usage_RequiredQuestStatus_ForTaking, IntStat.Usage_RequiredQuestStatus_Take },
            { PropertyName.GeneratorQualityOverride, IntStat.Gen_QualityOverride },
            { PropertyName.GeneratorQualityVarianceOverride, IntStat.Gen_QualityVarianceOverride },
            { PropertyName.NPC_DeathLootQualityOverride, IntStat.Death_LootQualityOverride },
            { PropertyName.NPC_DeathLootQualityVarianceOverride, IntStat.Death_LootQualityVarianceOverride },
            { PropertyName.Usage_Animation, IntStat.Usage_BehaviorName },
            { PropertyName.Usage_UserAnimation, IntStat.Usage_UserBehaviorName },
            { PropertyName.GeneratorExitWorldBehavior, IntStat.Gen_ExitWorldBehavior },
            { PropertyName.GeneratorToggleStateGameEvent, IntStat.Gen_ToggleGameEvent },
            { PropertyName.Item_InscribePermission, IntStat.InscribePermission },
            { PropertyName.EnterWorldFX, IntStat.EnterWorldFX },
            { PropertyName.RadarBlip, IntStat.RadarBlip },
            { PropertyName.Item_CraftFlags, IntStat.Craft_Flags },
            { PropertyName.Weapon_FocusCost, IntStat.FocusCost },
            { PropertyName.GameplayStatistics_DeathFocus, IntStat.DeathFocus },
            { PropertyName.Usage_RequiredArcaneLore, IntStat.Usage_RequiredArcaneLore },
            { PropertyName.AppearanceMutationKey, IntStat.AppearanceMutationKey },
            { PropertyName.NPC_SkillTargetFlags, IntStat.SkillTargetFlags },
            { PropertyName.Item_Durability_MaxLevel, IntStat.Durability_MaxLevel },
            { PropertyName.Usage_RequiredLocationFeedbackType, IntStat.Usage_RequiredLocationFeedbackType },
            { PropertyName.Usage_RequiredCraftSkillRating, IntStat.Usage_RequiredCraftSkillRating },
            { PropertyName.Mine_ObjectQuantity, IntStat.Craft_MineObjectQuantity },
            { PropertyName.Mine_MaxUses, IntStat.Craft_MineMaxUses },
            { PropertyName.Mine_UsageResetTime, IntStat.Craft_MineUsageResetTime },
            { PropertyName.Item_Slots, IntStat.Slots },
            { PropertyName.Item_Durability_CurrentLevel, IntStat.Durability_CurrentLevel },
            { PropertyName.Craft_Tool_CraftSkillMod, IntStat.Craft_ToolCraftSkillMod },
            { PropertyName.Usage_Tool_RequiredSkillLevel, IntStat.Usage_RequiredSkillLevel },
            { PropertyName.Usage_UserAnimationRepeatCount, IntStat.Usage_UserBehaviorRepeatCount },
            { PropertyName.Activation_Type, IntStat.Activation_Type },
            { PropertyName.PortalScene, IntStat.Travel_PortalScene },
            { PropertyName.NPC_DeathLootMinQuantityOverride, IntStat.Death_LootMinQuantityOverride },
            { PropertyName.NPC_DeathLootMaxQuantityOverride, IntStat.Death_LootMaxQuantityOverride },
            { PropertyName.NPC_DamageType, IntStat.NPC_DamageType },
            { PropertyName.Item_ValidTargetWeenieType, IntStat.Usage_ValidWeenieType },
        };

        public static readonly Dictionary<PropertyName, LongIntStat> PROPERTY_NAME_TO_LONG_INT_STAT = new() {
            { PropertyName.GameplayStatistics_DeathExperience, LongIntStat.DeathXP },
            { PropertyName.RadarColor, LongIntStat.RadarColor },
        };

        public static readonly Dictionary<PropertyName, PositionStat> PROPERTY_NAME_TO_POSITION_STAT = new() {
            { PropertyName.Usage_RequiredLocation, PositionStat.Usage_RequiredLocation },
            { PropertyName.Usage_RequiredLocationTsysMin, PositionStat.Usage_RequiredLocationTsysMin },
            { PropertyName.Usage_RequiredLocationTsysMax, PositionStat.Usage_RequiredLocationTsysMax },
        };

        public static readonly Dictionary<PropertyName, StringInfoStat> PROPERTY_NAME_TO_STRING_INFO_STAT = new() {
            { PropertyName.Name, StringInfoStat.Name },
            { PropertyName.Description, StringInfoStat.Description },
            { PropertyName.Item_PluralName, StringInfoStat.PluralName },
            { PropertyName.Usage_SuccessMessage, StringInfoStat.Usage_SuccessMessage },
            { PropertyName.PluralName, StringInfoStat.PluralName },
            { PropertyName.Book_Source, StringInfoStat.Book_Source },
            { PropertyName.Inscription, StringInfoStat.Inscription },
            { PropertyName.AuthorName, StringInfoStat.AuthorName },
            { PropertyName.Usage_CriticalSuccessMessage, StringInfoStat.Usage_CriticalSuccessMessage },
            { PropertyName.StoreLocation, StringInfoStat.StoreLocation },
        };

        public static readonly Dictionary<PropertyName, StringStat> PROPERTY_NAME_TO_STRING_STAT = new() {
            { PropertyName.PortalDestination, StringStat.TeleportDestinationStr },
        };

        public PackageType packageType => PackageType.PropertyMapper;

        public Dictionary<PropertyName, uint> propServer; // m_hashPropServer
        public Dictionary<PropertyName, uint> propClient; // m_hashPropClient

        public PropertyMapper(AC2Reader data) {
            data.ReadPkg<AAHash>(v => propServer = v.to<PropertyName, uint>());
            data.ReadPkg<AAHash>(v => propClient = v.to<PropertyName, uint>());
        }
    }
}
