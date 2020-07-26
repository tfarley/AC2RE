namespace AC2E.Def {

    public class SkillRepository : IPackage {

        public PackageType packageType => PackageType.SkillRepository;

        public uint skillCredits; // m_nSkillCredits
        public ulong untrainXp; // m_nUntrainXP
        public uint heroSkillCredits; // m_nHeroSkillCredits
        public AAHash perkTypes; // m_hashPerkTypes
        public uint typeUntrained; // m_typeUntrained
        public AAHash categories; // m_hashCategories
        public ARHash<SkillInfo> skills; // m_hashSkills

        public SkillRepository() {

        }

        public SkillRepository(AC2Reader data) {
            skillCredits = data.ReadUInt32();
            untrainXp = data.ReadUInt64();
            heroSkillCredits = data.ReadUInt32();
            data.ReadPkg<AAHash>(v => perkTypes = v);
            typeUntrained = data.ReadUInt32();
            data.ReadPkg<AAHash>(v => categories = v);
            data.ReadPkg<ARHash<IPackage>>(v => skills = v.to<SkillInfo>());
        }

        public void write(AC2Writer data) {
            data.Write(skillCredits);
            data.Write(untrainXp);
            data.Write(heroSkillCredits);
            data.WritePkg(perkTypes);
            data.Write(typeUntrained);
            data.WritePkg(categories);
            data.WritePkg(skills);
        }
    }
}
