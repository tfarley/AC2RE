using System;

namespace AC2RE.Definitions {

    public class PlayerEffectRecipeAction : IPackage {

        public PackageType packageType => PackageType.PlayerEffectRecipeAction;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            EFFECT = 1 << 0, // 0x00000001, PlayerEffectRecipeAction::SetEffectByName
            EFFECT_DYNAMIC_SPELLCRAFT = 1 << 1, // 0x00000002, PlayerEffectRecipeAction::SetEffectByNameDynamicSpellcraft
        }

        public float spellcraft; // m_fSpellcraft
        public SingletonPkg<Effect> effect; // m_effect
        public Flag flags; // m_flags
        public DataId mappingTableDid; // m_didMappingTable

        public PlayerEffectRecipeAction(AC2Reader data) {
            spellcraft = data.ReadSingle();
            data.ReadPkg<Effect>(v => effect = v);
            flags = (Flag)data.ReadUInt32();
            mappingTableDid = data.ReadDataId();
        }
    }
}
