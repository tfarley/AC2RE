using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class SkillRepositoryPkg : IPackage {

        public PackageType packageType => PackageType.SkillRepository;

        public uint m_nSkillCredits;
        public ulong m_nUntrainXP;
        public uint m_nHeroSkillCredits;
        public PkgRef<AAHashPkg> m_hashPerkTypes;
        public uint m_typeUntrained;
        public PkgRef<AAHashPkg> m_hashCategories;
        public PkgRef<ARHashPkg<SkillInfoPkg>> m_hashSkills;

        public SkillRepositoryPkg() {

        }

        public SkillRepositoryPkg(BinaryReader data) {
            m_nSkillCredits = data.ReadUInt32();
            m_nUntrainXP = data.ReadUInt64();
            m_nHeroSkillCredits = data.ReadUInt32();
            m_hashPerkTypes = data.ReadPkgRef<AAHashPkg>();
            m_typeUntrained = data.ReadUInt32();
            m_hashCategories = data.ReadPkgRef<AAHashPkg>();
            m_hashSkills = data.ReadPkgRef<ARHashPkg<SkillInfoPkg>>();
        }

        public void resolveRefs() {
            PackageManager.convert<ARHashPkg<IPackage>>(m_hashSkills.id, v => v.to<SkillInfoPkg>());
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(m_nSkillCredits);
            data.Write(m_nUntrainXP);
            data.Write(m_nHeroSkillCredits);
            data.Write(m_hashPerkTypes, references);
            data.Write(m_typeUntrained);
            data.Write(m_hashCategories, references);
            data.Write(m_hashSkills, references);
        }
    }
}
