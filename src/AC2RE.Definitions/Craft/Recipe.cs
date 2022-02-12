using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Recipe : IHeapObject {

    public virtual PackageType packageType => PackageType.Recipe;

    // WLib Recipe
    [Flags]
    public enum Flag : uint {
        None = 0,
        IsFixedCost = 1 << 0, // IsFixedCost 0x00000001
        IsDynamicCost = 1 << 1, // IsDynamicCost 0x00000002

        IsHiddenWhenZeroCharges = 1 << 3, // IsHiddenWhenZeroCharges 0x00000008
        HasMinLevelRestriction = 1 << 4, // InqMinLevelRestriction 0x00000010
        HasMaxLevelRestriction = 1 << 5, // InqMaxLevelRestriction 0x00000020
        IsChargeRefreshing = 1 << 6, // IsChargeRefreshing 0x00000040
        UsesSpinner = 1 << 7, // UsesSpinner 0x00000080
        IsFixedDifficulty = 1 << 8, // IsFixedDifficulty 0x00000100
        IsDynamicDifficulty = 1 << 9, // IsDynamicDifficulty 0x00000200
        HasDifficultyMinusSkillCutoff = 1 << 10, // HasDifficultyMinusSkillCutoff 0x00000400
        IsFixedCraftXP = 1 << 11, // IsFixedCraftXP 0x00000800
        IsDynamicCraftXP = 1 << 12, // IsDynamicCraftXP 0x00001000
        HasRequiredEffect = 1 << 13, // InqRequiredEffect 0x00002000
        HasReadOnlySpinner = 1 << 14, // HasReadOnlySpinner 0x00004000
        HasSkillCheckOverride = 1 << 15, // HasSkillCheckOverride 0x00008000
        HasNameColoringTable = 1 << 16, // HasNameColoringTable 0x00010000
    }

    public int difficultyMinusSkillCutoff; // m_difficultyMinusSkillCutoff
    public SingletonPkg<LevelMappingTable> costMappingTable; // m_costMappingTable
    public Dictionary<uint, StringInfo> craftMessageOverrides; // m_craftMessageOverrides
    public SingletonPkg<LevelMappingTable> m_difficultyMappingTable; // m_difficultyMappingTable
    public DataId iconDid; // m_iconDID
    public Dictionary<uint, CraftCheckEntry> craftCheckEntries; // m_craftCheckEntries
    public uint craftSkillCategory; // m_craftSkillCategory
    public List<Ingredient> ingredients; // m_ingredients
    public StringInfo name; // m_siName
    public StringInfo description; // m_siDesc
    public uint maxLevel; // m_maxLevel
    public DataId craftSkillDid; // m_didCraftSkill
    public uint maxNumIngredients; // m_maxNumIngredients
    public uint minSpinnerVal; // m_minSpinnerVal
    public float bonus; // m_fBonus
    public int charges; // m_iCharges
    public SingletonPkg<RecipeNameColoringTable> nameColoringTable; // m_nameColoringTable
    public int chargeRefreshPeriod; // m_chargeRefreshPeriod
    public uint numAnimCycles; // m_uiNumAnimCycles
    public List<uint> craftThresholds; // m_craftThreshs
    public Flag flags; // m_flags
    public uint maxSpinnerVal; // m_maxSpinnerVal
    public uint minLevel; // m_minLevel
    public uint lastProductOrdinal; // m_uiLastProductOrdinal
    public SingletonPkg<LevelMappingTable> craftXpMappingTable; // m_craftXPMappingTable
    public CraftRandomEntry curRandEntry; // m_curRandEntry
    public Dictionary<uint, uint> targetsHash; // m_hashTargets
    public uint craftXp; // m_uiCraftXP
    public bool requiresTarget; // m_bRequiresTarget
    public uint difficulty; // m_uiDifficulty
    public SingletonPkg<SkillCheck> skillCheckOverride; // m_skillCheckOverride
    public SingletonPkg<Effect> requiredEffect; // m_requiredEffect
    public uint animation; // m_animation
    public float recoveryTime; // m_recoveryTime
    public int cost; // m_cost
    public CraftCheckEntry curCraftCheckEntry; // m_curCraftCheckEntry
    public bool hasAutoPopulatingIngredients; // m_bHasAutoPopulatingIngredients

    public Recipe(AC2Reader data) {
        difficultyMinusSkillCutoff = data.ReadInt32();
        data.ReadHO<LevelMappingTable>(v => costMappingTable = v);
        data.ReadHO<ARHash>(v => craftMessageOverrides = v.to<uint, StringInfo>());
        data.ReadHO<LevelMappingTable>(v => m_difficultyMappingTable = v);
        iconDid = data.ReadDataId();
        data.ReadHO<ARHash>(v => craftCheckEntries = v.to<uint, CraftCheckEntry>());
        craftSkillCategory = data.ReadUInt32();
        data.ReadHO<RList>(v => ingredients = v.to<Ingredient>());
        data.ReadHO<StringInfo>(v => name = v);
        data.ReadHO<StringInfo>(v => description = v);
        maxLevel = data.ReadUInt32();
        craftSkillDid = data.ReadDataId();
        maxNumIngredients = data.ReadUInt32();
        minSpinnerVal = data.ReadUInt32();
        bonus = data.ReadSingle();
        charges = data.ReadInt32();
        data.ReadHO<RecipeNameColoringTable>(v => nameColoringTable = v);
        chargeRefreshPeriod = data.ReadInt32();
        numAnimCycles = data.ReadUInt32();
        data.ReadHO<AList>(v => craftThresholds = v);
        flags = data.ReadEnum<Flag>();
        maxSpinnerVal = data.ReadUInt32();
        minLevel = data.ReadUInt32();
        lastProductOrdinal = data.ReadUInt32();
        data.ReadHO<LevelMappingTable>(v => craftXpMappingTable = v);
        data.ReadHO<CraftRandomEntry>(v => curRandEntry = v);
        data.ReadHO<AAHash>(v => targetsHash = v);
        craftXp = data.ReadUInt32();
        requiresTarget = data.ReadBoolean();
        difficulty = data.ReadUInt32();
        data.ReadHO<SkillCheck>(v => skillCheckOverride = v);
        data.ReadHO<Effect>(v => requiredEffect = v);
        animation = data.ReadUInt32();
        recoveryTime = data.ReadSingle();
        cost = data.ReadInt32();
        data.ReadHO<CraftCheckEntry>(v => curCraftCheckEntry = v);
        hasAutoPopulatingIngredients = data.ReadBoolean();
    }
}
