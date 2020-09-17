namespace AC2E.Def {

    public class DefaultPermissionBlob : IPackage {

        public PackageType packageType => PackageType.DefaultPermissionBlob;

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
        public uint forQuest; // m_forQuest
        public float closeProximityMsgRadius; // m_fCloseProximityMsgRadius
        public int requiredArcaneLore; // m_iRequiredArcaneLore
        public bool heroOnly; // m_bHeroOnly
        public Position positionRequired; // m_positionRequired
        public uint questStatusRequired; // m_questStatusRequired
        public float veryFarProximityMsgRadius; // m_fVeryFarProximityMsgRadius
        public int minRank; // m_iMinRank
        public uint stRequired1; // m_stRequired1
        public uint stRequired2; // m_stRequired2
        public uint questRequired; // m_questRequired
        public SpeciesType speciesRequired; // m_speciesRequired
        public Position lastPosition; // m_posLastPosition
        public int requiredCraftSkillRating; // m_iRequiredCraftSkillRating
        public uint stRequired2Rating; // m_stRequired2Rating
        public int minLevel; // m_iMinLevel
        public int maxLevel; // m_iMaxLevel
        public uint stRestricted1; // m_stRestricted1
        public uint stRestricted2; // m_stRestricted2
        public uint mask; // m_uiMask
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
            forQuest = data.ReadUInt32();
            closeProximityMsgRadius = data.ReadSingle();
            requiredArcaneLore = data.ReadInt32();
            heroOnly = data.ReadBoolean();
            data.ReadPkg<Position>(v => positionRequired = v);
            questStatusRequired = data.ReadUInt32();
            veryFarProximityMsgRadius = data.ReadSingle();
            minRank = data.ReadInt32();
            stRequired1 = data.ReadUInt32();
            stRequired2 = data.ReadUInt32();
            questRequired = data.ReadUInt32();
            speciesRequired = (SpeciesType)data.ReadUInt32();
            data.ReadPkg<Position>(v => lastPosition = v);
            requiredCraftSkillRating = data.ReadInt32();
            stRequired2Rating = data.ReadUInt32();
            minLevel = data.ReadInt32();
            maxLevel = data.ReadInt32();
            stRestricted1 = data.ReadUInt32();
            stRestricted2 = data.ReadUInt32();
            mask = data.ReadUInt32();
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
