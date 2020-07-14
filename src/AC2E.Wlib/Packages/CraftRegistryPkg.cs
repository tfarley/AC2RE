using AC2E.Interp;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class CraftRegistryPkg : IPackage {

        public PackageType packageType => PackageType.CraftRegistry;

        public PkgRef<ARHashPkg<RecipeRecordPkg>> m_recipeRecords;
        public float m_CraftSkillScore;
        public uint m_CraftSkillTitle; // TODO: CraftSkillTitleType
        public double m_usageResetTime;
        public PkgRef<ARHashPkg<CraftSkillRecordPkg>> m_hashCraftSkillRecords;
        public PkgRef<ARHashPkg<RecipeRecordPkg>> m_hashRecipeRecords;

        public CraftRegistryPkg() {

        }

        public CraftRegistryPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            m_recipeRecords = data.ReadPkgRef<ARHashPkg<IPackage>, ARHashPkg<RecipeRecordPkg>>(resolvers, v => v.to<RecipeRecordPkg>());
            m_CraftSkillScore = data.ReadSingle();
            m_CraftSkillTitle = data.ReadUInt32();
            m_usageResetTime = data.ReadDouble();
            m_hashCraftSkillRecords = data.ReadPkgRef<ARHashPkg<IPackage>, ARHashPkg<CraftSkillRecordPkg>>(resolvers, v => v.to<CraftSkillRecordPkg>());
            m_hashRecipeRecords = data.ReadPkgRef<ARHashPkg<IPackage>, ARHashPkg<RecipeRecordPkg>>(resolvers, v => v.to<RecipeRecordPkg>());
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(m_recipeRecords, references);
            data.Write(m_CraftSkillScore);
            data.Write(m_CraftSkillTitle);
            data.Write(m_usageResetTime);
            data.Write(m_hashCraftSkillRecords, references);
            data.Write(m_hashRecipeRecords, references);
        }
    }
}
