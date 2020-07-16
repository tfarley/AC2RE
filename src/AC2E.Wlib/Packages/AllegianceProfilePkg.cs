using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class AllegianceProfilePkg : IPackage {

        public PackageType packageType => PackageType.AllegianceProfile;

        public StringInfo m_allegianceName;
        public AllegianceDataPkg m_patron;
        public AllegianceDataPkg m_member;
        public AllegianceDataPkg m_monarch;
        public bool m_fMemberOnline;
        public bool m_fPatronOnline;
        public RList<AllegianceDataPkg> m_vassals;
        public BoolList m_vassalsOnlineBools;
        public StringInfo m_motd;
        public bool m_fMonarchOnline;
        public uint m_factionType;
        public uint m_total;
        public InstanceIdHashSet m_officerIDs;

        public AllegianceProfilePkg() {

        }

        public AllegianceProfilePkg(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<StringInfo>(v => m_allegianceName = v, registry);
            data.ReadPkgRef<AllegianceDataPkg>(v => m_patron = v, registry);
            data.ReadPkgRef<AllegianceDataPkg>(v => m_member = v, registry);
            data.ReadPkgRef<AllegianceDataPkg>(v => m_monarch = v, registry);
            m_fMemberOnline = data.ReadUInt32() != 0;
            m_fPatronOnline = data.ReadUInt32() != 0;
            data.ReadPkgRef<RList<IPackage>>(v => m_vassals = v.to<AllegianceDataPkg>(), registry);
            data.ReadPkgRef<AList>(v => m_vassalsOnlineBools = new BoolList(v), registry);
            data.ReadPkgRef<StringInfo>(v => m_motd = v, registry);
            m_fMonarchOnline = data.ReadUInt32() != 0;
            m_factionType = data.ReadUInt32();
            m_total = data.ReadUInt32();
            data.ReadPkgRef<LAHashSet>(v => m_officerIDs = new InstanceIdHashSet(v), registry);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_allegianceName, registry);
            data.Write(m_patron, registry);
            data.Write(m_member, registry);
            data.Write(m_monarch, registry);
            data.Write(m_fMemberOnline ? (uint)1 : (uint)0);
            data.Write(m_fPatronOnline ? (uint)1 : (uint)0);
            data.Write(m_vassals, registry);
            data.Write(m_vassalsOnlineBools, registry);
            data.Write(m_motd, registry);
            data.Write(m_fMonarchOnline ? (uint)1 : (uint)0);
            data.Write(m_factionType);
            data.Write(m_total);
            data.Write(m_officerIDs, registry);
        }
    }
}
