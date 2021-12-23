namespace AC2RE.Definitions;

// Dat file 34000000
public enum PropertyName : uint {
    Undef = 0,

    PhysicsMaterial = 0x40000001, // PhysicsMaterial
    LightType = 0x40000002, // LightType
    LightDirection = 0x40000003, // LightDirection
    LightRange = 0x40000004, // LightRange

    LightDiffuse = 0x40000008, // LightDiffuse
    LightDiffuseIntensity = 0x40000009, // LightDiffuseIntensity
    LightSpecular = 0x4000000A, // LightSpecular

    LandscapeShadowCast = 0x40000014, // LandscapeShadowCast
    VolumeShadowCast = 0x40000015, // VolumeShadowCast

    IndoorShadowCast = 0x40000017, // IndoorShadowCast

    EtherealEnvironment = 0x4000001B, // EtherealEnvironment
    EtherealWater = 0x4000001C, // EtherealWater

    CameraOrthographic = 0x4000001E, // CameraOrthographic
    CameraFOV = 0x4000001F, // CameraFOV
    CameraVerticalScale = 0x40000020, // CameraVerticalScale
    CameraNearDistance = 0x40000021, // CameraNearDistance
    CameraFarDistance = 0x40000022, // CameraFarDistance
    EtherealTest = 0x40000023, // EtherealTest
    DefaultEthereal = 0x40000024, // DefaultEthereal

    CameraEthereal = 0x40000026, // CameraEthereal

    ClearanceBBoxMin1 = 0x40000028, // ClearanceBBoxMin1
    ClearanceBBoxMin2 = 0x40000029, // ClearanceBBoxMin2
    ClearanceBBoxMin3 = 0x4000002A, // ClearanceBBoxMin3
    ClearanceBBoxMin4 = 0x4000002B, // ClearanceBBoxMin4
    ClearanceBBoxMax1 = 0x4000002C, // ClearanceBBoxMax1
    ClearanceBBoxMax2 = 0x4000002D, // ClearanceBBoxMax2
    ClearanceBBoxMax3 = 0x4000002E, // ClearanceBBoxMax3
    ClearanceBBoxMax4 = 0x4000002F, // ClearanceBBoxMax4
    ClearanceCylRadius1 = 0x40000030, // ClearanceCylRadius1
    ClearanceCylRadius2 = 0x40000031, // ClearanceCylRadius2
    ClearanceCylCenter1 = 0x40000032, // ClearanceCylCenter1
    ClearanceCylCenter2 = 0x40000033, // ClearanceCylCenter2
    ClearanceCylHeight1 = 0x40000034, // ClearanceCylHeight1
    ClearanceCylHeight2 = 0x40000035, // ClearanceCylHeight2
    TerrainType = 0x40000036, // TerrainType
    DetailMap = 0x40000037, // DetailMap
    StaticEthereal = 0x40000038, // StaticEthereal
    ContentID = 0x40000039, // ContentID
    ContentDescription = 0x4000003A, // ContentDescription
    ContentType = 0x4000003B, // ContentType
    ContentName = 0x4000003C, // ContentName
    BlockID = 0x4000003D, // BlockID
    BlockX = 0x4000003E, // BlockX
    BlockY = 0x4000003F, // BlockY
    ControlledVelocity = 0x40000040, // ControlledVelocity
    ControlledAcceleration = 0x40000041, // ControlledAcceleration
    UnControlledGeneralVel = 0x40000042, // UnControlledGeneralVel
    UnControlledGeneralAccel = 0x40000043, // UnControlledGeneralAccel
    UnControlledEarthVel = 0x40000044, // UnControlledEarthVel
    UnControlledEarthAccel = 0x40000045, // UnControlledEarthAccel
    UnControlledAirVel = 0x40000046, // UnControlledAirVel
    UnControlledAirAccel = 0x40000047, // UnControlledAirAccel
    UnControlledSeafloorVel = 0x40000048, // UnControlledSeafloorVel
    UnControlledSeafloorAccel = 0x40000049, // UnControlledSeafloorAccel
    UnControlledWaterVel = 0x4000004A, // UnControlledWaterVel
    UnControlledWaterAccel = 0x4000004B, // UnControlledWaterAccel
    LocalSpace = 0x4000004C, // LocalSpace
    InvisibleIngame = 0x4000004D, // InvisibleIngame

    LandblockType = 0x40000051, // LandblockType

    InteriorFogType = 0x40000056, // InteriorFogType
    InteriorFogColor = 0x40000057, // InteriorFogColor
    InteriorFogNear = 0x40000058, // InteriorFogNear
    InteriorFogFar = 0x40000059, // InteriorFogFar
    InteriorAmbientLight = 0x4000005A, // InteriorAmbientLight
    LightShadowType = 0x4000005B, // LightShadowType
    Translucent = 0x4000005C, // Translucent
    AppearanceKey = 0x4000005D, // AppearanceKey
    AppearanceScalar = 0x4000005E, // AppearanceScalar
    CellPhysicsMaterial = 0x4000005F, // CellPhysicsMaterial
    CellAmbientSoundType = 0x40000060, // CellAmbientSoundType
    CoronaTexture = 0x40000061, // CoronaTexture
    CoronaSize = 0x40000062, // CoronaSize
    CoronaOpacity = 0x40000063, // CoronaOpacity
    CoronaFadeSpeed = 0x40000064, // CoronaFadeSpeed
    PortalDestination = 0x40000065, // PortalDestination

    WaterHeight = 0x40000067, // WaterHeight
    WaterType = 0x40000068, // WaterType
    DistanceFogType = 0x40000069, // DistanceFogType

    UseSharedRep = 0x4000006B, // UseSharedRep
    ForceOutside = 0x4000006C, // ForceOutside

    Encounter_Block = 0x4000006E, // Encounter-Block
    MissileEthereal = 0x4000006F, // MissileEthereal

    MaximumDegradeLevel = 0x40000071, // MaximumDegradeLevel

    Physics_VelScale = 0x40000073, // Physics_VelScale
    Physics_AccScale = 0x40000074, // Physics_AccScale
    Physics_CombatAnimScale = 0x40000075, // Physics_CombatAnimScale
    Physics_JumpScale = 0x40000076, // Physics_JumpScale
    NeverHousekeep = 0x40000077, // NeverHousekeep
    PlacesDatType = 0x40000078, // PlacesDatType
    LinkColor = 0x40000079, // LinkColor
    LinkName = 0x4000007A, // LinkName

    Usage_MinLevel = 0x41000001, // Usage_MinLevel
    Usage_MaxLevel = 0x41000002, // Usage_MaxLevel

    OpenDoor = 0x41000008, // OpenDoor

    Skill_Parent = 0x41000016, // Skill_Parent
    Skill_Prereq = 0x41000017, // Skill_Prereq

    Skill_ZeroVigorModifier = 0x4100001B, // Skill_ZeroVigorModifier

    Skill_NumHooks = 0x4100001D, // Skill_NumHooks
    GeneratorProfile = 0x4100001E, // GeneratorProfile
    GeneratorRegenPeriod = 0x4100001F, // GeneratorRegenPeriod
    GeneratorInitialDelay = 0x41000020, // GeneratorInitialDelay
    GeneratorMinQuantityOverride = 0x41000021, // GeneratorMinQuantityOverride
    GeneratorMaxQuantityOverride = 0x41000022, // GeneratorMaxQuantityOverride

    CreatureEthereal = 0x4100002C, // CreatureEthereal
    PlayerEthereal = 0x4100002D, // PlayerEthereal
    NPCEthereal = 0x4100002E, // NPCEthereal
    AdminEthereal = 0x4100002F, // AdminEthereal

    GeneratorNonPersonalizable = 0x41000032, // GeneratorNonPersonalizable
    GeneratorRegenAllIfUnbound = 0x41000033, // GeneratorRegenAllIfUnbound
    GeneratorEnterWorldPreserve = 0x41000034, // GeneratorEnterWorldPreserve
    GeneratorMunge = 0x41000035, // GeneratorMunge
    GeneratorDontMunge = 0x41000036, // GeneratorDontMunge
    GeneratorAbsoluteQuantity = 0x41000037, // GeneratorAbsoluteQuantity
    GeneratorCheckpoint = 0x41000038, // GeneratorCheckpoint
    GeneratorDontCheckpoint = 0x41000039, // GeneratorDontCheckpoint

    Skill_Passive = 0x4100003C, // Skill_Passive
    Skill_MinVigor = 0x4100003D, // Skill_MinVigor
    Skill_UsePrereq = 0x4100003E, // Skill_UsePrereq
    Skill_RequiredWieldedItem = 0x4100003F, // Skill_RequiredWieldedItem

    Skill_Description = 0x41000042, // Skill_Description

    Skill_MinCharacterLevel = 0x41000044, // Skill_MinCharacterLevel
    Skill_AllowedSpecies = 0x41000045, // Skill_AllowedSpecies
    Skill_AllowedFactions = 0x41000046, // Skill_AllowedFactions
    Skill_RequiredWormItem = 0x41000047, // Skill_RequiredWormItem
    Skill_RequiredEffect = 0x41000048, // Skill_RequiredEffect
    Skill_UseWhileMoving = 0x41000049, // Skill_UseWhileMoving
    Skill_ResetTime = 0x4100004A, // Skill_ResetTime
    Skill_PowerupTime = 0x4100004B, // Skill_PowerupTime
    Skill_IsHidden = 0x4100004C, // Skill_IsHidden
    Skill_RecoveryTime = 0x4100004D, // Skill_RecoveryTime
    Skill_ShieldModifier = 0x4100004E, // Skill_ShieldModifier
    Skill_Barring = 0x4100004F, // Skill_Barring
    Skill_CreditCost = 0x41000050, // Skill_CreditCost
    Skill_IsUntrainable = 0x41000051, // Skill_IsUntrainable
    Skill_AdvancementTable = 0x41000052, // Skill_AdvancementTable
    Skill_AdvancementModifier = 0x41000053, // Skill_AdvancementModifier
    Skill_AdvancementCap = 0x41000054, // Skill_AdvancementCap
    Skill_LevelWhenTrained = 0x41000055, // Skill_LevelWhenTrained
    Skill_MultVigorModifier = 0x41000056, // Skill_MultVigorModifier
    Skill_RequiresMoveTo = 0x41000057, // Skill_RequiresMoveTo
    Skill_RequiresTurnTo = 0x41000058, // Skill_RequiresTurnTo
    Skill_MinHealth = 0x41000059, // Skill_MinHealth
    Skill_MaxHealth = 0x4100005A, // Skill_MaxHealth
    Skill_TargetEffect = 0x4100005B, // Skill_TargetEffect
    Skill_MaxVigor = 0x4100005C, // Skill_MaxVigor
    Skill_MinRange = 0x4100005D, // Skill_MinRange
    Skill_MaxRange = 0x4100005E, // Skill_MaxRange
    Skill_UserEffect = 0x4100005F, // Skill_UserEffect

    Skill_BarringWieldedItem = 0x41000061, // Skill_BarringWieldedItem
    NeutralFactionEthereal = 0x41000062, // NeutralFactionEthereal
    Skill_BarringWornItem = 0x41000063, // Skill_BarringWornItem
    Faction1Ethereal = 0x41000064, // Faction1Ethereal
    Skill_BarringEffect = 0x41000065, // Skill_BarringEffect
    Skill_AllowedImplementsForRightHand = 0x41000066, // Skill_AllowedImplementsForRightHand
    Skill_AllowedImplementsForLeftHand = 0x41000067, // Skill_AllowedImplementsForLeftHand
    Faction2Ethereal = 0x41000068, // Faction2Ethereal
    Faction3Ethereal = 0x41000069, // Faction3Ethereal
    Item_EnglishName = 0x4100006A, // Item_EnglishName
    Skill_EnglishName = 0x4100006B, // Skill_EnglishName
    Skill_MinUseTime = 0x4100006C, // Skill_MinUseTime
    Skill_AttackTimeModifier = 0x4100006D, // Skill_AttackTimeModifier
    Skill_ArmorModifier = 0x4100006E, // Skill_ArmorModifier
    Skill_Hook_IsPrimaryImplement = 0x4100006F, // Skill_Hook_IsPrimaryImplement
    Skill_AddVigorModifier = 0x41000070, // Skill_AddVigorModifier
    Skill_Hook_IsSecondaryImplement = 0x41000071, // Skill_Hook_IsSecondaryImplement
    Skill_AbilityLevelModifier = 0x41000072, // Skill_AbilityLevelModifier
    Skill_IgnoreWeaponTime = 0x41000073, // Skill_IgnoreWeaponTime
    Skill_IgnoreArmorDelay = 0x41000074, // Skill_IgnoreArmorDelay
    Skill_IgnoreWeaponOffense = 0x41000075, // Skill_IgnoreWeaponOffense
    Skill_IgnoreWeaponDefense = 0x41000076, // Skill_IgnoreWeaponDefense
    Skill_IgnoreWeaponVigor = 0x41000077, // Skill_IgnoreWeaponVigor
    Skill_IgnoreWeaponDamage = 0x41000078, // Skill_IgnoreWeaponDamage
    Skill_IgnoreShieldVigor = 0x41000079, // Skill_IgnoreShieldVigor
    Skill_IgnoreShieldDelay = 0x4100007A, // Skill_IgnoreShieldDelay
    Skill_IsNonDamaging = 0x4100007B, // Skill_IsNonDamaging
    Skill_BasicFightingWeights = 0x4100007C, // Skill_BasicFightingWeights
    Skill_IsAutoAttack = 0x4100007D, // Skill_IsAutoAttack
    Skill_Hook_IsShieldAllowed = 0x4100007E, // Skill_Hook_IsShieldAllowed
    Skill_Hook_IsIntersection = 0x4100007F, // Skill_Hook_IsIntersection
    Skill_Hook_IsDistance = 0x41000080, // Skill_Hook_IsDistance
    Skill_Hook_IsProjectile = 0x41000081, // Skill_Hook_IsProjectile
    Skill_IsHarmful = 0x41000082, // Skill_IsHarmful
    Skill_CriticalThreshold = 0x41000083, // Skill_CriticalThreshold
    Skill_CriticalModifier = 0x41000084, // Skill_CriticalModifier

    Item_Description = 0x41000086, // Item_Description
    Item_IsWieldable = 0x41000087, // Item_IsWieldable
    Item_IsStackable = 0x41000088, // Item_IsStackable
    Item_MaxStackSize = 0x41000089, // Item_MaxStackSize
    Item_PreferredInventoryLocation = 0x4100008A, // Item_PreferredInventoryLocation
    Item_ValidInventoryLocations = 0x4100008B, // Item_ValidInventoryLocations
    Item_PrecludedInventoryLocations = 0x4100008C, // Item_PrecludedInventoryLocations
    Item_PrimaryParentingLocation = 0x4100008D, // Item_PrimaryParentingLocation
    Item_SecondaryParentingLocation = 0x4100008E, // Item_SecondaryParentingLocation
    Item_ImplementType = 0x4100008F, // Item_ImplementType
    Item_MinLevel = 0x41000090, // Item_MinLevel
    Item_MaxLevel = 0x41000091, // Item_MaxLevel
    Item_FactionRequired = 0x41000092, // Item_FactionRequired
    Item_RequiredSkill1 = 0x41000093, // Item_RequiredSkill1
    Item_RequiredSkill2 = 0x41000094, // Item_RequiredSkill2
    Item_RestrictedSkill1 = 0x41000095, // Item_RestrictedSkill1
    Item_RestrictedSkill2 = 0x41000096, // Item_RestrictedSkill2
    Item_RequiredRace = 0x41000097, // Item_RequiredRace
    Item_RequiredQuest = 0x41000098, // Item_RequiredQuest
    Item_RequiredQuestStatus = 0x41000099, // Item_RequiredQuestStatus

    Item_Value = 0x4100009C, // Item_Value
    Item_IsDestroyOnSell = 0x4100009D, // Item_IsDestroyOnSell

    Item_EquipperEffects = 0x410000A4, // Item_EquipperEffects
    Item_UsageEffects = 0x410000A5, // Item_UsageEffects
    Item_TargetEffects = 0x410000A6, // Item_TargetEffects
    Item_ArmorLevel = 0x410000A7, // Item_ArmorLevel
    Item_CombatDelay = 0x410000A8, // Item_CombatDelay
    Item_VigorCost = 0x410000A9, // Item_VigorCost
    Item_Damage = 0x410000AA, // Item_Damage
    Item_Variance = 0x410000AB, // Item_Variance
    Item_WeaponTime = 0x410000AC, // Item_WeaponTime
    Item_IsHarmless = 0x410000AD, // Item_IsHarmless
    Item_OffenseModifier = 0x410000AE, // Item_OffenseModifier
    Skill_Hook_AttackAreaRange = 0x410000AF, // Skill_Hook_AttackAreaRange
    Skill_Hook_IsAreaOfEffect = 0x410000B0, // Skill_Hook_IsAreaOfEffect
    Skill_Hook_RequiresLOS = 0x410000B1, // Skill_Hook_RequiresLOS
    Skill_Hook_DetonatesOnMiss = 0x410000B2, // Skill_Hook_DetonatesOnMiss
    Skill_Hook_AddDamage = 0x410000B3, // Skill_Hook_AddDamage
    Skill_Hook_MultDamage = 0x410000B4, // Skill_Hook_MultDamage
    Item_IsOpen = 0x410000B5, // Item_IsOpen
    Item_IsLocked = 0x410000B6, // Item_IsLocked
    Item_MinUseDist = 0x410000B7, // Item_MinUseDist

    Item_CanUseWhileMoving = 0x410000B9, // Item_CanUseWhileMoving
    Item_CanUseOnlyInInventory = 0x410000BA, // Item_CanUseOnlyInInventory
    Item_CanUseWhenCollided = 0x410000BB, // Item_CanUseWhenCollided
    Item_IsLockOnUse = 0x410000BC, // Item_IsLockOnUse
    Item_IsTargetedUsageItem = 0x410000BD, // Item_IsTargetedUsageItem
    Item_MoveToTarget = 0x410000BE, // Item_MoveToTarget
    Item_ValidTargetTypes = 0x410000BF, // Item_ValidTargetTypes
    Item_HealthCost = 0x410000C0, // Item_HealthCost
    Item_TimeBetweenUses = 0x410000C1, // Item_TimeBetweenUses
    Item_Lockable = 0x410000C2, // Item_Lockable
    Item_LockOnClose = 0x410000C3, // Item_LockOnClose
    Item_OpenOnUnlock = 0x410000C4, // Item_OpenOnUnlock
    Item_IsAttunable = 0x410000C5, // Item_IsAttunable
    Item_KeyID = 0x410000C6, // Item_KeyID
    Item_IsUsable = 0x410000C7, // Item_IsUsable
    Item_IsSelectable = 0x410000C8, // Item_IsSelectable
    Item_IsTakeable = 0x410000C9, // Item_IsTakeable
    Item_IsDestroyOnUse = 0x410000CA, // Item_IsDestroyOnUse

    Item_MapDestination = 0x410000CC, // Item_MapDestination
    Item_LocDestination = 0x410000CD, // Item_LocDestination
    NPC_EnglishName = 0x410000CE, // NPC_EnglishName
    NPC_Description = 0x410000CF, // NPC_Description

    NPC_AISuperClass = 0x410000D1, // NPC_AISuperClass
    NPC_AISubClass = 0x410000D2, // NPC_AISubClass
    NPC_DoesAgentDestroyOnUse = 0x410000D3, // NPC_DoesAgentDestroyOnUse
    NPC_AICanUseDoors = 0x410000D4, // NPC_AICanUseDoors
    NPC_AreAllItemsDestroyedOnRot = 0x410000D5, // NPC_AreAllItemsDestroyedOnRot
    NPC_IsLootProof = 0x410000D6, // NPC_IsLootProof
    NPC_LootTimer = 0x410000D7, // NPC_LootTimer
    NPC_BaseRotTime = 0x410000D8, // NPC_BaseRotTime
    NPC_MaxAcceleratedRotTime = 0x410000D9, // NPC_MaxAcceleratedRotTime
    NPC_MinAcceleratedRotTime = 0x410000DA, // NPC_MinAcceleratedRotTime
    NPC_Level = 0x410000DB, // NPC_Level
    NPC_DeathExperience = 0x410000DC, // NPC_DeathExperience
    NPC_IsAcceptingItems = 0x410000DD, // NPC_IsAcceptingItems
    NPC_ArmorLevel = 0x410000DE, // NPC_ArmorLevel

    NPC_Skills = 0x410000E0, // NPC_Skills
    NPC_Health = 0x410000E1, // NPC_Health
    NPC_Vigor = 0x410000E2, // NPC_Vigor

    NPC_Inventory = 0x410000E5, // NPC_Inventory
    NPC_HomesickRadius = 0x410000E6, // NPC_HomesickRadius
    NPC_PerceptionRadius = 0x410000E7, // NPC_PerceptionRadius
    NPC_IsWandering = 0x410000E8, // NPC_IsWandering
    NPC_MaxWanderRange = 0x410000E9, // NPC_MaxWanderRange
    NPC_FactionMembership = 0x410000EA, // NPC_FactionMembership
    NPC_IsFactionBasedOnLandblock = 0x410000EB, // NPC_IsFactionBasedOnLandblock
    NPC_IsFactionOwnershipChangeOnDeath = 0x410000EC, // NPC_IsFactionOwnershipChangeOnDeath
    NPC_CanWieldImplements = 0x410000ED, // NPC_CanWieldImplements
    NPC_CanUseWeaponAndShield = 0x410000EE, // NPC_CanUseWeaponAndShield
    NPC_CanDualWield = 0x410000EF, // NPC_CanDualWield
    NPC_NaturalDamage = 0x410000F0, // NPC_NaturalDamage
    NPC_NaturalVariance = 0x410000F1, // NPC_NaturalVariance
    NPC_NaturalVigorCost = 0x410000F2, // NPC_NaturalVigorCost
    NPC_NaturalWeaponTime = 0x410000F3, // NPC_NaturalWeaponTime
    NPC_NaturalOffenseModifier = 0x410000F4, // NPC_NaturalOffenseModifier
    Item_FullDestination = 0x410000F5, // Item_FullDestination
    NPC_ChallengeLevel = 0x410000F6, // NPC_ChallengeLevel
    NPC_CanWieldTwoHanded = 0x410000F7, // NPC_CanWieldTwoHanded
    Effect_EnglishName = 0x410000F8, // Effect_EnglishName
    Effect_Description = 0x410000F9, // Effect_Description
    Effect_Duration = 0x410000FA, // Effect_Duration
    Effect_ProbabilityOfApplication = 0x410000FB, // Effect_ProbabilityOfApplication
    Effect_TsysName = 0x410000FC, // Effect_TsysName
    Effect_EquivalenceClass = 0x410000FD, // Effect_EquivalenceClass
    Effect_ClassPriority = 0x410000FE, // Effect_ClassPriority
    Effect_IsClientVisible = 0x410000FF, // Effect_IsClientVisible
    Effect_IsHarmful = 0x41000100, // Effect_IsHarmful
    Effect_IsRemoveOnDeath = 0x41000101, // Effect_IsRemoveOnDeath
    Effect_IsFactionEffect = 0x41000102, // Effect_IsFactionEffect
    Effect_IsHeartbeat = 0x41000103, // Effect_IsHeartbeat
    Effect_IsChainBreaker = 0x41000104, // Effect_IsChainBreaker
    Effect_IsVitalChangeInterested = 0x41000105, // Effect_IsVitalChangeInterested
    Effect_IsEffectApplicationInterested = 0x41000106, // Effect_IsEffectApplicationInterested
    Effect_IsInstantaneous = 0x41000107, // Effect_IsInstantaneous
    Effect_MinLevel = 0x41000108, // Effect_MinLevel
    Effect_MaxLevel = 0x41000109, // Effect_MaxLevel
    Effect_FactionRequired = 0x4100010A, // Effect_FactionRequired
    Effect_RequiredSkill1 = 0x4100010B, // Effect_RequiredSkill1
    Effect_RequiredSkill2 = 0x4100010C, // Effect_RequiredSkill2
    Effect_RestrictedSkill1 = 0x4100010D, // Effect_RestrictedSkill1
    Effect_RestrictedSkill2 = 0x4100010E, // Effect_RestrictedSkill2
    Effect_RequiredRace = 0x4100010F, // Effect_RequiredRace
    Effect_Trait = 0x41000110, // Effect_Trait
    Effect_TraitAmount = 0x41000111, // Effect_TraitAmount
    Effect_UncommonTrait = 0x41000112, // Effect_UncommonTrait
    Effect_MinTsysSpellcraft = 0x41000113, // Effect_MinTsysSpellcraft
    Effect_MaxTsysSpellcraft = 0x41000114, // Effect_MaxTsysSpellcraft
    Effect_TsysValue = 0x41000115, // Effect_TsysValue
    Item_MinimumRank = 0x41000116, // Item_MinimumRank
    Item_MaximumRank = 0x41000117, // Item_MaximumRank
    Item_NonAllegianceOnly = 0x41000118, // Item_NonAllegianceOnly
    Item_MonarchOnly = 0x41000119, // Item_MonarchOnly
    Craft_Recipe_EnglishName = 0x4100011A, // Craft_Recipe_EnglishName
    Craft_Recipe_Description = 0x4100011B, // Craft_Recipe_Description

    Craft_Recipe_Difficulty = 0x4100011E, // Craft_Recipe_Difficulty
    Craft_Recipe_MaxSuccesses = 0x4100011F, // Craft_Recipe_MaxSuccesses
    Craft_Recipe_Charges = 0x41000120, // Craft_Recipe_Charges
    Craft_Recipe_RecoveryTime = 0x41000121, // Craft_Recipe_RecoveryTime
    Craft_Recipe_SkillBonus = 0x41000122, // Craft_Recipe_SkillBonus
    Craft_Recipe_NumIngredients = 0x41000123, // Craft_Recipe_NumIngredients
    Craft_Recipe_Ingredients = 0x41000124, // Craft_Recipe_Ingredients
    Craft_Recipe_MasteryActions = 0x41000125, // Craft_Recipe_MasteryActions
    Craft_Recipe_CraftCheckEntries = 0x41000126, // Craft_Recipe_CraftCheckEntries
    Skill_AttackerSuccessString = 0x41000127, // Skill_AttackerSuccessString
    Skill_AttackerFailureString = 0x41000128, // Skill_AttackerFailureString
    Skill_AttackerCriticalString = 0x41000129, // Skill_AttackerCriticalString
    Skill_DefenderSuccessString = 0x4100012A, // Skill_DefenderSuccessString
    Skill_DefenderFailureString = 0x4100012B, // Skill_DefenderFailureString
    Skill_DefenderCriticalString = 0x4100012C, // Skill_DefenderCriticalString
    NPC_LoveTable = 0x4100012D, // NPC_LoveTable

    Usage_RequiredFaction = 0x41000131, // Usage_RequiredFaction
    Usage_RequiredRace = 0x41000132, // Usage_RequiredRace
    Usage_RequiredQuest = 0x41000133, // Usage_RequiredQuest
    Usage_RequiredQuestStatus = 0x41000134, // Usage_RequiredQuestStatus
    Usage_RequiredSkill1 = 0x41000135, // Usage_RequiredSkill1
    Usage_RequiredSkill2 = 0x41000136, // Usage_RequiredSkill2
    Usage_RestrictedSkill1 = 0x41000137, // Usage_RestrictedSkill1
    Usage_RestrictedSkill2 = 0x41000138, // Usage_RestrictedSkill2
    Usage_MinRank = 0x41000139, // Usage_MinRank
    Usage_MaxRank = 0x4100013A, // Usage_MaxRank
    Usage_NonAllegianceOnly = 0x4100013B, // Usage_NonAllegianceOnly
    Usage_MonarchOnly = 0x4100013C, // Usage_MonarchOnly
    Usage_LandblockFactionRequired = 0x4100013D, // Usage_LandblockFactionRequired
    Lock_Lockable = 0x4100013E, // Lock_Lockable
    Lock_OpenOnUnlock = 0x4100013F, // Lock_OpenOnUnlock
    Lock_LockOnClose = 0x41000140, // Lock_LockOnClose
    Lock_AICanIgnoreLock = 0x41000141, // Lock_AICanIgnoreLock
    Lock_KeyID = 0x41000142, // Lock_KeyID
    Name = 0x41000143, // Name
    Description = 0x41000144, // Description
    Generator_EnglishName = 0x41000145, // Generator_EnglishName
    Quest_BestowedSceneID = 0x41000146, // Quest_BestowedSceneID
    Usage_Effect1 = 0x41000147, // Usage_Effect1
    Usage_Effect1_Spellcraft = 0x41000148, // Usage_Effect1_Spellcraft
    Usage_Effect2 = 0x41000149, // Usage_Effect2
    Usage_Effect2_Spellcraft = 0x4100014A, // Usage_Effect2_Spellcraft

    Quest_ContentDescription = 0x4100014C, // Quest_ContentDescription
    Quest_ExamineDescription = 0x4100014D, // Quest_ExamineDescription
    Quest_FailOnLogout = 0x4100014E, // Quest_FailOnLogout
    Quest_FailOnDeath = 0x4100014F, // Quest_FailOnDeath
    Quest_FellowshipPropagate = 0x41000150, // Quest_FellowshipPropagate
    Quest_Active = 0x41000151, // Quest_Active
    Quest_Phases = 0x41000152, // Quest_Phases
    Quest_Solutions = 0x41000153, // Quest_Solutions
    Quest_Duration = 0x41000154, // Quest_Duration
    Quest_Retry = 0x41000155, // Quest_Retry
    Quest_CompletionXP = 0x41000156, // Quest_CompletionXP
    Quest_ChallengeLevel = 0x41000157, // Quest_ChallengeLevel
    Quest_Points = 0x41000158, // Quest_Points
    Quest_LevelMinimum = 0x41000159, // Quest_LevelMinimum
    Quest_LevelMaximum = 0x4100015A, // Quest_LevelMaximum
    Quest_Faction = 0x4100015B, // Quest_Faction
    Quest_RequiredSkill1 = 0x4100015C, // Quest_RequiredSkill1
    Quest_RequiredSkill2 = 0x4100015D, // Quest_RequiredSkill2
    Quest_RestrictedSkill1 = 0x4100015E, // Quest_RestrictedSkill1
    Quest_RestrictedSkill2 = 0x4100015F, // Quest_RestrictedSkill2
    Quest_RequiredRace = 0x41000160, // Quest_RequiredRace
    Quest_RequiredQuest = 0x41000161, // Quest_RequiredQuest
    Quest_RequiredQuestStatus = 0x41000162, // Quest_RequiredQuestStatus
    Quest_RankMinimum = 0x41000163, // Quest_RankMinimum
    Quest_RankMaximum = 0x41000164, // Quest_RankMaximum
    Quest_MonarchOnly = 0x41000165, // Quest_MonarchOnly
    Quest_NonAllegianceOnly = 0x41000166, // Quest_NonAllegianceOnly
    Quest_RequiredLandblockFaction = 0x41000167, // Quest_RequiredLandblockFaction
    Usage_Effect3 = 0x41000168, // Usage_Effect3
    Usage_Effect3_Spellcraft = 0x41000169, // Usage_Effect3_Spellcraft
    Usage_Effect4 = 0x4100016A, // Usage_Effect4
    Usage_Effect4_Spellcraft = 0x4100016B, // Usage_Effect4_Spellcraft
    Usage_Effect5 = 0x4100016C, // Usage_Effect5
    Usage_Effect5_Spellcraft = 0x4100016D, // Usage_Effect5_Spellcraft
    GameplayStatistics_MaxHealth = 0x4100016E, // GameplayStatistics_MaxHealth
    GameplayStatistics_HealthRegenRate = 0x4100016F, // GameplayStatistics_HealthRegenRate
    GameplayStatistics_MaxVigor = 0x41000170, // GameplayStatistics_MaxVigor
    GameplayStatistics_VigorRegenRate = 0x41000171, // GameplayStatistics_VigorRegenRate
    GameplayStatistics_DeathExperience = 0x41000172, // GameplayStatistics_DeathExperience
    GameplayStatistics_Level = 0x41000173, // GameplayStatistics_Level
    Mine_Recipe = 0x41000174, // Mine_Recipe
    Mine_Trait = 0x41000175, // Mine_Trait
    Mine_TraitAmount = 0x41000176, // Mine_TraitAmount
    Craft_MineDifficulty = 0x41000177, // Craft_MineDifficulty
    AI_HomesickRadius = 0x41000178, // AI_HomesickRadius
    AI_CanJoinCliques = 0x41000179, // AI_CanJoinCliques
    AI_PerceptionRadius = 0x4100017A, // AI_PerceptionRadius
    AI_MovementType = 0x4100017B, // AI_MovementType
    AI_FactionMembership = 0x4100017C, // AI_FactionMembership
    AI_FactionMembershipBasedOnLandblock = 0x4100017D, // AI_FactionMembershipBasedOnLandblock
    AI_FactionOwnershipChangeOnDeath = 0x4100017E, // AI_FactionOwnershipChangeOnDeath
    AI_FreeAttacking = 0x4100017F, // AI_FreeAttacking
    AICombat_WieldImplements = 0x41000180, // AICombat_WieldImplements
    AICombat_UseWeaponAndShield = 0x41000181, // AICombat_UseWeaponAndShield
    AICombat_CanDualWield = 0x41000182, // AICombat_CanDualWield
    AICombat_CanWieldTwoHanded = 0x41000183, // AICombat_CanWieldTwoHanded
    AICombat_NaturalDamage = 0x41000184, // AICombat_NaturalDamage
    AICombat_NaturalVariance = 0x41000185, // AICombat_NaturalVariance
    AICombat_NaturalVigorCost = 0x41000186, // AICombat_NaturalVigorCost
    AICombat_NaturalWeaponTime = 0x41000187, // AICombat_NaturalWeaponTime
    AICombat_NaturalOffenseModifier = 0x41000188, // AICombat_NaturalOffenseModifier
    AI_NPCTable = 0x41000189, // AI_NPCTable
    AI_LoveTable = 0x4100018A, // AI_LoveTable
    AI_Inventory1 = 0x4100018B, // AI_Inventory1
    AI_Inventory2 = 0x4100018C, // AI_Inventory2
    AI_Inventory3 = 0x4100018D, // AI_Inventory3
    PortalFlags = 0x4100018E, // PortalFlags
    MaximumUpkeepPoints = 0x4100018F, // MaximumUpkeepPoints
    ResetInterval = 0x41000190, // ResetInterval
    FuelType = 0x41000191, // FuelType
    SpellcraftModifier = 0x41000192, // SpellcraftModifier
    Usage_RequiredSkill1Rating = 0x41000193, // Usage_RequiredSkill1Rating
    Usage_RequiredSkill2Rating = 0x41000194, // Usage_RequiredSkill2Rating
    Lock_IsLocked = 0x41000195, // Lock_IsLocked
    Weapon_Damage = 0x41000196, // Weapon_Damage
    Weapon_Variance = 0x41000197, // Weapon_Variance
    Weapon_VigorCost = 0x41000198, // Weapon_VigorCost
    Weapon_Speed = 0x41000199, // Weapon_Speed
    Weapon_OffenseMod = 0x4100019A, // Weapon_OffenseMod
    Weapon_SingleWeaponStance = 0x4100019B, // Weapon_SingleWeaponStance
    Weapon_WithShieldStance = 0x4100019C, // Weapon_WithShieldStance
    Weapon_DualWieldStance = 0x4100019D, // Weapon_DualWieldStance
    Weapon_Harmless = 0x4100019E, // Weapon_Harmless
    AI_Wandering = 0x4100019F, // AI_Wandering
    AI_WanderingRange = 0x410001A0, // AI_WanderingRange
    AI_WanderingProb = 0x410001A1, // AI_WanderingProb
    AI_CanUseDoors = 0x410001A2, // AI_CanUseDoors
    AI_DetectionSpheres = 0x410001A3, // AI_DetectionSpheres
    AI_AISubClass = 0x410001A4, // AI_AISubClass
    AI_AISuperClass = 0x410001A5, // AI_AISuperClass
    NPC_DeathLootProfile = 0x410001A6, // NPC_DeathLootProfile
    NPC_DeathRemoveLootProfile = 0x410001A7, // NPC_DeathRemoveLootProfile

    NPC_DeathEffect1 = 0x410001A9, // NPC_DeathEffect1
    NPC_DeathEffect1Stat = 0x410001AA, // NPC_DeathEffect1Stat
    NPC_DeathEffect2 = 0x410001AB, // NPC_DeathEffect2
    NPC_DeathEffect2Stat = 0x410001AC, // NPC_DeathEffect2Stat
    NPC_DeathEffect3 = 0x410001AD, // NPC_DeathEffect3
    NPC_DeathEffect3Stat = 0x410001AE, // NPC_DeathEffect3Stat
    NPC_GrooveLevel = 0x410001AF, // NPC_GrooveLevel

    NPC_StartingSkill1 = 0x410001BA, // NPC_StartingSkill1

    NPC_StartingSkill2 = 0x410001BC, // NPC_StartingSkill2

    NPC_StartingSkill3 = 0x410001BE, // NPC_StartingSkill3

    NPC_StartingSkill4 = 0x410001C0, // NPC_StartingSkill4

    NPC_StartingSkill5 = 0x410001C2, // NPC_StartingSkill5

    NPC_TSysCoarseItemClass = 0x410001C4, // NPC_TSysCoarseItemClass
    NPC_TSysFineItemClass = 0x410001C5, // NPC_TSysFineItemClass

    NPC_UnlockedAfterFirstLoot = 0x410001C8, // NPC_UnlockedAfterFirstLoot

    NPC_StartingSkill6 = 0x410001D3, // NPC_StartingSkill6

    NPC_StartingSkill7 = 0x410001D5, // NPC_StartingSkill7

    NPC_StartingSkill8 = 0x410001D7, // NPC_StartingSkill8

    NPC_StartingSkill9 = 0x410001D9, // NPC_StartingSkill9

    NPC_StartingSkill10 = 0x410001DB, // NPC_StartingSkill10

    AI_Inventory1Quantity = 0x410001DD, // AI_Inventory1Quantity
    AI_Inventory2Quantity = 0x410001DE, // AI_Inventory2Quantity
    AI_Inventory3Quantity = 0x410001DF, // AI_Inventory3Quantity

    NPC_StartingSkill11 = 0x410001E0, // NPC_StartingSkill11

    NPC_StartingSkill12 = 0x410001E2, // NPC_StartingSkill12

    NPC_StartingSkill13 = 0x410001E4, // NPC_StartingSkill13

    NPC_StartingSkill14 = 0x410001E6, // NPC_StartingSkill14

    NPC_StartingSkill15 = 0x410001E8, // NPC_StartingSkill15

    NPC_StartingSkill16 = 0x410001EA, // NPC_StartingSkill16

    NPC_StartingSkill17 = 0x410001EC, // NPC_StartingSkill17

    NPC_StartingSkill18 = 0x410001EE, // NPC_StartingSkill18

    NPC_StartingSkill19 = 0x410001F0, // NPC_StartingSkill19

    NPC_StartingSkill20 = 0x410001F2, // NPC_StartingSkill20

    NPC_StartingSkill1Level = 0x410001F4, // NPC_StartingSkill1Level
    NPC_StartingSkill2Level = 0x410001F5, // NPC_StartingSkill2Level
    NPC_StartingSkill3Level = 0x410001F6, // NPC_StartingSkill3Level
    NPC_StartingSkill4Level = 0x410001F7, // NPC_StartingSkill4Level
    NPC_StartingSkill5Level = 0x410001F8, // NPC_StartingSkill5Level
    NPC_StartingSkill6Level = 0x410001F9, // NPC_StartingSkill6Level
    NPC_StartingSkill7Level = 0x410001FA, // NPC_StartingSkill7Level
    NPC_StartingSkill8Level = 0x410001FB, // NPC_StartingSkill8Level
    NPC_StartingSkill9Level = 0x410001FC, // NPC_StartingSkill9Level
    NPC_StartingSkill10Level = 0x410001FD, // NPC_StartingSkill10Level
    NPC_StartingSkill11Level = 0x410001FE, // NPC_StartingSkill11Level
    NPC_StartingSkill12Level = 0x410001FF, // NPC_StartingSkill12Level
    NPC_StartingSkill13Level = 0x41000200, // NPC_StartingSkill13Level
    NPC_StartingSkill14Level = 0x41000201, // NPC_StartingSkill14Level
    NPC_StartingSkill15Level = 0x41000202, // NPC_StartingSkill15Level
    NPC_StartingSkill16Level = 0x41000203, // NPC_StartingSkill16Level
    NPC_StartingSkill17Level = 0x41000204, // NPC_StartingSkill17Level
    NPC_StartingSkill18Level = 0x41000205, // NPC_StartingSkill18Level
    NPC_StartingSkill19Level = 0x41000206, // NPC_StartingSkill19Level
    NPC_StartingSkill20Level = 0x41000207, // NPC_StartingSkill20Level

    Icon = 0x4100020A, // Icon
    PhysObj = 0x4100020B, // PhysObj
    Placeable = 0x4100020C, // Placeable
    NPC_IsGroupMonster = 0x4100020D, // NPC_IsGroupMonster
    Item_ValidInventoryLocation1 = 0x4100020E, // Item_ValidInventoryLocation1
    Item_ValidInventoryLocation2 = 0x4100020F, // Item_ValidInventoryLocation2
    Item_PrecludedInventoryLocation1 = 0x41000210, // Item_PrecludedInventoryLocation1
    Item_PrecludedInventoryLocation2 = 0x41000211, // Item_PrecludedInventoryLocation2
    Item_PrecludedInventoryLocation3 = 0x41000212, // Item_PrecludedInventoryLocation3
    Item_PrecludedInventoryLocation4 = 0x41000213, // Item_PrecludedInventoryLocation4
    Item_ParentingOrientation = 0x41000214, // Item_ParentingOrientation
    Container_MaxContainerSize = 0x41000215, // Container_MaxContainerSize
    Container_ContainedWaitOn = 0x41000216, // Container_ContainedWaitOn
    Container_ManagedGenerator = 0x41000217, // Container_ManagedGenerator
    Container_MaxItems = 0x41000218, // Container_MaxItems
    Scale = 0x41000219, // Scale
    Item_AcquireEffect1 = 0x4100021A, // Item_AcquireEffect1
    Item_AcquireEffect1Stat = 0x4100021B, // Item_AcquireEffect1Stat
    Item_AcquireEffect2 = 0x4100021C, // Item_AcquireEffect2
    Item_AcquireEffect2Stat = 0x4100021D, // Item_AcquireEffect2Stat
    Item_AcquireEffect3 = 0x4100021E, // Item_AcquireEffect3
    Item_AcquireEffect3Stat = 0x4100021F, // Item_AcquireEffect3Stat
    Item_EquipperEffect1 = 0x41000220, // Item_EquipperEffect1
    Item_EquipperEffect1Stat = 0x41000221, // Item_EquipperEffect1Stat
    Item_EquipperEffect2 = 0x41000222, // Item_EquipperEffect2
    Item_EquipperEffect2Stat = 0x41000223, // Item_EquipperEffect2Stat
    Item_EquipperEffect3 = 0x41000224, // Item_EquipperEffect3
    Item_EquipperEffect3Stat = 0x41000225, // Item_EquipperEffect3Stat
    Item_TargetEffect1 = 0x41000226, // Item_TargetEffect1
    Item_TargetEffect1Stat = 0x41000227, // Item_TargetEffect1Stat
    Item_TargetEffect2 = 0x41000228, // Item_TargetEffect2
    Item_TargetEffect2Stat = 0x41000229, // Item_TargetEffect2Stat
    Item_TargetEffect3 = 0x4100022A, // Item_TargetEffect3
    Item_TargetEffect3Stat = 0x4100022B, // Item_TargetEffect3Stat
    Item_PluralName = 0x4100022C, // Item_PluralName

    Item_Quantity = 0x4100022F, // Item_Quantity
    Item_TSysCoarseItemClass = 0x41000230, // Item_TSysCoarseItemClass
    Item_TSysFineItemClass = 0x41000231, // Item_TSysFineItemClass
    Item_GenInternal = 0x41000232, // Item_GenInternal
    Item_GenIsAGenerator = 0x41000233, // Item_GenIsAGenerator
    Item_UsageAction = 0x41000234, // Item_UsageAction
    Item_UsageAnimation = 0x41000235, // Item_UsageAnimation
    Item_UsageAIHint1 = 0x41000236, // Item_UsageAIHint1
    Item_UsageAIHint2 = 0x41000237, // Item_UsageAIHint2
    Item_MinPermissionCheckDist = 0x41000238, // Item_MinPermissionCheckDist
    Item_UsagePermission = 0x41000239, // Item_UsagePermission

    Item_UsageTargetType = 0x4100023B, // Item_UsageTargetType

    Mobile = 0x41000242, // Mobile
    IgnoreCollisions = 0x41000243, // IgnoreCollisions
    ReportCollisions = 0x41000244, // ReportCollisions
    Item_WornAppearanceRace1 = 0x41000245, // Item_WornAppearanceRace1
    Item_WornAppearanceSex1 = 0x41000246, // Item_WornAppearanceSex1
    Item_WornAppearanceFile1 = 0x41000247, // Item_WornAppearanceFile1
    Item_WornAppearanceRace2 = 0x41000248, // Item_WornAppearanceRace2
    Item_WornAppearanceSex2 = 0x41000249, // Item_WornAppearanceSex2
    Item_WornAppearanceFile2 = 0x4100024A, // Item_WornAppearanceFile2

    Item_MaxInscriptionLength = 0x4100024C, // Item_MaxInscriptionLength
    Item_PileAppearance = 0x4100024D, // Item_PileAppearance

    Item_TakePermission = 0x4100024F, // Item_TakePermission
    Usage_AllowedRaces = 0x41000250, // Usage_AllowedRaces

    Usage_RequiredQuest_ForTaking = 0x41000257, // Usage_RequiredQuest_ForTaking
    Usage_RequiredQuestStatus_ForTaking = 0x41000258, // Usage_RequiredQuestStatus_ForTaking
    AI_ChampionMonster = 0x41000259, // AI_ChampionMonster
    AI_UniqueMonster = 0x4100025A, // AI_UniqueMonster
    AI_SpecialEffectMonster = 0x4100025B, // AI_SpecialEffectMonster
    AI_QuestMonster = 0x4100025C, // AI_QuestMonster
    NPC_StartingSkill1_IsBase = 0x4100025D, // NPC_StartingSkill1_IsBase
    NPC_StartingSkill2_IsBase = 0x4100025E, // NPC_StartingSkill2_IsBase
    NPC_StartingSkill3_IsBase = 0x4100025F, // NPC_StartingSkill3_IsBase
    NPC_StartingSkill4_IsBase = 0x41000260, // NPC_StartingSkill4_IsBase
    NPC_StartingSkill5_IsBase = 0x41000261, // NPC_StartingSkill5_IsBase
    NPC_StartingSkill6_IsBase = 0x41000262, // NPC_StartingSkill6_IsBase
    NPC_StartingSkill7_IsBase = 0x41000263, // NPC_StartingSkill7_IsBase
    NPC_StartingSkill8_IsBase = 0x41000264, // NPC_StartingSkill8_IsBase
    NPC_StartingSkill9_IsBase = 0x41000265, // NPC_StartingSkill9_IsBase
    NPC_StartingSkill10_IsBase = 0x41000266, // NPC_StartingSkill10_IsBase
    NPC_StartingSkill11_IsBase = 0x41000267, // NPC_StartingSkill11_IsBase
    NPC_StartingSkill12_IsBase = 0x41000268, // NPC_StartingSkill12_IsBase
    NPC_StartingSkill13_IsBase = 0x41000269, // NPC_StartingSkill13_IsBase
    NPC_StartingSkill14_IsBase = 0x4100026A, // NPC_StartingSkill14_IsBase
    NPC_StartingSkill15_IsBase = 0x4100026B, // NPC_StartingSkill15_IsBase
    NPC_StartingSkill16_IsBase = 0x4100026C, // NPC_StartingSkill16_IsBase
    NPC_StartingSkill17_IsBase = 0x4100026D, // NPC_StartingSkill17_IsBase
    NPC_StartingSkill18_IsBase = 0x4100026E, // NPC_StartingSkill18_IsBase
    NPC_StartingSkill19_IsBase = 0x4100026F, // NPC_StartingSkill19_IsBase
    NPC_StartingSkill20_IsBase = 0x41000270, // NPC_StartingSkill20_IsBase
    RegionChat = 0x41000271, // RegionChat
    TradeChat = 0x41000272, // TradeChat
    GeneratorQualityOverride = 0x41000273, // GeneratorQualityOverride
    GeneratorQualityVarianceOverride = 0x41000274, // GeneratorQualityVarianceOverride
    NPC_DeathLootQualityOverride = 0x41000275, // NPC_DeathLootQualityOverride
    NPC_DeathLootQualityVarianceOverride = 0x41000276, // NPC_DeathLootQualityVarianceOverride

    AI_UnwieldItemsOnIdle = 0x41000278, // AI_UnwieldItemsOnIdle
    Item_IsQuestItem = 0x41000279, // Item_IsQuestItem
    MonsterStuckEthereal = 0x4100027A, // MonsterStuckEthereal
    MoveItemDistance = 0x4100027B, // MoveItemDistance
    Usage_Animation = 0x4100027C, // Usage_Animation
    Usage_UserAnimation = 0x4100027D, // Usage_UserAnimation
    Usage_UserAnimationTimeScale = 0x4100027E, // Usage_UserAnimationTimeScale
    Usage_SuccessMessage = 0x4100027F, // Usage_SuccessMessage
    GeneratorExitWorldBehavior = 0x41000280, // GeneratorExitWorldBehavior
    AI_IdleOnly = 0x41000281, // AI_IdleOnly
    Container_IsAcceptingItems = 0x41000282, // Container_IsAcceptingItems
    GeneratorToggleStateGameEvent = 0x41000283, // GeneratorToggleStateGameEvent

    GameplayStatistics_CombatHealthRegenRate = 0x41000285, // GameplayStatistics_CombatHealthRegenRate

    GameplayStatistics_CombatVigorRegenRate = 0x41000287, // GameplayStatistics_CombatVigorRegenRate
    Item_InscribePermission = 0x41000288, // Item_InscribePermission
    Usage_CrafterOnly = 0x41000289, // Usage_CrafterOnly
    NPC_DeathEffect4 = 0x4100028A, // NPC_DeathEffect4
    NPC_DeathEffect4Stat = 0x4100028B, // NPC_DeathEffect4Stat

    PKPermission_CannotBeHarmedBySameFaction = 0x4100028E, // PKPermission_CannotBeHarmedBySameFaction
    PKPermission_CannotHarmSameFaction = 0x4100028F, // PKPermission_CannotHarmSameFaction
    EnterWorldFX = 0x41000290, // EnterWorldFX
    AI_NeverSayDie = 0x41000291, // AI_NeverSayDie
    RadarBlip = 0x41000292, // RadarBlip
    Weapon_CriticalHitMod = 0x41000293, // Weapon_CriticalHitMod
    PluralName = 0x41000294, // PluralName

    Item_IsRareItem = 0x41000296, // Item_IsRareItem

    NPC_NeverLeaveCorpse = 0x4100029A, // NPC_NeverLeaveCorpse
    Usage_HeroOnly = 0x4100029B, // Usage_HeroOnly
    Usage_NonHeroOnly = 0x4100029C, // Usage_NonHeroOnly

    Item_CraftFlags = 0x4100029F, // Item_CraftFlags
    Weapon_FocusCost = 0x410002A0, // Weapon_FocusCost
    AICombat_NaturalFocusCost = 0x410002A1, // AICombat_NaturalFocusCost
    GameplayStatistics_DeathFocus = 0x410002A2, // GameplayStatistics_DeathFocus
    Usage_ShouldUnlockUserForUsageEffects = 0x410002A3, // Usage_ShouldUnlockUserForUsageEffects
    Usage_RequiredArcaneLore = 0x410002A4, // Usage_RequiredArcaneLore
    Item_IsIncomparableItem = 0x410002A5, // Item_IsIncomparableItem
    Usage_CancelSafeModeOnUsage = 0x410002A6, // Usage_CancelSafeModeOnUsage
    AppearanceMutationKey = 0x410002A7, // AppearanceMutationKey
    AppearanceMutationKeyValue = 0x410002A8, // AppearanceMutationKeyValue
    Usage_ItemInteractionTable = 0x410002A9, // Usage_ItemInteractionTable
    NPC_SkillTargetFlags = 0x410002AA, // NPC_SkillTargetFlags
    Item_Durability_MaxLevel = 0x410002AB, // Item_Durability_MaxLevel
    Item_Durability_DecayMod = 0x410002AC, // Item_Durability_DecayMod
    Book_Source = 0x410002AD, // Book_Source
    Usage_RequiredLocation = 0x410002AE, // Usage_RequiredLocation
    Usage_RequiredLocationTsysMin = 0x410002AF, // Usage_RequiredLocationTsysMin
    Usage_RequiredLocationTsysMax = 0x410002B0, // Usage_RequiredLocationTsysMax
    Usage_ErrorMessagesTableID = 0x410002B1, // Usage_ErrorMessagesTableID
    Usage_RequiredLocationRadius = 0x410002B2, // Usage_RequiredLocationRadius
    Usage_RequiredLocationCloseProximityMsgRadius = 0x410002B3, // Usage_RequiredLocationCloseProximityMsgRadius
    Usage_RequiredLocationMediumProximityMsgRadius = 0x410002B4, // Usage_RequiredLocationMediumProximityMsgRadius
    Usage_RequiredLocationFarProximityMsgRadius = 0x410002B5, // Usage_RequiredLocationFarProximityMsgRadius
    Usage_RequiredLocationFeedbackType = 0x410002B6, // Usage_RequiredLocationFeedbackType

    Usage_RequiredLocationVeryFarProximityMsgRadius = 0x410002B8, // Usage_RequiredLocationVeryFarProximityMsgRadius
    Inscription = 0x410002B9, // Inscription
    AuthorName = 0x410002BA, // AuthorName
    RadarColor = 0x410002BB, // RadarColor
    Usage_CriticalSuccessMessage = 0x410002BC, // Usage_CriticalSuccessMessage
    Usage_RequiredCraftSkill = 0x410002BD, // Usage_RequiredCraftSkill
    Usage_RequiredCraftSkillRating = 0x410002BE, // Usage_RequiredCraftSkillRating
    Usage_SummonerOnly = 0x410002BF, // Usage_SummonerOnly
    Usage_Duration = 0x410002C0, // Usage_Duration
    ForgeEffect = 0x410002C1, // ForgeEffect
    ForgeEffectRadius = 0x410002C2, // ForgeEffectRadius
    Mine_RequiredEffect = 0x410002C3, // Mine_RequiredEffect
    CraftSkill = 0x410002C4, // CraftSkill
    Mine_Object = 0x410002C5, // Mine_Object
    Mine_ObjectQuantity = 0x410002C6, // Mine_ObjectQuantity
    Mine_ObjectQuantityVariance = 0x410002C7, // Mine_ObjectQuantityVariance
    Mine_MaxUses = 0x410002C8, // Mine_MaxUses
    Mine_UsageResetTime = 0x410002C9, // Mine_UsageResetTime
    AI_HealthWarningLevel = 0x410002CA, // AI_HealthWarningLevel
    AI_HealthAggressiveness = 0x410002CB, // AI_HealthAggressiveness
    AI_VigorWarningLevel = 0x410002CC, // AI_VigorWarningLevel
    AI_VigorAggressiveness = 0x410002CD, // AI_VigorAggressiveness
    ButcheryProfile = 0x410002CE, // ButcheryProfile
    Item_Slots = 0x410002CF, // Item_Slots
    Item_Durability_CurrentLevel = 0x410002D0, // Item_Durability_CurrentLevel
    Item_IsExtractable = 0x410002D1, // Item_IsExtractable
    Effect_IsExtractable = 0x410002D2, // Effect_IsExtractable
    Craft_Recipe_CraftSkill = 0x410002D3, // Craft_Recipe_CraftSkill
    Craft_Tool_XPMod = 0x410002D4, // Craft_Tool_XPMod
    Craft_Tool_QuantityMod = 0x410002D5, // Craft_Tool_QuantityMod
    Craft_Tool_CraftSkillMod = 0x410002D6, // Craft_Tool_CraftSkillMod
    Usage_Tool_RequiredSkillLevel = 0x410002D7, // Usage_Tool_RequiredSkillLevel
    Craft_DyePlant_Mod = 0x410002D8, // Craft_DyePlant_Mod
    Usage_UserAnimationRepeatCount = 0x410002D9, // Usage_UserAnimationRepeatCount
    NPC_BypassableArmorMod = 0x410002DA, // NPC_BypassableArmorMod
    Activation_Type = 0x410002DB, // Activation_Type
    Usage_DurabilityLostOnUse = 0x410002DC, // Usage_DurabilityLostOnUse

    StoreTemplate = 0x410002DE, // StoreTemplate
    Effect_IsMonsterOnly = 0x410002DF, // Effect_IsMonsterOnly
    NPC_CombatSpeedResistance = 0x410002E0, // NPC_CombatSpeedResistance

    Book_ShowControls = 0x410002E2, // Book_ShowControls
    Book_Image = 0x410002E3, // Book_Image
    NPC_DeathEffect5 = 0x410002E4, // NPC_DeathEffect5
    NPC_DeathEffect5Stat = 0x410002E5, // NPC_DeathEffect5Stat
    NPC_DeathEffect6 = 0x410002E6, // NPC_DeathEffect6
    NPC_DeathEffect6Stat = 0x410002E7, // NPC_DeathEffect6Stat
    NPC_DeathEffect7 = 0x410002E8, // NPC_DeathEffect7
    NPC_DeathEffect7Stat = 0x410002E9, // NPC_DeathEffect7Stat
    NPC_DeathEffect8 = 0x410002EA, // NPC_DeathEffect8
    NPC_DeathEffect8Stat = 0x410002EB, // NPC_DeathEffect8Stat
    StoreGroup = 0x410002EC, // StoreGroup
    MaxConsignments = 0x410002ED, // MaxConsignments
    AI_NearDeathLevel = 0x410002EE, // AI_NearDeathLevel
    PortalScene = 0x410002EF, // PortalScene
    StoreLocation = 0x410002F0, // StoreLocation
    Usage_IsBindOnUse = 0x410002F1, // Usage_IsBindOnUse
    Usage_LegionsExpansionOnly = 0x410002F2, // Usage_LegionsExpansionOnly
    AICombat_TargetConsiderInterval = 0x410002F3, // AICombat_TargetConsiderInterval
    NPC_ArmorThreshold = 0x410002F4, // NPC_ArmorThreshold
    NPC_DeathLootMinQuantityOverride = 0x410002F5, // NPC_DeathLootMinQuantityOverride
    NPC_DeathLootMaxQuantityOverride = 0x410002F6, // NPC_DeathLootMaxQuantityOverride
    NPC_DeathLootAbsoluteOverride = 0x410002F7, // NPC_DeathLootAbsoluteOverride
    NPC_DamageTypeMod = 0x410002F8, // NPC_DamageTypeMod
    NPC_DamageType = 0x410002F9, // NPC_DamageType
    NPC_CorpseOverrideEntity = 0x410002FA, // NPC_CorpseOverrideEntity
    NPC_MungeCorpseOverrideEntity = 0x410002FB, // NPC_MungeCorpseOverrideEntity
    NPC_TrophyDrop1 = 0x410002FC, // NPC_TrophyDrop1
    NPC_TrophyDrop1_Rate = 0x410002FD, // NPC_TrophyDrop1_Rate
    NPC_TrophyDrop2 = 0x410002FE, // NPC_TrophyDrop2
    NPC_TrophyDrop2_Rate = 0x410002FF, // NPC_TrophyDrop2_Rate
    NPC_TrophyDrop3 = 0x41000300, // NPC_TrophyDrop3
    NPC_TrophyDrop3_Rate = 0x41000301, // NPC_TrophyDrop3_Rate
    NPC_TrophyDrop4 = 0x41000302, // NPC_TrophyDrop4
    NPC_TrophyDrop4_Rate = 0x41000303, // NPC_TrophyDrop4_Rate
    NPC_TrophyDrop5 = 0x41000304, // NPC_TrophyDrop5
    NPC_TrophyDrop5_Rate = 0x41000305, // NPC_TrophyDrop5_Rate
    NPC_TrophyDrop6 = 0x41000306, // NPC_TrophyDrop6
    NPC_TrophyDrop6_Rate = 0x41000307, // NPC_TrophyDrop6_Rate
    NPC_TrophyDrop7 = 0x41000308, // NPC_TrophyDrop7
    NPC_TrophyDrop7_Rate = 0x41000309, // NPC_TrophyDrop7_Rate
    NPC_TrophyDrop8 = 0x4100030A, // NPC_TrophyDrop8
    NPC_TrophyDrop8_Rate = 0x4100030B, // NPC_TrophyDrop8_Rate
    NPC_TrophyDrop9 = 0x4100030C, // NPC_TrophyDrop9
    NPC_TrophyDrop9_Rate = 0x4100030D, // NPC_TrophyDrop9_Rate
    NPC_TrophyDrop10 = 0x4100030E, // NPC_TrophyDrop10
    NPC_TrophyDrop10_Rate = 0x4100030F, // NPC_TrophyDrop10_Rate
    NPC_TrophyDrop11 = 0x41000310, // NPC_TrophyDrop11
    NPC_TrophyDrop11_Rate = 0x41000311, // NPC_TrophyDrop11_Rate
    NPC_TrophyDrop12 = 0x41000312, // NPC_TrophyDrop12
    NPC_TrophyDrop12_Rate = 0x41000313, // NPC_TrophyDrop12_Rate
    NPC_TrophyDrop13 = 0x41000314, // NPC_TrophyDrop13
    NPC_TrophyDrop13_Rate = 0x41000315, // NPC_TrophyDrop13_Rate
    NPC_TrophyDrop14 = 0x41000316, // NPC_TrophyDrop14
    NPC_TrophyDrop14_Rate = 0x41000317, // NPC_TrophyDrop14_Rate
    NPC_TrophyDrop15 = 0x41000318, // NPC_TrophyDrop15
    NPC_TrophyDrop15_Rate = 0x41000319, // NPC_TrophyDrop15_Rate
    NPC_SpecialTrophyDrop1 = 0x4100031A, // NPC_SpecialTrophyDrop1
    NPC_SpecialTrophyDrop1_Rate = 0x4100031B, // NPC_SpecialTrophyDrop1_Rate
    NPC_SpecialTrophyDrop2 = 0x4100031C, // NPC_SpecialTrophyDrop2
    NPC_SpecialTrophyDrop2_Rate = 0x4100031D, // NPC_SpecialTrophyDrop2_Rate
    NPC_SpecialTrophyDrop3 = 0x4100031E, // NPC_SpecialTrophyDrop3
    NPC_SpecialTrophyDrop3_Rate = 0x4100031F, // NPC_SpecialTrophyDrop3_Rate
    NPC_SpecialTrophyDrop4 = 0x41000320, // NPC_SpecialTrophyDrop4
    NPC_SpecialTrophyDrop4_Rate = 0x41000321, // NPC_SpecialTrophyDrop4_Rate
    NPC_SpecialTrophyDrop5 = 0x41000322, // NPC_SpecialTrophyDrop5
    NPC_SpecialTrophyDrop5_Rate = 0x41000323, // NPC_SpecialTrophyDrop5_Rate

    Item_ValidTargetWeenieType = 0x41000325, // Item_ValidTargetWeenieType
    Item_NatureDamageMod = 0x41000326, // Item_NatureDamageMod
    Item_DecayDamageMod = 0x41000327, // Item_DecayDamageMod
    Item_MartialDamageMod = 0x41000328, // Item_MartialDamageMod
    Item_ArcaneDamageMod = 0x41000329, // Item_ArcaneDamageMod
    Item_NatureDamageModCap = 0x4100032A, // Item_NatureDamageModCap
    Item_DecayDamageModCap = 0x4100032B, // Item_DecayDamageModCap
    Item_MartialDamageModCap = 0x4100032C, // Item_MartialDamageModCap
    Item_ArcaneDamageModCap = 0x4100032D, // Item_ArcaneDamageModCap
    Item_NatureDamageModGrowthRate = 0x4100032E, // Item_NatureDamageModGrowthRate
    Item_DecayDamageModGrowthRate = 0x4100032F, // Item_DecayDamageModGrowthRate
    Item_MartialDamageModGrowthRate = 0x41000330, // Item_MartialDamageModGrowthRate
    Item_ArcaneDamageModGrowthRate = 0x41000331, // Item_ArcaneDamageModGrowthRate
    Item_NatureDamageModBaseMutabilityChance = 0x41000332, // Item_NatureDamageModBaseMutabilityChance
    Item_DecayDamageModBaseMutabilityChance = 0x41000333, // Item_DecayDamageModBaseMutabilityChance
    Item_MartialDamageModBaseMutabilityChance = 0x41000334, // Item_MartialDamageModBaseMutabilityChance
    Item_ArcaneDamageModBaseMutabilityChance = 0x41000335, // Item_ArcaneDamageModBaseMutabilityChance
    Item_IsDamageModMutable = 0x41000336, // Item_IsDamageModMutable
    AICombat_MeleeNPC = 0x41000337, // AICombat_MeleeNPC
    AICombat_MissileNPC = 0x41000338, // AICombat_MissileNPC
    AI_HarvestingVariance = 0x41000339, // AI_HarvestingVariance
    NPC_NeverDropTesserae = 0x4100033A, // NPC_NeverDropTesserae
    NPC_NeverDropLodestones = 0x4100033B, // NPC_NeverDropLodestones
    Quest_Retry_Cancelled = 0x4100033C, // Quest_Retry_Cancelled

    Gravity = 0x80000001, // Gravity
    Temperature = 0x80000002, // Temperature
    Humidity = 0x80000003, // Humidity
    Passability = 0x80000004, // Passability

    Friction = 0x80000006, // Friction
    Encounter_Density = 0x80000007, // Encounter-Density

    FactionStatus = 0x81000003, // FactionStatus
}
