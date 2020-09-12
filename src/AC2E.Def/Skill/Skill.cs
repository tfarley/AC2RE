namespace AC2E.Def {

    public class Skill : AbilityCalculator {

        public override PackageType packageType => PackageType.Skill;

        public StringInfo lore; // mLore
        public int allowedSpecies; // mAllowedSpecies
        public uint minCharLevel; // mMinCharLevel
        public AAHash barringSkills; // mBarringSkills
        public int levelWhenTrained; // mLevelWhenTrained
        public AAHash parents; // mParents
        public float combatSpeedModifier; // m_fCombatSpeedModifier
        public uint reqQuestId; // m_reqQuestID
        public int allowedClasses; // mAllowedClasses
        public AAHash prereqs; // mPrereqs
        public float advMod; // mAdvMod
        public int minPkRating; // m_iMinPKRating
        public bool shouldStartTimerOnEffectFailure; // mShouldStartTimerOnEffectFailure
        public int minSkillLevel; // mMinSkillLevel
        public DataId advTableDid; // mdidAdvTable
        public uint heroCost; // mHeroCost
        public int advCap; // mAdvCap
        public uint allowedFactions; // m_allowedFactions
        public StringInfo description; // mDesc
        public uint cost; // mCost
        public DataId iconDid; // mIcon
        public StringInfo name; // mName
        public uint skillFlags; // m_skillFlags

        public Skill(AC2Reader data) : base(data) {
            data.ReadPkg<StringInfo>(v => lore = v);
            allowedSpecies = data.ReadInt32();
            minCharLevel = data.ReadUInt32();
            data.ReadPkg<AAHash>(v => barringSkills = v);
            levelWhenTrained = data.ReadInt32();
            data.ReadPkg<AAHash>(v => parents = v);
            combatSpeedModifier = data.ReadSingle();
            reqQuestId = data.ReadUInt32();
            allowedClasses = data.ReadInt32();
            data.ReadPkg<AAHash>(v => prereqs = v);
            advMod = data.ReadSingle();
            minPkRating = data.ReadInt32();
            shouldStartTimerOnEffectFailure = data.ReadBoolean();
            minSkillLevel = data.ReadInt32();
            advTableDid = data.ReadDataId();
            heroCost = data.ReadUInt32();
            advCap = data.ReadInt32();
            allowedFactions = data.ReadUInt32();
            data.ReadPkg<StringInfo>(v => description = v);
            cost = data.ReadUInt32();
            iconDid = data.ReadDataId();
            data.ReadPkg<StringInfo>(v => name = v);
            skillFlags = data.ReadUInt32();
        }
    }
}
