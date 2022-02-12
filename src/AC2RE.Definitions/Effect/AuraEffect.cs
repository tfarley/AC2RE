using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class AuraEffect : Effect {

    public override PackageType packageType => PackageType.AuraEffect;

    // WLib AuraEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,
        ValidTargetPlayer = 1 << 0, // ValidTargetPlayer 0x00000001
        ValidTargetWeapon = 1 << 1, // ValidTargetWeapon 0x00000002
        ValidTargetShield = 1 << 2, // ValidTargetShield 0x00000004
        ValidTargetItem = 1 << 3, // ValidTargetItem 0x00000008
        ValidTargetPet = 1 << 4, // ValidTargetPet 0x00000010
        ValidTargetOtherPlayer = 1 << 5, // ValidTargetOtherPlayer 0x00000020
        ValidTargetMaster = 1 << 6, // ValidTargetMaster 0x00000040
    }

    public int maxRangeSqr; // m_fMaxRangeSqr
    public List<SingletonPkg<Effect>> effects; // m_listEffect
    public Flag auraFlags; // m_uiAuraFlags

    public AuraEffect(AC2Reader data) : base(data) {
        maxRangeSqr = data.ReadInt32();
        data.ReadHO<RList>(v => effects = v.to(SingletonPkg<Effect>.cast));
        auraFlags = data.ReadEnum<Flag>();
    }
}
