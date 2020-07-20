namespace AC2E.Def {

    public class AllegianceHierarchy : IPackage {

        public PackageType packageType => PackageType.AllegianceHierarchy;

        public uint m_removalRoomID;
        public StringInfo m_allegianceName;
        public WPString m_recallLocation;
        public AllegianceNode m_monarch;
        public int m_realTimeLastRename;
        public WPString m_roomName;
        public bool m_fChatRoomCreationInProgress;
        public bool m_fFactionKingdomRestrictionsOn;
        public NRHash<WPString, AllegianceNode> m_NameToNodeHash;
        public InstanceIdRHash<AllegianceNode> m_IIDToNodeHash;
        public StringInfo m_motd;
        public bool m_chatActive;
        public bool m_fAllowNeutralsToBypassKingdomRestrictions;
        public uint m_factionType;
        public uint m_total;
        public LAHashSet m_officerIDs;

        public AllegianceHierarchy() {

        }

        public AllegianceHierarchy(AC2Reader data, PackageRegistry registry) {
            m_removalRoomID = data.ReadUInt32();
            data.ReadPkgRef<StringInfo>(v => m_allegianceName = v, registry);
            data.ReadPkgRef<WPString>(v => m_recallLocation = v, registry);
            data.ReadPkgRef<AllegianceNode>(v => m_monarch = v, registry);
            m_realTimeLastRename = data.ReadInt32();
            data.ReadPkgRef<WPString>(v => m_roomName = v, registry);
            m_fChatRoomCreationInProgress = data.ReadBoolean();
            m_fFactionKingdomRestrictionsOn = data.ReadBoolean();
            data.ReadPkgRef<NRHash<IPackage, IPackage>>(v => m_NameToNodeHash = v.to<WPString, AllegianceNode>(), registry);
            data.ReadPkgRef<LRHash<IPackage>>(v => m_IIDToNodeHash = new InstanceIdRHash<AllegianceNode>(v.to<AllegianceNode>()), registry);
            data.ReadPkgRef<StringInfo>(v => m_motd = v, registry);
            m_chatActive = data.ReadBoolean();
            m_fAllowNeutralsToBypassKingdomRestrictions = data.ReadBoolean();
            m_factionType = data.ReadUInt32();
            m_total = data.ReadUInt32();
            data.ReadPkgRef<LAHashSet>(v => m_officerIDs = v, registry);
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(m_removalRoomID);
            data.Write(m_allegianceName, registry);
            data.Write(m_recallLocation, registry);
            data.Write(m_monarch, registry);
            data.Write(m_realTimeLastRename);
            data.Write(m_roomName, registry);
            data.Write(m_fChatRoomCreationInProgress);
            data.Write(m_fFactionKingdomRestrictionsOn);
            data.Write(m_NameToNodeHash, registry);
            data.Write(m_IIDToNodeHash, registry);
            data.Write(m_motd, registry);
            data.Write(m_chatActive);
            data.Write(m_fAllowNeutralsToBypassKingdomRestrictions);
            data.Write(m_factionType);
            data.Write(m_total);
            data.Write(m_officerIDs, registry);
        }
    }
}
