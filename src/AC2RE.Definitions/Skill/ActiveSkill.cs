using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ActiveSkill : Skill {

        public override PackageType packageType => PackageType.ActiveSkill;

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
        public ActiveSkillTarget validTargets; // m_validTargets
        public uint validPetOnly; // m_validPetOnly
        public BehaviorId powerUpBehavior; // m_powerUpBehavior
        public float startFocusModMult; // m_startFocusModMult
        public float dmgAttributeMod; // m_fDmgAttributeMod
        public ImplementType allowedImplementsLeft; // m_AllowedImplementsLeft
        public ActiveSkillTarget requiredSkillTargetFlags; // m_requiredSkillTargetFlags
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
        public ActiveSkillFlag activeFlags; // m_uiFlags

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
            validTargets = (ActiveSkillTarget)data.ReadUInt32();
            validPetOnly = data.ReadUInt32();
            powerUpBehavior = (BehaviorId)data.ReadUInt32();
            startFocusModMult = data.ReadSingle();
            dmgAttributeMod = data.ReadSingle();
            allowedImplementsLeft = (ImplementType)data.ReadInt32();
            requiredSkillTargetFlags = (ActiveSkillTarget)data.ReadUInt32();
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
            activeFlags = (ActiveSkillFlag)data.ReadUInt32();
        }
    }
}
