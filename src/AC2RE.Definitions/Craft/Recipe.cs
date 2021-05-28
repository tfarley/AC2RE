using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class Recipe : IPackage {

        public virtual PackageType packageType => PackageType.Recipe;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            FIXED_COST = 1 << 0, // 0x00000001, Recipe::IsFixedCost
            DYNAMIC_COST = 1 << 1, // 0x00000002, Recipe::IsDynamicCost

            HIDDEN_WHEN_ZERO_CHARGES = 1 << 3, // 0x00000008, Recipe::IsHiddenWhenZeroCharges
            MIN_LEVEL_RESTRICTION = 1 << 4, // 0x00000010, Recipe::InqMinLevelRestriction
            MAX_LEVEL_RESTRICTION = 1 << 5, // 0x00000020, Recipe::InqMaxLevelRestriction
            CHARGE_REFRESHING = 1 << 6, // 0x00000040, Recipe::IsChargeRefreshing
            USES_SPINNER = 1 << 7, // 0x00000080, Recipe::UsesSpinner
            FIXED_DIFFICULTY = 1 << 8, // 0x00000100, Recipe::IsFixedDifficulty
            DYNAMIC_DIFFICULTY = 1 << 9, // 0x00000200, Recipe::IsDynamicDifficulty
            DIFFICULTY_MINUS_SKILL_CUTOFF = 1 << 10, // 0x00000400, Recipe::HasDifficultyMinusSkillCutoff
            FIXED_CRAFT_XP = 1 << 11, // 0x00000800, Recipe::IsFixedCraftXP
            DYNAMIC_CRAFT_XP = 1 << 12, // 0x00001000, Recipe::IsDynamicCraftXP
            REQUIRED_EFFECT = 1 << 13, // 0x00002000, Recipe::InqRequiredEffect
            READ_ONLY_SPINNER = 1 << 14, // 0x00004000, Recipe::HasReadOnlySpinner
            SKILL_CHECK_OVERRIDE = 1 << 15, // 0x00008000, Recipe::HasSkillCheckOverride
            NAME_COLORING_TABLE = 1 << 16, // 0x00010000, Recipe::HasNameColoringTable
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
            data.ReadPkg<LevelMappingTable>(v => costMappingTable = v);
            data.ReadPkg<ARHash>(v => craftMessageOverrides = v.to<uint, StringInfo>());
            data.ReadPkg<LevelMappingTable>(v => m_difficultyMappingTable = v);
            iconDid = data.ReadDataId();
            data.ReadPkg<ARHash>(v => craftCheckEntries = v.to<uint, CraftCheckEntry>());
            craftSkillCategory = data.ReadUInt32();
            data.ReadPkg<RList>(v => ingredients = v.to<Ingredient>());
            data.ReadPkg<StringInfo>(v => name = v);
            data.ReadPkg<StringInfo>(v => description = v);
            maxLevel = data.ReadUInt32();
            craftSkillDid = data.ReadDataId();
            maxNumIngredients = data.ReadUInt32();
            minSpinnerVal = data.ReadUInt32();
            bonus = data.ReadSingle();
            charges = data.ReadInt32();
            data.ReadPkg<RecipeNameColoringTable>(v => nameColoringTable = v);
            chargeRefreshPeriod = data.ReadInt32();
            numAnimCycles = data.ReadUInt32();
            data.ReadPkg<AList>(v => craftThresholds = v);
            flags = (Flag)data.ReadUInt32();
            maxSpinnerVal = data.ReadUInt32();
            minLevel = data.ReadUInt32();
            lastProductOrdinal = data.ReadUInt32();
            data.ReadPkg<LevelMappingTable>(v => craftXpMappingTable = v);
            data.ReadPkg<CraftRandomEntry>(v => curRandEntry = v);
            data.ReadPkg<AAHash>(v => targetsHash = v);
            craftXp = data.ReadUInt32();
            requiresTarget = data.ReadBoolean();
            difficulty = data.ReadUInt32();
            data.ReadPkg<SkillCheck>(v => skillCheckOverride = v);
            data.ReadPkg<Effect>(v => requiredEffect = v);
            animation = data.ReadUInt32();
            recoveryTime = data.ReadSingle();
            cost = data.ReadInt32();
            data.ReadPkg<CraftCheckEntry>(v => curCraftCheckEntry = v);
            hasAutoPopulatingIngredients = data.ReadBoolean();
        }
    }
}
