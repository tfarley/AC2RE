using System;

namespace AC2RE.Definitions {

    public class ItemEffectRecipeAction : IPackage {

        public PackageType packageType => PackageType.ItemEffectRecipeAction;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            EFFECT = 1 << 0, // 0x00000001, ItemEffectRecipeAction::Set*EffectByName
            EFFECT_DYNAMIC_SPELLCRAFT = 1 << 1, // 0x00000002, ItemEffectRecipeAction::Set*EffectByNameDynamicSpellcraft
        }

        public uint ordinal; // m_uiOrdinal
        public float timeout; // m_ttTimeout
        public float spellcraft; // m_fSpellcraft
        public SingletonPkg<Effect> effect; // m_effect
        public int effectKind; // m_iEffectKind
        public Flag flags; // m_flags
        public DataId mappingTableDid; // m_didMappingTable
        public uint minSpinnerVal; // m_uiMinSpinnerVal
        public uint maxSpinnerVal; // m_uiMaxSpinnerVal

        public ItemEffectRecipeAction(AC2Reader data) {
            ordinal = data.ReadUInt32();
            timeout = data.ReadSingle();
            spellcraft = data.ReadSingle();
            data.ReadPkg<Effect>(v => effect = v);
            effectKind = data.ReadInt32();
            flags = (Flag)data.ReadUInt32();
            mappingTableDid = data.ReadDataId();
            minSpinnerVal = data.ReadUInt32();
            maxSpinnerVal = data.ReadUInt32();
        }
    }
}
