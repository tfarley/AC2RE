using System.Collections.Generic;

namespace AC2E.Def {

    public class AllegianceProfile : IPackage {

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
            data.ReadPkg<StringInfo>(v => allegianceName = v);
            data.ReadPkg<AllegianceData>(v => patron = v);
            data.ReadPkg<AllegianceData>(v => member = v);
            data.ReadPkg<AllegianceData>(v => monarch = v);
            memberOnline = data.ReadBoolean();
            patronOnline = data.ReadBoolean();
            data.ReadPkg<RList>(v => vassals = v.to<AllegianceData>());
            data.ReadPkg<AList>(v => vassalsOnline = v.to<bool>());
            data.ReadPkg<StringInfo>(v => motd = v);
            monarchOnline = data.ReadBoolean();
            factionType = (FactionType)data.ReadUInt32();
            total = data.ReadUInt32();
            data.ReadPkg<LAHashSet>(v => officerIds = v.to<InstanceId>());
        }

        public void write(AC2Writer data) {
            data.WritePkg(allegianceName);
            data.WritePkg(patron);
            data.WritePkg(member);
            data.WritePkg(monarch);
            data.Write(memberOnline);
            data.Write(patronOnline);
            data.WritePkg(RList.from(vassals));
            data.WritePkg(AList.from(vassalsOnline));
            data.WritePkg(motd);
            data.Write(monarchOnline);
            data.Write((uint)factionType);
            data.Write(total);
            data.WritePkg(LAHashSet.from(officerIds));
        }
    }
}
