using System;

namespace AC2RE.Definitions;

public class GenesisEffect : ParameterizedNumericEffect {

    public override PackageType packageType => PackageType.GenesisEffect;

    // WLib GenesisEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,

        IsPermanentSummonDuration = 1 << 5, // SetPermanentSummonDuration 0x00000020
        IsPlaceItemInternal = 1 << 6, // PlaceItemInternal 0x00000040
        IsPlaceItemExternal = 1 << 7, // PlaceItemExternal 0x00000080
        IsExclusive = 1 << 8, // SetExclusive 0x00000100
        IsExclusiveFakeSuccess = 1 << 9, // SetExclusiveFakeSuccess 0x00000200
        IsExternalNoCheckpoint = 1 << 10, // SetExternalNoCheckpoint 0x00000400
    }

    public Flag genesisFlags => (Flag)flags;

    public GenesisEffect(AC2Reader data) : base(data) {

    }
}
