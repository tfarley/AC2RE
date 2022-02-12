using System.Collections.Generic;

namespace AC2RE.Definitions;

public class AllegianceProfile : IHeapObject {

    public PackageType packageType => PackageType.AllegianceProfile;

    public StringInfo allegianceName; // m_allegianceName
    public AllegianceData patron; // m_patron
    public AllegianceData member; // m_member
    public AllegianceData monarch; // m_monarch
    public bool memberOnline; // m_fMemberOnline
    public bool patronOnline; // m_fPatronOnline
    public List<AllegianceData> vassals; // m_vassals
    public List<bool> vassalsOnline; // m_vassalsOnlineBools
    public StringInfo motd; // m_motd
    public bool monarchOnline; // m_fMonarchOnline
    public FactionType factionType; // m_factionType
    public uint total; // m_total
    public HashSet<InstanceId> officerIds; // m_officerIDs

    public AllegianceProfile() {

    }

    public AllegianceProfile(AC2Reader data) {
        data.ReadHO<StringInfo>(v => allegianceName = v);
        data.ReadHO<AllegianceData>(v => patron = v);
        data.ReadHO<AllegianceData>(v => member = v);
        data.ReadHO<AllegianceData>(v => monarch = v);
        memberOnline = data.ReadBoolean();
        patronOnline = data.ReadBoolean();
        data.ReadHO<RList>(v => vassals = v.to<AllegianceData>());
        data.ReadHO<AList>(v => vassalsOnline = v.to<bool>());
        data.ReadHO<StringInfo>(v => motd = v);
        monarchOnline = data.ReadBoolean();
        factionType = data.ReadEnum<FactionType>();
        total = data.ReadUInt32();
        data.ReadHO<LAHashSet>(v => officerIds = v.to<InstanceId>());
    }

    public void write(AC2Writer data) {
        data.WriteHO(allegianceName);
        data.WriteHO(patron);
        data.WriteHO(member);
        data.WriteHO(monarch);
        data.Write(memberOnline);
        data.Write(patronOnline);
        data.WriteHO(RList.from(vassals));
        data.WriteHO(AList.from(vassalsOnline));
        data.WriteHO(motd);
        data.Write(monarchOnline);
        data.WriteEnum(factionType);
        data.Write(total);
        data.WriteHO(LAHashSet.from(officerIds));
    }
}
