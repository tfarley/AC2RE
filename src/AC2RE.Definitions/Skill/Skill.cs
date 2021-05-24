using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class Skill : MasterListMember {

        public override PackageType packageType => PackageType.Skill;

        public StringInfo lore; // mLore
        public SpeciesType allowedSpecies; // mAllowedSpecies
        public uint minCharLevel; // mMinCharLevel
        public Dictionary<SkillId, uint> barringSkillIds; // mBarringSkills
        public int levelWhenTrained; // mLevelWhenTrained
        public Dictionary<SkillId, uint> parentSkillIds; // mParents
        public float combatSpeedModifier; // m_fCombatSpeedModifier
        public QuestId reqQuestId; // m_reqQuestID
        public int allowedClasses; // mAllowedClasses
        public Dictionary<SkillId, uint> prereqSkillIds; // mPrereqs
        public float advMod; // mAdvMod
        public int minPkRating; // m_iMinPKRating
        public bool shouldStartTimerOnEffectFailure; // mShouldStartTimerOnEffectFailure
        public int minSkillLevel; // mMinSkillLevel
        public DataId advTableDid; // mdidAdvTable
        public uint heroCost; // mHeroCost
        public int advCap; // mAdvCap
        public FactionType allowedFactions; // m_allowedFactions
        public StringInfo description; // mDesc
        public uint cost; // mCost
        public DataId iconDid; // mIcon
        public StringInfo name; // mName
        public uint skillFlags; // m_skillFlags

        public Skill(AC2Reader data) : base(data) {
            data.ReadPkg<StringInfo>(v => lore = v);
            allowedSpecies = (SpeciesType)data.ReadUInt32();
            minCharLevel = data.ReadUInt32();
            data.ReadPkg<AAHash>(v => barringSkillIds = v.to<SkillId, uint>());
            levelWhenTrained = data.ReadInt32();
            data.ReadPkg<AAHash>(v => parentSkillIds = v.to<SkillId, uint>());
            combatSpeedModifier = data.ReadSingle();
            reqQuestId = (QuestId)data.ReadUInt32();
            allowedClasses = data.ReadInt32();
            data.ReadPkg<AAHash>(v => prereqSkillIds = v.to<SkillId, uint>());
            advMod = data.ReadSingle();
            minPkRating = data.ReadInt32();
            shouldStartTimerOnEffectFailure = data.ReadBoolean();
            minSkillLevel = data.ReadInt32();
            advTableDid = data.ReadDataId();
            heroCost = data.ReadUInt32();
            advCap = data.ReadInt32();
            allowedFactions = (FactionType)data.ReadUInt32();
            data.ReadPkg<StringInfo>(v => description = v);
            cost = data.ReadUInt32();
            iconDid = data.ReadDataId();
            data.ReadPkg<StringInfo>(v => name = v);
            skillFlags = data.ReadUInt32();
        }
    }
}
