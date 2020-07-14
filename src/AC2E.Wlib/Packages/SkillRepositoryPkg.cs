﻿using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class SkillRepositoryPkg : IPackage {

        public PackageType packageType => PackageType.SkillRepository;

        public uint m_nSkillCredits;
        public ulong m_nUntrainXP;
        public uint m_nHeroSkillCredits;
        public AAHash m_hashPerkTypes;
        public uint m_typeUntrained;
        public AAHash m_hashCategories;
        public ARHash<SkillInfoPkg> m_hashSkills;

        public SkillRepositoryPkg() {

        }

        public SkillRepositoryPkg(BinaryReader data, PackageRegistry registry) {
            m_nSkillCredits = data.ReadUInt32();
            m_nUntrainXP = data.ReadUInt64();
            m_nHeroSkillCredits = data.ReadUInt32();
            data.ReadPkgRef<AAHash>(v => m_hashPerkTypes = v, registry);
            m_typeUntrained = data.ReadUInt32();
            data.ReadPkgRef<AAHash>(v => m_hashCategories = v, registry);
            data.ReadPkgRef<ARHash<IPackage>>(v => m_hashSkills = v.to<SkillInfoPkg>(), registry);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_nSkillCredits);
            data.Write(m_nUntrainXP);
            data.Write(m_nHeroSkillCredits);
            data.Write(m_hashPerkTypes, registry);
            data.Write(m_typeUntrained);
            data.Write(m_hashCategories, registry);
            data.Write(m_hashSkills, registry);
        }
    }
}
