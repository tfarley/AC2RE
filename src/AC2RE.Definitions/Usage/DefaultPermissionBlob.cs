using System;

namespace AC2RE.Definitions {

    public class DefaultPermissionBlob : IPackage {

        public PackageType packageType => PackageType.DefaultPermissionBlob;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            MIN_LEVEL = 1 << 0, // 0x00000001, DefaultPermissionBlob::HasMinLevel
            MAX_LEVEL = 1 << 1, // 0x00000002, DefaultPermissionBlob::HasMaxLevel
            FACTION_REQUIRED = 1 << 2, // 0x00000004, DefaultPermissionBlob::HasFactionRequired
            REQUIRED_SKILL_1 = 1 << 3, // 0x00000008, DefaultPermissionBlob::HasRequiredSkill1
            REQUIRED_SKILL_2 = 1 << 4, // 0x00000010, DefaultPermissionBlob::HasRequiredSkill2
            RESTRICTED_SKILL_1 = 1 << 5, // 0x00000020, DefaultPermissionBlob::HasRestrictedSkill1
            RESTRICTED_SKILL_2 = 1 << 6, // 0x00000040, DefaultPermissionBlob::HasRestrictedSkill2
            REQUIRED_RACE = 1 << 7, // 0x00000080, DefaultPermissionBlob::HasRequiredRace
            REQUIRED_QUEST = 1 << 8, // 0x00000100, DefaultPermissionBlob::HasRequiredQuest
            REQUIRED_QUEST_STATUS = 1 << 9, // 0x00000200, DefaultPermissionBlob::HasRequiredQuestStatus
            LANDBLOCK_FACTION = 1 << 10, // 0x00000400, DefaultPermissionBlob::HasLandblockFaction
            MIN_RANK = 1 << 11, // 0x00000800, DefaultPermissionBlob::HasMinRank
            MAX_RANK = 1 << 12, // 0x00001000, DefaultPermissionBlob::HasMaxRank
            NON_ALLEGIANCE_ONLY = 1 << 13, // 0x00002000, DefaultPermissionBlob::HasNonAllegianceOnly
            MONARCH_ONLY = 1 << 14, // 0x00004000, DefaultPermissionBlob::HasMonarchOnly
            REQUIRED_SKILL_1_RATING = 1 << 15, // 0x00008000, DefaultPermissionBlob::HasRequiredSkill1Rating
            REQUIRED_SKILL_2_RATING = 1 << 16, // 0x00010000, DefaultPermissionBlob::HasRequiredSkill2Rating
            CRAFTER_ONLY = 1 << 17, // 0x00020000, DefaultPermissionBlob::HasCrafterOnly
            HERO_ONLY = 1 << 18, // 0x00040000, DefaultPermissionBlob::HasHeroOnly
            REQUIRED_ARCANE_LORE = 1 << 19, // 0x00080000, DefaultPermissionBlob::HasRequiredArcaneLore
            REQUIRED_LOCATION = 1 << 20, // 0x00100000, DefaultPermissionBlob::HasRequiredLocation
            USE_WHILE_MOVING = 1 << 21, // 0x00200000, DefaultPermissionBlob::HasUseWhileMoving
            SUMMONER_ONLY = 1 << 22, // 0x00400000, DefaultPermissionBlob::HasSummonerOnly
            REQUIRED_CRAFT_SKILL = 1 << 23, // 0x00800000, DefaultPermissionBlob::HasRequiredCraftSkill
            LEGIONS_EXPANSION_ONLY = 1 << 24, // 0x01000000, DefaultPermissionBlob::HasLegionsExpansionOnly
            BOUND = 1 << 25, // 0x02000000, DefaultPermissionBlob::IsBound
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
