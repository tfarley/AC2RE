namespace AC2E.Def {

    public class Recipe : IPackage {

        public virtual PackageType packageType => PackageType.Recipe;

        public int difficultyMinusSkillCutoff; // m_difficultyMinusSkillCutoff
        public SingletonPkg<LevelMappingTable> costMappingTable; // m_costMappingTable
        public ARHash<IPackage> craftMessageOverrides; // m_craftMessageOverrides
        public SingletonPkg<LevelMappingTable> m_difficultyMappingTable; // m_difficultyMappingTable
        public DataId iconDid; // m_iconDID
        public ARHash<IPackage> craftCheckEntries; // m_craftCheckEntries
        public uint craftSkillCategory; // m_craftSkillCategory
        public RList<IPackage> ingredients; // m_ingredients
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
        public AList craftThresholds; // m_craftThreshs
        public uint flags; // m_flags
        public uint maxSpinnerVal; // m_maxSpinnerVal
        public uint minLevel; // m_minLevel
        public uint lastProductOrdinal; // m_uiLastProductOrdinal
        public SingletonPkg<LevelMappingTable> craftXpMappingTable; // m_craftXPMappingTable
        public CraftRandomEntry curRandEntry; // m_curRandEntry
        public AAHash targetsHash; // m_hashTargets
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
            data.ReadSingletonPkg(v => costMappingTable = v.to<LevelMappingTable>());
            data.ReadPkg<ARHash<IPackage>>(v => craftMessageOverrides = v);
            data.ReadSingletonPkg(v => m_difficultyMappingTable = v.to<LevelMappingTable>());
            iconDid = data.ReadDataId();
            data.ReadPkg<ARHash<IPackage>>(v => craftCheckEntries = v);
            craftSkillCategory = data.ReadUInt32();
            data.ReadPkg<RList<IPackage>>(v => ingredients = v);
            data.ReadPkg<StringInfo>(v => name = v);
            data.ReadPkg<StringInfo>(v => description = v);
            maxLevel = data.ReadUInt32();
            craftSkillDid = data.ReadDataId();
            maxNumIngredients = data.ReadUInt32();
            minSpinnerVal = data.ReadUInt32();
            bonus = data.ReadSingle();
            charges = data.ReadInt32();
            data.ReadSingletonPkg(v => nameColoringTable = v.to<RecipeNameColoringTable>());
            chargeRefreshPeriod = data.ReadInt32();
            numAnimCycles = data.ReadUInt32();
            data.ReadPkg<AList>(v => craftThresholds = v);
            flags = data.ReadUInt32();
            maxSpinnerVal = data.ReadUInt32();
            minLevel = data.ReadUInt32();
            lastProductOrdinal = data.ReadUInt32();
            data.ReadSingletonPkg(v => craftXpMappingTable = v.to<LevelMappingTable>());
            data.ReadPkg<CraftRandomEntry>(v => curRandEntry = v);
            data.ReadPkg<AAHash>(v => targetsHash = v);
            craftXp = data.ReadUInt32();
            requiresTarget = data.ReadBoolean();
            difficulty = data.ReadUInt32();
            data.ReadSingletonPkg(v => skillCheckOverride = v.to<SkillCheck>());
            data.ReadSingletonPkg(v => requiredEffect = v.to<Effect>());
            animation = data.ReadUInt32();
            recoveryTime = data.ReadSingle();
            cost = data.ReadInt32();
            data.ReadPkg<CraftCheckEntry>(v => curCraftCheckEntry = v);
            hasAutoPopulatingIngredients = data.ReadBoolean();
        }
    }
}
