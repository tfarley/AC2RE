using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class UsageDesc : IHeapObject {

    public PackageType packageType => PackageType.UsageDesc;

    // WLib UsageDesc
    [Flags]
    public enum ControlFlag : uint {
        None = 0,
        ShouldDeleteItemOnUse = 1 << 0, // ShouldDeleteItemOnUse 0x00000001
        DeleteItemOverride = 1 << 1, // DeleteItemOverride 0x00000002
        ShouldLockUser = 1 << 2, // ShouldLockUser 0x00000004
        ShouldLockItem = 1 << 3, // ShouldLockItem 0x00000008
        ShouldLockTarget = 1 << 4, // ShouldLockTarget 0x00000010
        ShouldCheckPrereqs = 1 << 5, // ShouldCheckPrereqs 0x00000020
        IsQuiet = 1 << 6, // IsQuiet 0x00000040
        ShouldSupressSuccessMessage = 1 << 7, // ShouldSupressSuccessMessage 0x00000080
        ShouldCheckPermissionsWithinPermRadius = 1 << 8, // ShouldCheckPermissionsWithinPermRadius 0x00000100
        ShouldInterruptMovement = 1 << 9, // ShouldInterruptMovement 0x00000200
        ShouldDelegateUsage = 1 << 10, // ShouldDelegateUsage 0x00000400
        IsLockOverride = 1 << 11, // IsLockOverride 0x00000800

        ShouldUnlockUserForUsageEffects = 1 << 13, // ShouldUnlockUserForUsageEffects 0x00002000
    }

    public StringInfo successText; // m_siSuccessMessage
    public UsageBlob usageBlob; // m_usageBlob
    public InstanceId itemId; // m_itemID
    public Position userPos; // m_posUser
    public WeenieType userWeenieType; // m_wtUser
    public InstanceId userId; // m_userID
    public float distToUsedItem; // m_fDistanceToUsedItem
    public InstanceId targetId; // m_targetID
    public ErrorType error; // m_status
    public InstanceId effectTargetId; // m_effTargetID
    public uint usageTargetTypeValid; // m_uttValid // TODO: UsageTargetType
    public List<SingletonPkg<Effect>> effectsToApply; // m_effsToApply
    public int vigorCost; // m_iVigorCost
    public ControlFlag controlFlags; // m_controlFlags
    public bool cancelsSF; // m_bCancelsSF
    public int healthCost; // m_iHealthCost

    public UsageDesc() {

    }

    public UsageDesc(AC2Reader data) {
        data.ReadHO<StringInfo>(v => successText = v);
        data.ReadHO<UsageBlob>(v => usageBlob = v);
        itemId = data.ReadInstanceId();
        data.ReadHO<Position>(v => userPos = v);
        userWeenieType = data.ReadEnum<WeenieType>();
        userId = data.ReadInstanceId();
        distToUsedItem = data.ReadSingle();
        targetId = data.ReadInstanceId();
        error = data.ReadEnum<ErrorType>();
        effectTargetId = data.ReadInstanceId();
        usageTargetTypeValid = data.ReadUInt32();
        data.ReadHO<RList>(v => effectsToApply = v.to(SingletonPkg<Effect>.cast));
        vigorCost = data.ReadInt32();
        controlFlags = data.ReadEnum<ControlFlag>();
        cancelsSF = data.ReadBoolean();
        healthCost = data.ReadInt32();
    }

    public void write(AC2Writer data) {
        data.WriteHO(successText);
        data.WriteHO(usageBlob);
        data.Write(itemId);
        data.WriteHO(userPos);
        data.WriteEnum(userWeenieType);
        data.Write(userId);
        data.Write(distToUsedItem);
        data.Write(targetId);
        data.WriteEnum(error);
        data.Write(effectTargetId);
        data.Write(usageTargetTypeValid);
        data.WriteHO(RList.from(effectsToApply));
        data.Write(vigorCost);
        data.WriteEnum(controlFlags);
        data.Write(cancelsSF);
        data.Write(healthCost);
    }
}
