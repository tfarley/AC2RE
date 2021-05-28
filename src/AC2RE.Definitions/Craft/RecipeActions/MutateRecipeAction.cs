using System;

namespace AC2RE.Definitions {

    public class MutateRecipeAction : IPackage {

        public PackageType packageType => PackageType.MutateRecipeAction;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            OBJECT_TO_MUTATE = 1 << 0, // 0x00000001, MutateRecipeAction::SetObjectToMutate
            OBJECT_TO_MUTATE_DYNAMIC_QUALITY = 1 << 1, // 0x00000002, MutateRecipeAction::SetObjectToMutateDynamicQuality
        }

        public uint ordinal; // m_uiOrdinal
        public IPackage treasureProfile; // m_prof
        public BiasProfile biasProfile; // m_biasProf
        public Flag flags; // m_flags
        public DataId mappingTableDid; // m_didMappingTable
        public uint minSpinnerVal; // m_uiMinSpinnerVal
        public uint maxSpinnerVal; // m_uiMaxSpinnerVal

        public MutateRecipeAction(AC2Reader data) {
            ordinal = data.ReadUInt32();
            // TODO: Unknown type "TreasureProfile" - perhaps newer native type?
            data.ReadPkg<IPackage>(v => treasureProfile = v);
            data.ReadPkg<BiasProfile>(v => biasProfile = v);
            flags = (Flag)data.ReadUInt32();
            mappingTableDid = data.ReadDataId();
            minSpinnerVal = data.ReadUInt32();
            maxSpinnerVal = data.ReadUInt32();
        }
    }
}
