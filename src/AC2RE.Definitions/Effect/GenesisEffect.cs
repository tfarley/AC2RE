using System;

namespace AC2RE.Definitions {

    public class GenesisEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.GenesisEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            PERMANENT_SUMMON_DURATION = 1 << 5, // 0x00000020, GenesisEffect::SetPermanentSummonDuration
            PLACE_ITEM_INTERNAL = 1 << 6, // 0x00000040, GenesisEffect::PlaceItemInternal
            PLACE_ITEM_EXTERNAL = 1 << 7, // 0x00000080, GenesisEffect::PlaceItemExternal
            EXCLUSIVE = 1 << 8, // 0x00000100, GenesisEffect::SetExclusive
            EXCLUSIVE_FAKE_SUCCESS = 1 << 9, // 0x00000200, GenesisEffect::SetExclusiveFakeSuccess
            EXTERNAL_NO_CHECKPOINT = 1 << 10, // 0x00000400, GenesisEffect::SetExternalNoCheckpoint
        }

        public Flag genesisFlags => (Flag)flags;

        public GenesisEffect(AC2Reader data) : base(data) {

        }
    }
}
