using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class CraftRegistryPkg : IPackage {

        public PackageType packageType => PackageType.CraftRegistry;

        public ARHashPkg<RecipeRecordPkg> m_recipeRecords;
        public float m_CraftSkillScore;
        public uint m_CraftSkillTitle; // TODO: CraftSkillTitleType
        public double m_usageResetTime;
        public ARHashPkg<CraftSkillRecordPkg> m_hashCraftSkillRecords;
        public ARHashPkg<RecipeRecordPkg> m_hashRecipeRecords;

        public CraftRegistryPkg() {

        }

        public CraftRegistryPkg(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<ARHashPkg<IPackage>>(v => m_recipeRecords = v.to<RecipeRecordPkg>(), registry);
            m_CraftSkillScore = data.ReadSingle();
            m_CraftSkillTitle = data.ReadUInt32();
            m_usageResetTime = data.ReadDouble();
            data.ReadPkgRef<ARHashPkg<IPackage>>(v => m_hashCraftSkillRecords = v.to<CraftSkillRecordPkg>(), registry);
            data.ReadPkgRef<ARHashPkg<IPackage>>(v => m_hashRecipeRecords = v.to<RecipeRecordPkg>(), registry);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_recipeRecords, registry);
            data.Write(m_CraftSkillScore);
            data.Write(m_CraftSkillTitle);
            data.Write(m_usageResetTime);
            data.Write(m_hashCraftSkillRecords, registry);
            data.Write(m_hashRecipeRecords, registry);
        }
    }
}
