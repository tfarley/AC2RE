namespace AC2E.Def {

    public class AllegianceProfile : IPackage {

        public PackageType packageType => PackageType.AllegianceProfile;

        public StringInfo allegianceName; // m_allegianceName
        public AllegianceData patron; // m_patron
        public AllegianceData member; // m_member
        public AllegianceData monarch; // m_monarch
        public bool memberOnline; // m_fMemberOnline
        public bool patronOnline; // m_fPatronOnline
        public RList<AllegianceData> vassals; // m_vassals
        public BoolList vassalsOnline; // m_vassalsOnlineBools
        public StringInfo motd; // m_motd
        public bool monarchOnline; // m_fMonarchOnline
        public uint factionType; // m_factionType
        public uint total; // m_total
        public InstanceIdHashSet officerIds; // m_officerIDs

        public AllegianceProfile() {

        }

        public AllegianceProfile(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => allegianceName = v);
            data.ReadPkg<AllegianceData>(v => patron = v);
            data.ReadPkg<AllegianceData>(v => member = v);
            data.ReadPkg<AllegianceData>(v => monarch = v);
            memberOnline = data.ReadBoolean();
            patronOnline = data.ReadBoolean();
            data.ReadPkg<RList<IPackage>>(v => vassals = v.to<AllegianceData>());
            data.ReadPkg<AList>(v => vassalsOnline = new BoolList(v));
            data.ReadPkg<StringInfo>(v => motd = v);
            monarchOnline = data.ReadBoolean();
            factionType = data.ReadUInt32();
            total = data.ReadUInt32();
            data.ReadPkg<LAHashSet>(v => officerIds = new InstanceIdHashSet(v));
        }

        public void write(AC2Writer data) {
            data.WritePkg(allegianceName);
            data.WritePkg(patron);
            data.WritePkg(member);
            data.WritePkg(monarch);
            data.Write(memberOnline);
            data.Write(patronOnline);
            data.WritePkg(vassals);
            data.WritePkg(vassalsOnline);
            data.WritePkg(motd);
            data.Write(monarchOnline);
            data.Write(factionType);
            data.Write(total);
            data.WritePkg(officerIds);
        }
    }
}
