namespace AC2E.Def {

    public class ActiveSkill : Skill {

        public override PackageType packageType => PackageType.ActiveSkill;

        public int maxPets; // m_MaxPets
        public uint behaviorOtherHit; // m_BehaviorOtherHit
        public int maxVigor; // m_MaxVigor
        public float minUseTime; // m_MinUseTime
        public RList<SingletonPkg<IPackage>> reqEffects; // m_ReqEffects
        public int minHealth; // m_MinHealth
        public int allowedImplementsRight; // m_AllowedImplementsRight
        public RList<SingletonPkg<IPackage>> barringEffects; // m_BarringEffects
        public float minRange; // m_MinRange
        public int endFocusModAdd; // m_endFocusModAdd
        public float recoveryTime; // m_RecoveryTime
        public int maxHealth; // m_MaxHealth
        public RList<SingletonPkg<IPackage>> userEffects; // m_UserEffects
        public AHashSet validWeenieTypes; // m_validWeenieTypes
        public uint behaviorSelfHit; // m_BehaviorSelfHit
        public int minVigor; // m_MinVigor
        public uint behaviorSelfMiss; // m_BehaviorSelfMiss
        public AAHash weightHash; // m_WeightHash
        public uint minRank; // m_nMinRank
        public uint behaviorOtherMiss; // m_BehaviorOtherMiss
        public uint validTargets; // m_validTargets
        public uint validPetOnly; // m_validPetOnly
        public uint powerUpBehavior; // m_powerUpBehavior
        public float startFocusModMult; // m_startFocusModMult
        public float dmgAttributeMod; // m_fDmgAttributeMod
        public int allowedImplementsLeft; // m_AllowedImplementsLeft
        public uint requiredSkillTargetFlags; // m_requiredSkillTargetFlags
        public uint behaviorOtherCrit; // m_BehaviorOtherCrit
        public uint minLevel; // m_nMinLevel
        public uint maxLevel; // m_nMaxLevel
        public AAHash useBarringSkills; // m_UseBarringSkills
        public int maxFocus; // m_MaxFocus
        public AAHash usePrereqSkills; // m_UsePrereqSkills
        public float endFocusModMult; // m_endFocusModMult
        public RList<SingletonPkg<IPackage>> targetEffects; // m_TargetEffects
        public int startFocusModAdd; // m_startFocusModAdd
        public int vigorModAdd; // m_VigorModAdd
        public AList invalidAISuperClasses; // m_invalidAISuperClasses
        public uint validFactionOnly; // m_validFactionOnly
        public float powerupTime; // m_PowerupTime
        public float vigorModMult; // m_VigorModMult
        public RArray<AttackHook> hooks; // m_Hooks
        public float resetTime; // m_ResetTime
        public int minFocus; // m_MinFocus
        public float dmgAttributeChance; // m_fDmgAttributeChance
        public float maxRange; // m_MaxRange
        public uint flags; // m_uiFlags

        public ActiveSkill(AC2Reader data) : base(data) {
            maxPets = data.ReadInt32();
            behaviorOtherHit = data.ReadUInt32();
            maxVigor = data.ReadInt32();
            minUseTime = data.ReadSingle();
            data.ReadPkg<RList<IPackage>>(v => reqEffects = v.to<SingletonPkg<IPackage>>());
            minHealth = data.ReadInt32();
            allowedImplementsRight = data.ReadInt32();
            data.ReadPkg<RList<IPackage>>(v => barringEffects = v.to<SingletonPkg<IPackage>>());
            minRange = data.ReadSingle();
            endFocusModAdd = data.ReadInt32();
            recoveryTime = data.ReadSingle();
            maxHealth = data.ReadInt32();
            data.ReadPkg<RList<IPackage>>(v => userEffects = v.to<SingletonPkg<IPackage>>());
            data.ReadPkg<AHashSet>(v => validWeenieTypes = v);
            behaviorSelfHit = data.ReadUInt32();
            minVigor = data.ReadInt32();
            behaviorSelfMiss = data.ReadUInt32();
            data.ReadPkg<AAHash>(v => weightHash = v);
            minRank = data.ReadUInt32();
            behaviorOtherMiss = data.ReadUInt32();
            validTargets = data.ReadUInt32();
            validPetOnly = data.ReadUInt32();
            powerUpBehavior = data.ReadUInt32();
            startFocusModMult = data.ReadSingle();
            dmgAttributeMod = data.ReadSingle();
            allowedImplementsLeft = data.ReadInt32();
            requiredSkillTargetFlags = data.ReadUInt32();
            behaviorOtherCrit = data.ReadUInt32();
            minLevel = data.ReadUInt32();
            maxLevel = data.ReadUInt32();
            data.ReadPkg<AAHash>(v => useBarringSkills = v);
            maxFocus = data.ReadInt32();
            data.ReadPkg<AAHash>(v => usePrereqSkills = v);
            endFocusModMult = data.ReadSingle();
            data.ReadPkg<RList<IPackage>>(v => targetEffects = v.to<SingletonPkg<IPackage>>());
            startFocusModAdd = data.ReadInt32();
            vigorModAdd = data.ReadInt32();
            data.ReadPkg<AList>(v => invalidAISuperClasses = v);
            validFactionOnly = data.ReadUInt32();
            powerupTime = data.ReadSingle();
            vigorModMult = data.ReadSingle();
            data.ReadPkg<RArray<IPackage>>(v => hooks = v.to<AttackHook>());
            resetTime = data.ReadSingle();
            minFocus = data.ReadInt32();
            dmgAttributeChance = data.ReadSingle();
            maxRange = data.ReadSingle();
            flags = data.ReadUInt32();
        }
    }
}
