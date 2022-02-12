using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CraftRegistry : IHeapObject {

    public PackageType packageType => PackageType.CraftRegistry;

    public Dictionary<uint, RecipeRecord> recipeRecords; // m_recipeRecords
    public float craftSkillScore; // m_CraftSkillScore
    public CraftSkillTitleType craftSkillTitle; // m_CraftSkillTitle
    public double usageResetTime; // m_usageResetTime
    public Dictionary<DataId, CraftSkillRecord> craftSkillRecords; // m_hashCraftSkillRecords
    public Dictionary<DataId, RecipeRecord> recipeRecords2; // m_hashRecipeRecords

    public CraftRegistry() {

    }

    public CraftRegistry(AC2Reader data) {
        data.ReadHO<ARHash>(v => recipeRecords = v.to<uint, RecipeRecord>());
        craftSkillScore = data.ReadSingle();
        craftSkillTitle = data.ReadEnum<CraftSkillTitleType>();
        usageResetTime = data.ReadDouble();
        data.ReadHO<ARHash>(v => craftSkillRecords = v.to<DataId, CraftSkillRecord>());
        data.ReadHO<ARHash>(v => recipeRecords2 = v.to<DataId, RecipeRecord>());
    }

    public void write(AC2Writer data) {
        data.WriteHO(ARHash.from(recipeRecords));
        data.Write(craftSkillScore);
        data.WriteEnum(craftSkillTitle);
        data.Write(usageResetTime);
        data.WriteHO(ARHash.from(craftSkillRecords));
        data.WriteHO(ARHash.from(recipeRecords2));
    }
}
