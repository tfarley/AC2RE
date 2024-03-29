﻿namespace AC2RE.Definitions;

public class AttackHookData : HookData {

    public override PackageType packageType => PackageType.AttackHookData;

    public uint attackType; // mAttackType

    public AttackHookData(AC2Reader data) : base(data) {
        attackType = data.ReadUInt32();
    }
}
