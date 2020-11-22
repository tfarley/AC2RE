namespace AC2RE.Definitions {

    public class PlayerEffectRecipeAction : IPackage {

        public PackageType packageType => PackageType.PlayerEffectRecipeAction;

        public float spellcraft; // m_fSpellcraft
        public SingletonPkg<Effect> effect; // m_effect
        public uint flags; // m_flags
        public DataId mappingTableDid; // m_didMappingTable

        public PlayerEffectRecipeAction(AC2Reader data) {
            spellcraft = data.ReadSingle();
            data.ReadSingletonPkg<Effect>(v => effect = v);
            flags = data.ReadUInt32();
            mappingTableDid = data.ReadDataId();
        }
    }
}
