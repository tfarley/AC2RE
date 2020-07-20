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

        public AllegianceProfile(AC2Reader data, PackageRegistry registry) {
            data.ReadPkgRef<StringInfo>(v => m_allegianceName = v, registry);
            data.ReadPkgRef<AllegianceData>(v => m_patron = v, registry);
            data.ReadPkgRef<AllegianceData>(v => m_member = v, registry);
            data.ReadPkgRef<AllegianceData>(v => m_monarch = v, registry);
            m_fMemberOnline = data.ReadBoolean();
            m_fPatronOnline = data.ReadBoolean();
            data.ReadPkgRef<RList<IPackage>>(v => m_vassals = v.to<AllegianceData>(), registry);
            data.ReadPkgRef<AList>(v => m_vassalsOnlineBools = new BoolList(v), registry);
            data.ReadPkgRef<StringInfo>(v => m_motd = v, registry);
            m_fMonarchOnline = data.ReadBoolean();
            m_factionType = data.ReadUInt32();
            m_total = data.ReadUInt32();
            data.ReadPkgRef<LAHashSet>(v => m_officerIDs = new InstanceIdHashSet(v), registry);
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(m_allegianceName, registry);
            data.Write(m_patron, registry);
            data.Write(m_member, registry);
            data.Write(m_monarch, registry);
            data.Write(m_fMemberOnline);
            data.Write(m_fPatronOnline);
            data.Write(m_vassals, registry);
            data.Write(m_vassalsOnlineBools, registry);
            data.Write(m_motd, registry);
            data.Write(m_fMonarchOnline);
            data.Write(m_factionType);
            data.Write(m_total);
            data.Write(m_officerIDs, registry);
        }
    }
}
