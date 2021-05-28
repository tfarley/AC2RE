using System;

namespace AC2RE.Definitions {

    public class FactionOwnershipEffect : Effect {

        public override PackageType packageType => PackageType.FactionOwnershipEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            FROM_CASTER = 1 << 0, // 0x00000001, FactionOwnershipEffect::FromCaster
            FROM_TARGET = 1 << 1, // 0x00000002, FactionOwnershipEffect::FromTarget
            FACTION = 1 << 2, // 0x00000004, FactionOwnershipEffect::SetFaction
        }

        public Flag factionOwnershipFlags => (Flag)flags;

        public FactionOwnershipEffect(AC2Reader data) : base(data) {

        }
    }
}
