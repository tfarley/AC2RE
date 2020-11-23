﻿using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class UsageDesc : IPackage {

        public PackageType packageType => PackageType.UsageDesc;

        public StringInfo successText; // m_siSuccessMessage
        public UsageBlob usageBlob; // m_usageBlob
        public InstanceId itemId; // m_itemID
        public Position userPos; // m_posUser
        public uint userWeenieType; // m_wtUser // TODO: WeenieType
        public InstanceId userId; // m_userID
        public float distToUsedItem; // m_fDistanceToUsedItem
        public InstanceId targetId; // m_targetID
        public ErrorType error; // m_status
        public InstanceId effectTargetId; // m_effTargetID
        public uint usageTargetTypeValid; // m_uttValid // TODO: UsageTargetType
        public List<SingletonPkg<Effect>> effectsToApply; // m_effsToApply
        public int vigorCost; // m_iVigorCost
        public uint controlFlags; // m_controlFlags
        public bool cancelsSF; // m_bCancelsSF
        public int healthCost; // m_iHealthCost

        public UsageDesc() {

        }

        public UsageDesc(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => successText = v);
            data.ReadPkg<UsageBlob>(v => usageBlob = v);
            itemId = data.ReadInstanceId();
            data.ReadPkg<Position>(v => userPos = v);
            userWeenieType = data.ReadUInt32();
            userId = data.ReadInstanceId();
            distToUsedItem = data.ReadSingle();
            targetId = data.ReadInstanceId();
            error = (ErrorType)data.ReadUInt32();
            effectTargetId = data.ReadInstanceId();
            usageTargetTypeValid = data.ReadUInt32();
            data.ReadPkg<RList>(v => effectsToApply = v.to(SingletonPkg<Effect>.cast));
            vigorCost = data.ReadInt32();
            controlFlags = data.ReadUInt32();
            cancelsSF = data.ReadBoolean();
            healthCost = data.ReadInt32();
        }

        public void write(AC2Writer data) {
            data.WritePkg(successText);
            data.WritePkg(usageBlob);
            data.Write(itemId);
            data.WritePkg(userPos);
            data.Write(userWeenieType);
            data.Write(userId);
            data.Write(distToUsedItem);
            data.Write(targetId);
            data.Write((uint)error);
            data.Write(effectTargetId);
            data.Write(usageTargetTypeValid);
            data.WritePkg(RList.from(effectsToApply));
            data.Write(vigorCost);
            data.Write(controlFlags);
            data.Write(cancelsSF);
            data.Write(healthCost);
        }
    }
}