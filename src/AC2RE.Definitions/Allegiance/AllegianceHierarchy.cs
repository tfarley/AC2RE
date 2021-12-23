using System.Collections.Generic;

namespace AC2RE.Definitions;

public class AllegianceHierarchy : IPackage {

    public PackageType packageType => PackageType.AllegianceHierarchy;

    public class AllegianceNode : IPackage {

        public PackageType packageType => PackageType.AllegianceNode;

        public List<AllegianceNode> vassals; // m_vassalNodes
        public AllegianceNode patron; // m_patron
        public AllegianceData allegianceData; // m_data
        public AllegianceNode vassal; // m_vassal
        public AllegianceNode peer; // m_peer

        public AllegianceNode() {

        }

        public AllegianceNode(AC2Reader data) {
            data.ReadPkg<RList>(v => vassals = v.to<AllegianceNode>());
            data.ReadPkg<AllegianceNode>(v => patron = v);
            data.ReadPkg<AllegianceData>(v => allegianceData = v);
            data.ReadPkg<AllegianceNode>(v => vassal = v);
            data.ReadPkg<AllegianceNode>(v => peer = v);
        }

        public void write(AC2Writer data) {
            data.WritePkg(RList.from(vassals));
            data.WritePkg(patron);
            data.WritePkg(allegianceData);
            data.WritePkg(vassal);
            data.WritePkg(peer);
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
        data.ReadPkg<StringInfo>(v => allegianceName = v);
        data.ReadPkg<WPString>(v => recallLocation = v);
        data.ReadPkg<AllegianceNode>(v => monarch = v);
        realTimeLastRename = data.ReadInt32();
        data.ReadPkg<WPString>(v => roomName = v);
        chatRoomCreationInProgress = data.ReadBoolean();
        factionKingdomRestrictionsOn = data.ReadBoolean();
        data.ReadPkg<NRHash>(v => nameToNode = v.to<WPString, AllegianceNode>());
        data.ReadPkg<LRHash>(v => idToNode = v.to<InstanceId, AllegianceNode>());
        data.ReadPkg<StringInfo>(v => motd = v);
        chatActive = data.ReadBoolean();
        allowNeutralsToBypassKingdomRestrictions = data.ReadBoolean();
        factionType = (FactionType)data.ReadUInt32();
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
        data.WritePkg(NRHash.from(nameToNode));
        data.WritePkg(LRHash.from(idToNode));
        data.WritePkg(motd);
        data.Write(chatActive);
        data.Write(allowNeutralsToBypassKingdomRestrictions);
        data.Write((uint)factionType);
        data.Write(total);
        data.WritePkg(LAHashSet.from(officerIds));
    }
}
