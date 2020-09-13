namespace AC2E.Def {

    public class PlayerEffectRecipeAction : RecipeAction {

        public override PackageType packageType => PackageType.PlayerEffectRecipeAction;

        public float spellcraft; // m_fSpellcraft
        public SingletonPkg<Effect> effect; // m_effect
        public uint flags; // m_flags
        public DataId mappingTableDid; // m_didMappingTable

        public PlayerEffectRecipeAction(AC2Reader data) : base(data) {
            spellcraft = data.ReadSingle();
            data.ReadSingletonPkg(v => effect = v.to<Effect>());
            flags = data.ReadUInt32();
            mappingTableDid = data.ReadDataId();
        }
    }
}
