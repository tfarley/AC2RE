using System.Collections.Generic;

namespace AC2RE.Definitions;

public class AllegianceHierarchy : IHeapObject {

    public PackageType packageType => PackageType.AllegianceHierarchy;

    public class AllegianceNode : IHeapObject {

        public PackageType packageType => PackageType.AllegianceNode;

        public List<AllegianceNode> vassals; // m_vassalNodes
        public AllegianceNode patron; // m_patron
        public AllegianceData allegianceData; // m_data
        public AllegianceNode vassal; // m_vassal
        public AllegianceNode peer; // m_peer

        public AllegianceNode() {

        }

        public AllegianceNode(AC2Reader data) {
            data.ReadHO<RList>(v => vassals = v.to<AllegianceNode>());
            data.ReadHO<AllegianceNode>(v => patron = v);
            data.ReadHO<AllegianceData>(v => allegianceData = v);
            data.ReadHO<AllegianceNode>(v => vassal = v);
            data.ReadHO<AllegianceNode>(v => peer = v);
        }

        public void write(AC2Writer data) {
            data.WriteHO(RList.from(vassals));
            data.WriteHO(patron);
            data.WriteHO(allegianceData);
            data.WriteHO(vassal);
            data.WriteHO(peer);
        }
    }

    public uint removalRoomId; // m_removalRoomID
    public StringInfo allegianceName; // m_allegianceName
    public WPString recallLocation; // m_recallLocation
    public AllegianceNode monarch; // m_monarch
    public int realTimeLastRename; // m_realTimeLastRename
    public WPString roomName; // m_roomName
    public bool chatRoomCreationInProgress; // m_fChatRoomCreationInProgress
    public bool factionKingdomRestrictionsOn; // m_fFactionKingdomRestrictionsOn
    public Dictionary<WPString, AllegianceNode> nameToNode; // m_NameToNodeHash
    public Dictionary<InstanceId, AllegianceNode> idToNode; // m_IIDToNodeHash
    public StringInfo motd; // m_motd
    public bool chatActive; // m_chatActive
    public bool allowNeutralsToBypassKingdomRestrictions; // m_fAllowNeutralsToBypassKingdomRestrictions
    public FactionType factionType; // m_factionType
    public uint total; // m_total
    public HashSet<ulong> officerIds; // m_officerIDs

    public AllegianceHierarchy() {

    }

    public AllegianceHierarchy(AC2Reader data) {
        removalRoomId = data.ReadUInt32();
        data.ReadHO<StringInfo>(v => allegianceName = v);
        data.ReadHO<WPString>(v => recallLocation = v);
        data.ReadHO<AllegianceNode>(v => monarch = v);
        realTimeLastRename = data.ReadInt32();
        data.ReadHO<WPString>(v => roomName = v);
        chatRoomCreationInProgress = data.ReadBoolean();
        factionKingdomRestrictionsOn = data.ReadBoolean();
        data.ReadHO<NRHash>(v => nameToNode = v.to<WPString, AllegianceNode>());
        data.ReadHO<LRHash>(v => idToNode = v.to<InstanceId, AllegianceNode>());
        data.ReadHO<StringInfo>(v => motd = v);
        chatActive = data.ReadBoolean();
        allowNeutralsToBypassKingdomRestrictions = data.ReadBoolean();
        factionType = data.ReadEnum<FactionType>();
        total = data.ReadUInt32();
        data.ReadHO<LAHashSet>(v => officerIds = v);
    }

    public void write(AC2Writer data) {
        data.Write(removalRoomId);
        data.WriteHO(allegianceName);
        data.WriteHO(recallLocation);
        data.WriteHO(monarch);
        data.Write(realTimeLastRename);
        data.WriteHO(roomName);
        data.Write(chatRoomCreationInProgress);
        data.Write(factionKingdomRestrictionsOn);
        data.WriteHO(NRHash.from(nameToNode));
        data.WriteHO(LRHash.from(idToNode));
        data.WriteHO(motd);
        data.Write(chatActive);
        data.Write(allowNeutralsToBypassKingdomRestrictions);
        data.WriteEnum(factionType);
        data.Write(total);
        data.WriteHO(LAHashSet.from(officerIds));
    }
}
