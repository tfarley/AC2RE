using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Skill : MasterListMember {

    public override PackageType packageType => PackageType.Skill;

    // WLib Skill
    [Flags]
    public enum Flag : uint {
        None = 0,
        IsUntrainable = 1 << 0, // GetUntrainable 0x00000001
        IsHidden = 1 << 1, // GetHidden 0x00000002
        CannotRaise = 1 << 2, // IsCannotRaise 0x00000004
        IsNotTrainableByPlayer = 1 << 3, // IsNotTrainableByPlayer 0x00000008
        IsHeroSkill = 1 << 4, // IsHeroSkill 0x00000010
        IsToggleSkill = 1 << 5, // IsToggleSkill 0x00000020
        HasZeroVigorPenalty = 1 << 6, // HasZeroVigorPenalty 0x00000040
    }

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
    public Flag flags; // m_skillFlags

    public Skill(AC2Reader data) : base(data) {
        data.ReadHO<StringInfo>(v => lore = v);
        allowedSpecies = data.ReadEnum<SpeciesType>();
        minCharLevel = data.ReadUInt32();
        data.ReadHO<AAHash>(v => barringSkillIds = v.to<SkillId, uint>());
        levelWhenTrained = data.ReadInt32();
        data.ReadHO<AAHash>(v => parentSkillIds = v.to<SkillId, uint>());
        combatSpeedModifier = data.ReadSingle();
        reqQuestId = data.ReadEnum<QuestId>();
        allowedClasses = data.ReadInt32();
        data.ReadHO<AAHash>(v => prereqSkillIds = v.to<SkillId, uint>());
        advMod = data.ReadSingle();
        minPkRating = data.ReadInt32();
        shouldStartTimerOnEffectFailure = data.ReadBoolean();
        minSkillLevel = data.ReadInt32();
        advTableDid = data.ReadDataId();
        heroCost = data.ReadUInt32();
        advCap = data.ReadInt32();
        allowedFactions = data.ReadEnum<FactionType>();
        data.ReadHO<StringInfo>(v => description = v);
        cost = data.ReadUInt32();
        iconDid = data.ReadDataId();
        data.ReadHO<StringInfo>(v => name = v);
        flags = data.ReadEnum<Flag>();
    }
}
