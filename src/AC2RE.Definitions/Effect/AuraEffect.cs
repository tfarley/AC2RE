using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class AuraEffect : Effect {

        public override PackageType packageType => PackageType.AuraEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            VALID_TARGET_PLAYER = 1 << 0, // 0x00000001, AuraEffect::ValidTargetPlayer
            VALID_TARGET_WEAPON = 1 << 1, // 0x00000002, AuraEffect::ValidTargetWeapon
            VALID_TARGET_SHIELD = 1 << 2, // 0x00000004, AuraEffect::ValidTargetShield
            VALID_TARGET_ITEM = 1 << 3, // 0x00000008, AuraEffect::ValidTargetItem
            VALID_TARGET_PET = 1 << 4, // 0x00000010, AuraEffect::ValidTargetPet
            VALID_TARGET_OTHER_PLAYER = 1 << 5, // 0x00000020, AuraEffect::ValidTargetOtherPlayer
            VALID_TARGET_MASTER = 1 << 6, // 0x00000040, AuraEffect::ValidTargetMaster
        }

        public int maxRangeSqr; // m_fMaxRangeSqr
        public List<SingletonPkg<Effect>> effects; // m_listEffect
        public Flag auraFlags; // m_uiAuraFlags

        public AuraEffect(AC2Reader data) : base(data) {
            maxRangeSqr = data.ReadInt32();
            data.ReadPkg<RList>(v => effects = v.to(SingletonPkg<Effect>.cast));
            auraFlags = (Flag)data.ReadUInt32();
        }
    }
}
