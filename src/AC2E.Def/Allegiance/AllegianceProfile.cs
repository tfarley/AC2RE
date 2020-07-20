namespace AC2E.Def {

    public class AllegianceProfile : IPackage {

        public PackageType packageType => PackageType.AllegianceProfile;

        public StringInfo m_allegianceName;
        public AllegianceData m_patron;
        public AllegianceData m_member;
        public AllegianceData m_monarch;
        public bool m_fMemberOnline;
        public bool m_fPatronOnline;
        public RList<AllegianceData> m_vassals;
        public BoolList m_vassalsOnlineBools;
        public StringInfo m_motd;
        public bool m_fMonarchOnline;
        public uint m_factionType;
        public uint m_total;
        public InstanceIdHashSet m_officerIDs;

        public AllegianceProfile() {

        }

        public AllegianceProfile(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => m_allegianceName = v);
            data.ReadPkg<AllegianceData>(v => m_patron = v);
            data.ReadPkg<AllegianceData>(v => m_member = v);
            data.ReadPkg<AllegianceData>(v => m_monarch = v);
            m_fMemberOnline = data.ReadBoolean();
            m_fPatronOnline = data.ReadBoolean();
            data.ReadPkg<RList<IPackage>>(v => m_vassals = v.to<AllegianceData>());
            data.ReadPkg<AList>(v => m_vassalsOnlineBools = new BoolList(v));
            data.ReadPkg<StringInfo>(v => m_motd = v);
            m_fMonarchOnline = data.ReadBoolean();
            m_factionType = data.ReadUInt32();
            m_total = data.ReadUInt32();
            data.ReadPkg<LAHashSet>(v => m_officerIDs = new InstanceIdHashSet(v));
        }

        public void write(AC2Writer data) {
            data.WritePkg(m_allegianceName);
            data.WritePkg(m_patron);
            data.WritePkg(m_member);
            data.WritePkg(m_monarch);
            data.Write(m_fMemberOnline);
            data.Write(m_fPatronOnline);
            data.WritePkg(m_vassals);
            data.WritePkg(m_vassalsOnlineBools);
            data.WritePkg(m_motd);
            data.Write(m_fMonarchOnline);
            data.Write(m_factionType);
            data.Write(m_total);
            data.WritePkg(m_officerIDs);
        }
    }
}
