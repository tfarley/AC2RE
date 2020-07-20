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

        public AllegianceHierarchy(AC2Reader data) {
            m_removalRoomID = data.ReadUInt32();
            data.ReadPkg<StringInfo>(v => m_allegianceName = v);
            data.ReadPkg<WPString>(v => m_recallLocation = v);
            data.ReadPkg<AllegianceNode>(v => m_monarch = v);
            m_realTimeLastRename = data.ReadInt32();
            data.ReadPkg<WPString>(v => m_roomName = v);
            m_fChatRoomCreationInProgress = data.ReadBoolean();
            m_fFactionKingdomRestrictionsOn = data.ReadBoolean();
            data.ReadPkg<NRHash<IPackage, IPackage>>(v => m_NameToNodeHash = v.to<WPString, AllegianceNode>());
            data.ReadPkg<LRHash<IPackage>>(v => m_IIDToNodeHash = new InstanceIdRHash<AllegianceNode>(v.to<AllegianceNode>()));
            data.ReadPkg<StringInfo>(v => m_motd = v);
            m_chatActive = data.ReadBoolean();
            m_fAllowNeutralsToBypassKingdomRestrictions = data.ReadBoolean();
            m_factionType = data.ReadUInt32();
            m_total = data.ReadUInt32();
            data.ReadPkg<LAHashSet>(v => m_officerIDs = v);
        }

        public void write(AC2Writer data) {
            data.Write(m_removalRoomID);
            data.WritePkg(m_allegianceName);
            data.WritePkg(m_recallLocation);
            data.WritePkg(m_monarch);
            data.Write(m_realTimeLastRename);
            data.WritePkg(m_roomName);
            data.Write(m_fChatRoomCreationInProgress);
            data.Write(m_fFactionKingdomRestrictionsOn);
            data.WritePkg(m_NameToNodeHash);
            data.WritePkg(m_IIDToNodeHash);
            data.WritePkg(m_motd);
            data.Write(m_chatActive);
            data.Write(m_fAllowNeutralsToBypassKingdomRestrictions);
            data.Write(m_factionType);
            data.Write(m_total);
            data.WritePkg(m_officerIDs);
        }
    }
}
