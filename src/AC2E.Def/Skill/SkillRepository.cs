namespace AC2E.Def {

    public class SkillRepository : IPackage {

        public PackageType packageType => PackageType.SkillRepository;

        public uint m_nSkillCredits;
        public ulong m_nUntrainXP;
        public uint m_nHeroSkillCredits;
        public AAHash m_hashPerkTypes;
        public uint m_typeUntrained;
        public AAHash m_hashCategories;
        public ARHash<SkillInfo> m_hashSkills;

        public SkillRepository() {

        }

        public SkillRepository(AC2Reader data) {
            m_nSkillCredits = data.ReadUInt32();
            m_nUntrainXP = data.ReadUInt64();
            m_nHeroSkillCredits = data.ReadUInt32();
            data.ReadPkg<AAHash>(v => m_hashPerkTypes = v);
            m_typeUntrained = data.ReadUInt32();
            data.ReadPkg<AAHash>(v => m_hashCategories = v);
            data.ReadPkg<ARHash<IPackage>>(v => m_hashSkills = v.to<SkillInfo>());
        }

        public void write(AC2Writer data) {
            data.Write(m_nSkillCredits);
            data.Write(m_nUntrainXP);
            data.Write(m_nHeroSkillCredits);
            data.WritePkg(m_hashPerkTypes);
            data.Write(m_typeUntrained);
            data.WritePkg(m_hashCategories);
            data.WritePkg(m_hashSkills);
        }
    }
}
