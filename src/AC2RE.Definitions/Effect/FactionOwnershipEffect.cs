using System;

namespace AC2RE.Definitions;

public class FactionOwnershipEffect : Effect {

    public override PackageType packageType => PackageType.FactionOwnershipEffect;

    // WLib FactionOwnershipEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,
        FromCaster = 1 << 0, // FromCaster 0x00000001
        FromTarget = 1 << 1, // FromTarget 0x00000002
        HasFaction = 1 << 2, // SetFaction 0x00000004
    }

    public Flag factionOwnershipFlags => (Flag)flags;

    public FactionOwnershipEffect(AC2Reader data) : base(data) {

    }
}
