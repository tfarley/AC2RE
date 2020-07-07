using AC2E.Def;
using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class SkillRepositoryPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.SkillRepository;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public uint m_nSkillCredits;
        public ulong m_nUntrainXP;
        public uint m_nHeroSkillCredits;
        public AAHashPkg m_hashPerkTypes;
        public uint m_typeUntrained;
        public AAHashPkg m_hashCategories;
        public ARHashPkg<SkillInfoPkg> m_hashSkills;

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
