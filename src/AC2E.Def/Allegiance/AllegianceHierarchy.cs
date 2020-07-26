namespace AC2E.Def {

    public class AllegianceHierarchy : IPackage {

        public PackageType packageType => PackageType.AllegianceHierarchy;

        public uint removalRoomId; // m_removalRoomID
        public StringInfo allegianceName; // m_allegianceName
        public WPString recallLocation; // m_recallLocation
        public AllegianceNode monarch; // m_monarch
        public int realTimeLastRename; // m_realTimeLastRename
        public WPString roomName; // m_roomName
        public bool chatRoomCreationInProgress; // m_fChatRoomCreationInProgress
        public bool factionKingdomRestrictionsOn; // m_fFactionKingdomRestrictionsOn
        public NRHash<WPString, AllegianceNode> nameToNode; // m_NameToNodeHash
        public InstanceIdRHash<AllegianceNode> idToNode; // m_IIDToNodeHash
        public StringInfo motd; // m_motd
        public bool chatActive; // m_chatActive
        public bool allowNeutralsToBypassKingdomRestrictions; // m_fAllowNeutralsToBypassKingdomRestrictions
        public uint factionType; // m_factionType
        public uint total; // m_total
        public LAHashSet officerIds; // m_officerIDs

        public AllegianceHierarchy() {

        }

        public AllegianceHierarchy(AC2Reader data) {
            removalRoomId = data.ReadUInt32();
            data.ReadPkg<StringInfo>(v => allegianceName = v);
            data.ReadPkg<WPString>(v => recallLocation = v);
            data.ReadPkg<AllegianceNode>(v => monarch = v);
            realTimeLastRename = data.ReadInt32();
            data.ReadPkg<WPString>(v => roomName = v);
            chatRoomCreationInProgress = data.ReadBoolean();
            factionKingdomRestrictionsOn = data.ReadBoolean();
            data.ReadPkg<NRHash<IPackage, IPackage>>(v => nameToNode = v.to<WPString, AllegianceNode>());
            data.ReadPkg<LRHash<IPackage>>(v => idToNode = new InstanceIdRHash<AllegianceNode>(v.to<AllegianceNode>()));
            data.ReadPkg<StringInfo>(v => motd = v);
            chatActive = data.ReadBoolean();
            allowNeutralsToBypassKingdomRestrictions = data.ReadBoolean();
            factionType = data.ReadUInt32();
            total = data.ReadUInt32();
            data.ReadPkg<LAHashSet>(v => officerIds = v);
        }

        public void write(AC2Writer data) {
            data.Write(removalRoomId);
            data.WritePkg(allegianceName);
            data.WritePkg(recallLocation);
            data.WritePkg(monarch);
            data.Write(realTimeLastRename);
            data.WritePkg(roomName);
            data.Write(chatRoomCreationInProgress);
            data.Write(factionKingdomRestrictionsOn);
            data.WritePkg(nameToNode);
            data.WritePkg(idToNode);
            data.WritePkg(motd);
            data.Write(chatActive);
            data.Write(allowNeutralsToBypassKingdomRestrictions);
            data.Write(factionType);
            data.Write(total);
            data.WritePkg(officerIds);
        }
    }
}
