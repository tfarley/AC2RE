namespace AC2E.Def {

    public class CraftRegistry : IPackage {

        public PackageType packageType => PackageType.CraftRegistry;

        public ARHash<RecipeRecord> m_recipeRecords;
        public float m_CraftSkillScore;
        public uint m_CraftSkillTitle; // TODO: CraftSkillTitleType
        public double m_usageResetTime;
        public ARHash<CraftSkillRecord> m_hashCraftSkillRecords;
        public ARHash<RecipeRecord> m_hashRecipeRecords;

        public CraftRegistry() {

        }

        public CraftRegistry(AC2Reader data) {
            data.ReadPkg<ARHash<IPackage>>(v => m_recipeRecords = v.to<RecipeRecord>());
            m_CraftSkillScore = data.ReadSingle();
            m_CraftSkillTitle = data.ReadUInt32();
            m_usageResetTime = data.ReadDouble();
            data.ReadPkg<ARHash<IPackage>>(v => m_hashCraftSkillRecords = v.to<CraftSkillRecord>());
            data.ReadPkg<ARHash<IPackage>>(v => m_hashRecipeRecords = v.to<RecipeRecord>());
        }

        public void write(AC2Writer data) {
            data.WritePkg(m_recipeRecords);
            data.Write(m_CraftSkillScore);
            data.Write(m_CraftSkillTitle);
            data.Write(m_usageResetTime);
            data.WritePkg(m_hashCraftSkillRecords);
            data.WritePkg(m_hashRecipeRecords);
        }
    }
}
