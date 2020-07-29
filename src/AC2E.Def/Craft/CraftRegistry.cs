﻿namespace AC2E.Def {

    public class CraftRegistry : IPackage {

        public PackageType packageType => PackageType.CraftRegistry;

        public ARHash<RecipeRecord> recipeRecords; // m_recipeRecords
        public float craftSkillScore; // m_CraftSkillScore
        public uint craftSkillTitle; // m_CraftSkillTitle // TODO: CraftSkillTitleType
        public double usageResetTime; // m_usageResetTime
        public ARHash<CraftSkillRecord> craftSkillRecords; // m_hashCraftSkillRecords
        public ARHash<RecipeRecord> recipeRecords2; // m_hashRecipeRecords

        public CraftRegistry() {

        }

        public CraftRegistry(AC2Reader data) {
            data.ReadPkg<ARHash<IPackage>>(v => recipeRecords = v.to<RecipeRecord>());
            craftSkillScore = data.ReadSingle();
            craftSkillTitle = data.ReadUInt32();
            usageResetTime = data.ReadDouble();
            data.ReadPkg<ARHash<IPackage>>(v => craftSkillRecords = v.to<CraftSkillRecord>());
            data.ReadPkg<ARHash<IPackage>>(v => recipeRecords2 = v.to<RecipeRecord>());
        }

        public void write(AC2Writer data) {
            data.WritePkg(recipeRecords);
            data.Write(craftSkillScore);
            data.Write(craftSkillTitle);
            data.Write(usageResetTime);
            data.WritePkg(craftSkillRecords);
            data.WritePkg(recipeRecords2);
        }
    }
}
