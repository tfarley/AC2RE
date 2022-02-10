using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ActiveSkill : Skill {

    public override PackageType packageType => PackageType.ActiveSkill;

    // WLib ActiveSkill
    [Flags]
    public new enum Flag : uint {
        None = 0,
        UseWhileMoving = 1 << 0, // GetUseWhileMoving 0x00000001
        IgnoreWeaponTime = 1 << 1, // GetIgnoreWeaponTime 0x00000002
        IgnoreArmorDelay = 1 << 2, // GetIgnoreArmorDelay 0x00000004
        IgnoreWeaponOffense = 1 << 3, // GetIgnoreWeaponOffense 0x00000008
        IgnoreWeaponDefense = 1 << 4, // GetIgnoreWeaponDefense 0x00000010
        IgnoreWeaponVigor = 1 << 5, // GetIgnoreWeaponVigor 0x00000020
        IgnoreWeaponDamage = 1 << 6, // GetIgnoreWeaponDamage 0x00000040
        IgnoreShieldVigor = 1 << 7, // GetIgnoreShieldVigor 0x00000080
        IgnoreShieldDelay = 1 << 8, // GetIgnoreShieldDelay 0x00000100
        IsNonDamaging = 1 << 9, // IsNonDamaging 0x00000200
        IsHarmful = 1 << 10, // IsHarmful 0x00000400
        RequiresMoveTo = 1 << 11, // RequiresMoveTo 0x00000800
        RequiresTurnTo = 1 << 12, // RequiresTurnTo 0x00001000
        IsSelfTargeted = 1 << 13, // IsSelfTargeted 0x00002000
        ShouldSucceedOnAnimationFailure = 1 << 14, // ShouldSucceedOnAnimationFailure 0x00004000
        IsIgnoredByPets = 1 << 15, // IsIgnoredByPets 0x00008000
        IgnoreWeaponFocus = 1 << 16, // GetIgnoreWeaponFocus 0x00010000
        IgnoreShieldFocus = 1 << 17, // GetIgnoreShieldFocus 0x00020000
        IgnoreResetTimers = 1 << 18, // GetIgnoreResetTimers 0x00040000
        UsesEffectRequiredArmorModifier = 1 << 19, // UsesEffectRequiredArmorModifier 0x00080000
        IsHealOnly = 1 << 20, // IsHealOnly 0x00100000
    }

    // WLib ActiveSkill
    [Flags]
    public enum Target : uint {
        None = 0,
        Self = 1 << 0, // ValidTargetSelf 0x00000001
        SelfItem = 1 << 1, // ValidTargetSelfItem 0x00000002
        ExternalItem = 1 << 2, // ValidTargetExternalItem 0x00000004
        MonsterItem = 1 << 3, // ValidTargetMonsterItem 0x00000008
        PlayerItem = 1 << 4, // ValidTargetPlayerItem 0x00000010
        PlayerCorpse = 1 << 5, // ValidTargetPlayerCorpse 0x00000020
        MonsterCorpse = 1 << 6, // ValidTargetMonsterCorpse 0x00000040
        Monster = 1 << 7, // ValidTargetMonster 0x00000080
        Player = 1 << 8, // ValidTargetPlayer 0x00000100
        NPC = 1 << 9, // ValidTargetNPC 0x00000200
        SameFellowshipOnly = 1 << 10, // ValidTargetSameFellowshipOnly 0x00000400
        NotSameFellowshipOnly = 1 << 11, // ValidTargetNotSameFellowshipOnly 0x00000800
        SameAllegianceOnly = 1 << 12, // ValidTargetSameAllegianceOnly 0x00001000
        NotSameAllegianceOnly = 1 << 13, // ValidTargetNotSameAllegianceOnly 0x00002000
        GreaterThanOrEqualToAllegianceRank = 1 << 14, // ValidTargetGreaterThanOrEqualToAllegianceRank 0x00004000
        SameFactionOnly = 1 << 15, // ValidTargetSameFactionOnly 0x00008000
        NotSameFactionOnly = 1 << 16, // ValidTargetNotSameFactionOnly 0x00010000
        FactionOnly = 1 << 17, // ValidTargetFactionOnly 0x00020000
        MinLevel = 1 << 18, // ValidTargetMinLevel 0x00040000,  ActiveSkill::ValidTargetMinLevel
        MaxLevel = 1 << 19, // ValidTargetMaxLevel 0x00080000
        UnownedPet = 1 << 20, // ValidTargetUnownedPet 0x00100000
        OwnedPet = 1 << 21, // ValidTargetOwnedPet 0x00200000
        PetOnly = 1 << 22, // ValidTargetPetOnly 0x00400000
        WeenieType = 1 << 23, // ValidTargetWeenieType 0x00800000
        PetSummoner = 1 << 24, // ValidTargetPetSummoner 0x01000000
        ResurrectableCorpse = 1 << 25, // ValidTargetResurrectableCorpse 0x02000000
        NotAISuperClass = 1 << 26, // ValidTargetNotAISuperClass 0x04000000
        SkillTargetFlagsOnly = 1 << 27, // ValidTargetSkillTargetFlagsOnly 0x08000000
        EquippedItem = 1 << 28, // ValidTargetEquippedItem 0x10000000
    }

    public int maxPets; // m_MaxPets
    public BehaviorId behaviorOtherHit; // m_BehaviorOtherHit
    public int maxVigor; // m_MaxVigor
    public float minUseTime; // m_MinUseTime
    public List<SingletonPkg<Effect>> reqEffects; // m_ReqEffects
    public int minHealth; // m_MinHealth
    public ImplementType allowedImplementsRight; // m_AllowedImplementsRight
    public List<SingletonPkg<Effect>> barringEffects; // m_BarringEffects
    public float minRange; // m_MinRange
    public int endFocusModAdd; // m_endFocusModAdd
    public float recoveryTime; // m_RecoveryTime
    public int maxHealth; // m_MaxHealth
    public List<SingletonPkg<Effect>> userEffects; // m_UserEffects
    public HashSet<WeenieType> validWeenieTypes; // m_validWeenieTypes
    public BehaviorId behaviorSelfHit; // m_BehaviorSelfHit
    public int minVigor; // m_MinVigor
    public BehaviorId behaviorSelfMiss; // m_BehaviorSelfMiss
    public Dictionary<uint, uint> weightHash; // m_WeightHash
    public uint minRank; // m_nMinRank
    public BehaviorId behaviorOtherMiss; // m_BehaviorOtherMiss
    public Target validTargets; // m_validTargets
    public uint validPetOnly; // m_validPetOnly
    public BehaviorId powerUpBehavior; // m_powerUpBehavior
    public float startFocusModMult; // m_startFocusModMult
    public float dmgAttributeMod; // m_fDmgAttributeMod
    public ImplementType allowedImplementsLeft; // m_AllowedImplementsLeft
    public Target requiredSkillTargetFlags; // m_requiredSkillTargetFlags
    public BehaviorId behaviorOtherCrit; // m_BehaviorOtherCrit
    public uint minLevel; // m_nMinLevel
    public uint maxLevel; // m_nMaxLevel
    public Dictionary<uint, uint> useBarringSkills; // m_UseBarringSkills
    public int maxFocus; // m_MaxFocus
    public Dictionary<uint, uint> usePrereqSkills; // m_UsePrereqSkills
    public float endFocusModMult; // m_endFocusModMult
    public List<SingletonPkg<Effect>> targetEffects; // m_TargetEffects
    public int startFocusModAdd; // m_startFocusModAdd
    public int vigorModAdd; // m_VigorModAdd
    public List<uint> invalidAISuperClasses; // m_invalidAISuperClasses
    public uint validFactionOnly; // m_validFactionOnly
    public float powerupTime; // m_PowerupTime
    public float vigorModMult; // m_VigorModMult
    public List<AttackHook> hooks; // m_Hooks
    public float resetTime; // m_ResetTime
    public int minFocus; // m_MinFocus
    public float dmgAttributeChance; // m_fDmgAttributeChance
    public float maxRange; // m_MaxRange
    public Flag activeFlags; // m_uiFlags

    public ActiveSkill(AC2Reader data) : base(data) {
        maxPets = data.ReadInt32();
        behaviorOtherHit = (BehaviorId)data.ReadUInt32();
        maxVigor = data.ReadInt32();
        minUseTime = data.ReadSingle();
        data.ReadHO<RList>(v => reqEffects = v.to(SingletonPkg<Effect>.cast));
        minHealth = data.ReadInt32();
        allowedImplementsRight = (ImplementType)data.ReadInt32();
        data.ReadHO<RList>(v => barringEffects = v.to(SingletonPkg<Effect>.cast));
        minRange = data.ReadSingle();
        endFocusModAdd = data.ReadInt32();
        recoveryTime = data.ReadSingle();
        maxHealth = data.ReadInt32();
        data.ReadHO<RList>(v => userEffects = v.to(SingletonPkg<Effect>.cast));
        data.ReadHO<AHashSet>(v => validWeenieTypes = v.to<WeenieType>());
        behaviorSelfHit = (BehaviorId)data.ReadUInt32();
        minVigor = data.ReadInt32();
        behaviorSelfMiss = (BehaviorId)data.ReadUInt32();
        data.ReadHO<AAHash>(v => weightHash = v);
        minRank = data.ReadUInt32();
        behaviorOtherMiss = (BehaviorId)data.ReadUInt32();
        validTargets = (Target)data.ReadUInt32();
        validPetOnly = data.ReadUInt32();
        powerUpBehavior = (BehaviorId)data.ReadUInt32();
        startFocusModMult = data.ReadSingle();
        dmgAttributeMod = data.ReadSingle();
        allowedImplementsLeft = (ImplementType)data.ReadInt32();
        requiredSkillTargetFlags = (Target)data.ReadUInt32();
        behaviorOtherCrit = (BehaviorId)data.ReadUInt32();
        minLevel = data.ReadUInt32();
        maxLevel = data.ReadUInt32();
        data.ReadHO<AAHash>(v => useBarringSkills = v);
        maxFocus = data.ReadInt32();
        data.ReadHO<AAHash>(v => usePrereqSkills = v);
        endFocusModMult = data.ReadSingle();
        data.ReadHO<RList>(v => targetEffects = v.to(SingletonPkg<Effect>.cast));
        startFocusModAdd = data.ReadInt32();
        vigorModAdd = data.ReadInt32();
        data.ReadHO<AList>(v => invalidAISuperClasses = v);
        validFactionOnly = data.ReadUInt32();
        powerupTime = data.ReadSingle();
        vigorModMult = data.ReadSingle();
        data.ReadHO<RArray>(v => hooks = v.to<AttackHook>());
        resetTime = data.ReadSingle();
        minFocus = data.ReadInt32();
        dmgAttributeChance = data.ReadSingle();
        maxRange = data.ReadSingle();
        activeFlags = (Flag)data.ReadUInt32();
    }
}
