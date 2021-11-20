using System;

namespace AC2RE.Definitions {

    public class DefaultPermissionBlob : IPackage {

        public PackageType packageType => PackageType.DefaultPermissionBlob;

        // WLib DefaultPermissionBlob
        [Flags]
        public enum Flag : uint {
            None = 0,
            HasMinLevel = 1 << 0, // HasMinLevel 0x00000001
            HasMaxLevel = 1 << 1, // HasMaxLevel 0x00000002
            HasFactionRequired = 1 << 2, // HasFactionRequired 0x00000004
            HasRequiredSkill1 = 1 << 3, // HasRequiredSkill1 0x00000008
            HasRequiredSkill2 = 1 << 4, // HasRequiredSkill2 0x00000010
            HasRestrictedSkill1 = 1 << 5, // HasRestrictedSkill1 0x00000020
            HasRestrictedSkill2 = 1 << 6, // HasRestrictedSkill2 0x00000040
            HasRequiredRace = 1 << 7, // HasRequiredRace 0x00000080
            HasRequiredQuest = 1 << 8, // HasRequiredQuest 0x00000100
            HasRequiredQuestStatus = 1 << 9, // HasRequiredQuestStatus 0x00000200
            HasLandblockFaction = 1 << 10, // HasLandblockFaction 0x00000400
            HasMinRank = 1 << 11, // HasMinRank 0x00000800
            HasMaxRank = 1 << 12, // HasMaxRank 0x00001000
            HasNonAllegianceOnly = 1 << 13, // HasNonAllegianceOnly 0x00002000
            HasMonarchOnly = 1 << 14, // HasMonarchOnly 0x00004000
            HasRequiredSkill1Rating = 1 << 15, // HasRequiredSkill1Rating 0x00008000
            HasRequiredSkill2Rating = 1 << 16, // HasRequiredSkill2Rating 0x00010000
            HasCrafterOnly = 1 << 17, // HasCrafterOnly 0x00020000
            HasHeroOnly = 1 << 18, // HasHeroOnly 0x00040000
            HasRequiredArcaneLore = 1 << 19, // HasRequiredArcaneLore 0x00080000
            HasRequiredLocation = 1 << 20, // HasRequiredLocation 0x00100000
            HasUseWhileMoving = 1 << 21, // HasUseWhileMoving 0x00200000
            HasSummonerOnly = 1 << 22, // HasSummonerOnly 0x00400000
            HasRequiredCraftSkill = 1 << 23, // HasRequiredCraftSkill 0x00800000
            HasLegionsExpansionOnly = 1 << 24, // HasLegionsExpansionOnly 0x01000000
            IsBound = 1 << 25, // IsBound 0x02000000
        }

        public DataId stringTableForErrorsDid; // m_didStringTableForErrors
        public FactionType factionRequired; // m_factionRequired
        public InstanceId boundToId; // m_iidBoundTo
        public uint stRequired1Rating; // m_stRequired1Rating
        public InstanceId lastUserId; // m_iidLastUser
        public bool useWhileMoving; // m_bUseWhileMoving
        public int maxRank; // m_iMaxRank
        public float farProximityMsgRadius; // m_fFarProximityMsgRadius
        public bool monarchOnly; // m_bMonarchOnly
        public float usageRadius; // m_fUsageRadius
        public float lastDistance; // m_fLastDistance
        public QuestId forQuest; // m_forQuest
        public float closeProximityMsgRadius; // m_fCloseProximityMsgRadius
        public int requiredArcaneLore; // m_iRequiredArcaneLore
        public bool heroOnly; // m_bHeroOnly
        public Position positionRequired; // m_positionRequired
        public QuestStatus questStatusRequired; // m_questStatusRequired
        public float veryFarProximityMsgRadius; // m_fVeryFarProximityMsgRadius
        public int minRank; // m_iMinRank
        public SkillId skillIdRequired1; // m_stRequired1
        public SkillId skillIdRequired2; // m_stRequired2
        public QuestId questRequired; // m_questRequired
        public SpeciesType speciesRequired; // m_speciesRequired
        public Position lastPosition; // m_posLastPosition
        public int requiredCraftSkillRating; // m_iRequiredCraftSkillRating
        public uint stRequired2Rating; // m_stRequired2Rating
        public int minLevel; // m_iMinLevel
        public int maxLevel; // m_iMaxLevel
        public SkillId skillIdRestricted1; // m_stRestricted1
        public SkillId skillIdRestricted2; // m_stRestricted2
        public Flag flags; // m_uiMask
        public bool landblockFaction; // m_bLandblockFaction
        public bool legionsExpansionOnly; // m_bLegionsExpansionOnly
        public float mediumProximityMsgRadius; // m_fMediumProximityMsgRadius
        public bool nonAllegianceOnly; // m_bNonAllegianceOnly
        public bool crafterOnly; // m_bCrafterOnly
        public bool summonerOnly; // m_bSummonerOnly
        public DataId requiredCraftSkillDid; // m_didRequiredCraftSkill
        public int locationFeedbackType; // m_iLocationFeedbackType

        public DefaultPermissionBlob(AC2Reader data) {
            stringTableForErrorsDid = data.ReadDataId();
            factionRequired = (FactionType)data.ReadUInt32();
            boundToId = data.ReadInstanceId();
            stRequired1Rating = data.ReadUInt32();
            lastUserId = data.ReadInstanceId();
            useWhileMoving = data.ReadBoolean();
            maxRank = data.ReadInt32();
            farProximityMsgRadius = data.ReadSingle();
            monarchOnly = data.ReadBoolean();
            usageRadius = data.ReadSingle();
            lastDistance = data.ReadSingle();
            forQuest = (QuestId)data.ReadUInt32();
            closeProximityMsgRadius = data.ReadSingle();
            requiredArcaneLore = data.ReadInt32();
            heroOnly = data.ReadBoolean();
            data.ReadPkg<Position>(v => positionRequired = v);
            questStatusRequired = (QuestStatus)data.ReadUInt32();
            veryFarProximityMsgRadius = data.ReadSingle();
            minRank = data.ReadInt32();
            skillIdRequired1 = (SkillId)data.ReadUInt32();
            skillIdRequired2 = (SkillId)data.ReadUInt32();
            questRequired = (QuestId)data.ReadUInt32();
            speciesRequired = (SpeciesType)data.ReadUInt32();
            data.ReadPkg<Position>(v => lastPosition = v);
            requiredCraftSkillRating = data.ReadInt32();
            stRequired2Rating = data.ReadUInt32();
            minLevel = data.ReadInt32();
            maxLevel = data.ReadInt32();
            skillIdRestricted1 = (SkillId)data.ReadUInt32();
            skillIdRestricted2 = (SkillId)data.ReadUInt32();
            flags = (Flag)data.ReadUInt32();
            landblockFaction = data.ReadBoolean();
            legionsExpansionOnly = data.ReadBoolean();
            mediumProximityMsgRadius = data.ReadSingle();
            nonAllegianceOnly = data.ReadBoolean();
            crafterOnly = data.ReadBoolean();
            summonerOnly = data.ReadBoolean();
            requiredCraftSkillDid = data.ReadDataId();
            locationFeedbackType = data.ReadInt32();
        }
    }
}
