using AC2E.Interp;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class SkillRepositoryPkg : IPackage {

        public PackageType packageType => PackageType.SkillRepository;

        public uint m_nSkillCredits;
        public ulong m_nUntrainXP;
        public uint m_nHeroSkillCredits;
        public AAHashPkg m_hashPerkTypes;
        public uint m_typeUntrained;
        public AAHashPkg m_hashCategories;
        public ARHashPkg<SkillInfoPkg> m_hashSkills;

        public SkillRepositoryPkg() {

        }

        public SkillRepositoryPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            m_nSkillCredits = data.ReadUInt32();
            m_nUntrainXP = data.ReadUInt64();
            m_nHeroSkillCredits = data.ReadUInt32();
            data.ReadPkgRef<AAHashPkg>(v => m_hashPerkTypes = v, resolvers);
            m_typeUntrained = data.ReadUInt32();
            data.ReadPkgRef<AAHashPkg>(v => m_hashCategories = v, resolvers);
            data.ReadPkgRef<ARHashPkg<IPackage>>(v => m_hashSkills = v.to<SkillInfoPkg>(), resolvers);
        }

        public void write(BinaryWriter data, List<IPackage> references) {
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
