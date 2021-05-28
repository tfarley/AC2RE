using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ActiveSkill : Skill {

        public override PackageType packageType => PackageType.ActiveSkill;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            USE_WHILE_MOVING = 1 << 0, // 0x00000001, ActiveSkill::GetUseWhileMoving
            IGNORE_WEAPON_TIME = 1 << 1, // 0x00000002, ActiveSkill::GetIgnoreWeaponTime
            IGNORE_ARMOR_DELAY = 1 << 2, // 0x00000004, ActiveSkill::GetIgnoreArmorDelay
            IGNORE_WEAPON_OFFENSE = 1 << 3, // 0x00000008, ActiveSkill::GetIgnoreWeaponOffense
            IGNORE_WEAPON_DEFENSE = 1 << 4, // 0x00000010, ActiveSkill::GetIgnoreWeaponDefense
            IGNORE_WEAPON_VIGOR = 1 << 5, // 0x00000020, ActiveSkill::GetIgnoreWeaponVigor
            IGNORE_WEAPON_DAMAGE = 1 << 6, // 0x00000040, ActiveSkill::GetIgnoreWeaponDamage
            IGNORE_SHIELD_VIGOR = 1 << 7, // 0x00000080, ActiveSkill::GetIgnoreShieldVigor
            IGNORE_SHIELD_DELAY = 1 << 8, // 0x00000100, ActiveSkill::GetIgnoreShieldDelay
            NON_DAMAGING = 1 << 9, // 0x00000200, ActiveSkill::IsNonDamaging
            HARMFUL = 1 << 10, // 0x00000400, ActiveSkill::IsHarmful
            REQUIRES_MOVE_TO = 1 << 11, // 0x00000800, ActiveSkill::RequiresMoveTo
            REQUIRES_TURN_TO = 1 << 12, // 0x00001000, ActiveSkill::RequiresTurnTo
            SELF_TARGETED = 1 << 13, // 0x00002000, ActiveSkill::IsSelfTargeted
            SUCCEED_ON_ANIMATION_FAILURE = 1 << 14, // 0x00004000, ActiveSkill::ShouldSucceedOnAnimationFailure
            IGNORED_BY_PETS = 1 << 15, // 0x00008000, ActiveSkill::IsIgnoredByPets
            IGNORE_WEAPON_FOCUS = 1 << 16, // 0x00010000, ActiveSkill::GetIgnoreWeaponFocus
            IGNORE_SHIELD_FOCUS = 1 << 17, // 0x00020000, ActiveSkill::GetIgnoreShieldFocus
            IGNORE_RESET_TIMERS = 1 << 18, // 0x00040000, ActiveSkill::GetIgnoreResetTimers
            USES_EFFECT_REQUIRED_ARMOR_MODIFIER = 1 << 19, // 0x00080000, ActiveSkill::UsesEffectRequiredArmorModifier
            HEAL_ONLY = 1 << 20, // 0x00100000, ActiveSkill::IsHealOnly
        }

        // WLib
        [Flags]
        public enum Target : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            SELF = 1 << 0, // 0x00000001, ActiveSkill::ValidTargetSelf
            SELF_ITEM = 1 << 1, // 0x00000002, ActiveSkill::ValidTargetSelfItem
            EXTERNAL_ITEM = 1 << 2, // 0x00000004, ActiveSkill::ValidTargetExternalItem
            MONSTER_ITEM = 1 << 3, // 0x00000008, ActiveSkill::ValidTargetMonsterItem
            PLAYER_ITEM = 1 << 4, // 0x00000010, ActiveSkill::ValidTargetPlayerItem
            PLAYER_CORPSE = 1 << 5, // 0x00000020, ActiveSkill::ValidTargetPlayerCorpse
            MONSTER_CORPSE = 1 << 6, // 0x00000040, ActiveSkill::ValidTargetMonsterCorpse
            MONSTER = 1 << 7, // 0x00000080, ActiveSkill::ValidTargetMonster
            PLAYER = 1 << 8, // 0x00000100, ActiveSkill::ValidTargetPlayer
            NPC = 1 << 9, // 0x00000200, ActiveSkill::ValidTargetNPC
            SAME_FELLOWSHIP_ONLY = 1 << 10, // 0x00000400, ActiveSkill::ValidTargetSameFellowshipOnly
            NOT_SAME_FELLOWSHIP_ONLY = 1 << 11, // 0x00000800, ActiveSkill::ValidTargetNotSameFellowshipOnly
            SAME_ALLEGIANCE_ONLY = 1 << 12, // 0x00001000, ActiveSkill::ValidTargetSameAllegianceOnly
            NOT_SAME_ALLEGIANCE_ONLY = 1 << 13, // 0x00002000, ActiveSkill::ValidTargetNotSameAllegianceOnly
            GREATER_THAN_OR_EQUAL_TO_ALLEGIANCE_RANK = 1 << 14, // 0x00004000, ActiveSkill::ValidTargetGreaterThanOrEqualToAllegianceRank
            SAME_FACTION_ONLY = 1 << 15, // 0x00008000, ActiveSkill::ValidTargetSameFactionOnly
            NOT_SAME_FACTION_ONLY = 1 << 16, // 0x00010000, ActiveSkill::ValidTargetNotSameFactionOnly
            FACTION_ONLY = 1 << 17, // 0x00020000, ActiveSkill::ValidTargetFactionOnly
            MIN_LEVEL = 1 << 18, // 0x00040000,  ActiveSkill::ValidTargetMinLevel
            MAX_LEVEL = 1 << 19, // 0x00080000, ActiveSkill::ValidTargetMaxLevel
            UNOWNED_PET = 1 << 20, // 0x00100000, ActiveSkill::ValidTargetUnownedPet
            OWNED_PET = 1 << 21, // 0x00200000, ActiveSkill::ValidTargetOwnedPet
            PET_ONLY = 1 << 22, // 0x00400000, ActiveSkill::ValidTargetPetOnly
            WEENIE_TYPE = 1 << 23, // 0x00800000, ActiveSkill::ValidTargetWeenieType
            PET_SUMMONER = 1 << 24, // 0x01000000, ActiveSkill::ValidTargetPetSummoner
            RESURRECTABLE_CORPSE = 1 << 25, // 0x02000000, ActiveSkill::ValidTargetResurrectableCorpse
            NOT_AI_SUPER_CLASS = 1 << 26, // 0x04000000, ActiveSkill::ValidTargetNotAISuperClass
            SKILL_TARGET_FLAGS_ONLY = 1 << 27, // 0x08000000, ActiveSkill::ValidTargetSkillTargetFlagsOnly
            EQUIPPED_ITEM = 1 << 28, // 0x10000000, ActiveSkill::ValidTargetEquippedItem
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
            data.ReadPkg<RList>(v => reqEffects = v.to(SingletonPkg<Effect>.cast));
            minHealth = data.ReadInt32();
            allowedImplementsRight = (ImplementType)data.ReadInt32();
            data.ReadPkg<RList>(v => barringEffects = v.to(SingletonPkg<Effect>.cast));
            minRange = data.ReadSingle();
            endFocusModAdd = data.ReadInt32();
            recoveryTime = data.ReadSingle();
            maxHealth = data.ReadInt32();
            data.ReadPkg<RList>(v => userEffects = v.to(SingletonPkg<Effect>.cast));
            data.ReadPkg<AHashSet>(v => validWeenieTypes = v.to<WeenieType>());
            behaviorSelfHit = (BehaviorId)data.ReadUInt32();
            minVigor = data.ReadInt32();
            behaviorSelfMiss = (BehaviorId)data.ReadUInt32();
            data.ReadPkg<AAHash>(v => weightHash = v);
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
            data.ReadPkg<AAHash>(v => useBarringSkills = v);
            maxFocus = data.ReadInt32();
            data.ReadPkg<AAHash>(v => usePrereqSkills = v);
            endFocusModMult = data.ReadSingle();
            data.ReadPkg<RList>(v => targetEffects = v.to(SingletonPkg<Effect>.cast));
            startFocusModAdd = data.ReadInt32();
            vigorModAdd = data.ReadInt32();
            data.ReadPkg<AList>(v => invalidAISuperClasses = v);
            validFactionOnly = data.ReadUInt32();
            powerupTime = data.ReadSingle();
            vigorModMult = data.ReadSingle();
            data.ReadPkg<RArray>(v => hooks = v.to<AttackHook>());
            resetTime = data.ReadSingle();
            minFocus = data.ReadInt32();
            dmgAttributeChance = data.ReadSingle();
            maxRange = data.ReadSingle();
            activeFlags = (Flag)data.ReadUInt32();
        }
    }
}
