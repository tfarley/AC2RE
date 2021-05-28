using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class UsageDesc : IPackage {

        public PackageType packageType => PackageType.UsageDesc;

        // WLib
        [Flags]
        public enum ControlFlag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            DELETE_ITEM_ON_USE = 1 << 0, // 0x00000001, UsageDesc::ShouldDeleteItemOnUse
            DELETE_ITEM_OVERRIDE = 1 << 1, // 0x00000002, UsageDesc::DeleteItemOverride
            LOCK_USER = 1 << 2, // 0x00000004, UsageDesc::ShouldLockUser
            LOCK_ITEM = 1 << 3, // 0x00000008, UsageDesc::ShouldLockItem
            LOCK_TARGET = 1 << 4, // 0x00000010, UsageDesc::ShouldLockTarget
            CHECK_PREREQS = 1 << 5, // 0x00000020, UsageDesc::ShouldCheckPrereqs
            QUIET = 1 << 6, // 0x00000040, UsageDesc::IsQuiet
            SUPRESS_SUCCESS_MESSAGE = 1 << 7, // 0x00000080, UsageDesc::ShouldSupressSuccessMessage
            CHECK_PERMISSIONS_WITHIN_PERM_RADIUS = 1 << 8, // 0x00000100, UsageDesc::ShouldCheckPermissionsWithinPermRadius
            INTERRUPT_MOVEMENT = 1 << 9, // 0x00000200, UsageDesc::ShouldInterruptMovement
            DELEGATE_USAGE = 1 << 10, // 0x00000400, UsageDesc::ShouldDelegateUsage
            LOCK_OVERRIDE = 1 << 11, // 0x00000800, UsageDesc::IsLockOverride

            UNLOCK_USER_FOR_USAGE_EFFECTS = 1 << 13, // 0x00002000, UsageDesc::ShouldUnlockUserForUsageEffects
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
            data.ReadPkg<StringInfo>(v => successText = v);
            data.ReadPkg<UsageBlob>(v => usageBlob = v);
            itemId = data.ReadInstanceId();
            data.ReadPkg<Position>(v => userPos = v);
            userWeenieType = (WeenieType)data.ReadUInt32();
            userId = data.ReadInstanceId();
            distToUsedItem = data.ReadSingle();
            targetId = data.ReadInstanceId();
            error = (ErrorType)data.ReadUInt32();
            effectTargetId = data.ReadInstanceId();
            usageTargetTypeValid = data.ReadUInt32();
            data.ReadPkg<RList>(v => effectsToApply = v.to(SingletonPkg<Effect>.cast));
            vigorCost = data.ReadInt32();
            controlFlags = (ControlFlag)data.ReadUInt32();
            cancelsSF = data.ReadBoolean();
            healthCost = data.ReadInt32();
        }

        public void write(AC2Writer data) {
            data.WritePkg(successText);
            data.WritePkg(usageBlob);
            data.Write(itemId);
            data.WritePkg(userPos);
            data.Write((uint)userWeenieType);
            data.Write(userId);
            data.Write(distToUsedItem);
            data.Write(targetId);
            data.Write((uint)error);
            data.Write(effectTargetId);
            data.Write(usageTargetTypeValid);
            data.WritePkg(RList.from(effectsToApply));
            data.Write(vigorCost);
            data.Write((uint)controlFlags);
            data.Write(cancelsSF);
            data.Write(healthCost);
        }
    }
}
