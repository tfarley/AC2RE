using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class CraftRegistry : IPackage {

        public PackageType packageType => PackageType.CraftRegistry;

        public Dictionary<uint, RecipeRecord> recipeRecords; // m_recipeRecords
        public float craftSkillScore; // m_CraftSkillScore
        public uint craftSkillTitle; // m_CraftSkillTitle // TODO: CraftSkillTitleType
        public double usageResetTime; // m_usageResetTime
        public Dictionary<uint, CraftSkillRecord> craftSkillRecords; // m_hashCraftSkillRecords
        public Dictionary<uint, RecipeRecord> recipeRecords2; // m_hashRecipeRecords

        public CraftRegistry() {

        }

        public CraftRegistry(AC2Reader data) {
            data.ReadPkg<ARHash>(v => recipeRecords = v.to<uint, RecipeRecord>());
            craftSkillScore = data.ReadSingle();
            craftSkillTitle = data.ReadUInt32();
            usageResetTime = data.ReadDouble();
            data.ReadPkg<ARHash>(v => craftSkillRecords = v.to<uint, CraftSkillRecord>());
            data.ReadPkg<ARHash>(v => recipeRecords2 = v.to<uint, RecipeRecord>());
        }

        public void write(AC2Writer data) {
            data.WritePkg(ARHash.from(recipeRecords));
            data.Write(craftSkillScore);
            data.Write(craftSkillTitle);
            data.Write(usageResetTime);
            data.WritePkg(ARHash.from(craftSkillRecords));
            data.WritePkg(ARHash.from(recipeRecords2));
        }
    }
}
