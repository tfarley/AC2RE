namespace AC2E.Def {

    public class InvMoveDesc : IPackage {

        public PackageType packageType => PackageType.InvMoveDesc;

        public bool mergeFlag;
        public uint actualFromSlot;
        public InstanceId targetID;
        public bool itemIsContainerFlag;
        public bool bIgnoreAttunement;
        public DataId m_itemAprID;
        public bool noCheckFlag;
        public InstanceId actualTargetContainer;
        public bool bShouldUnlock;
        public uint targetSlot;
        public uint moveType;
        public bool checkTakePermFlag;
        public bool bHidden;
        public uint targetWeenieType;
        public InstanceId fromID;
        public InstanceId splitItemAttunedID;
        public bool tradeFlag;
        public bool noRollbackFlag;
        public DataId splitItemEntityDID;
        public bool allItemUnitsTakenFlag;
        public uint actualTargetSlot;
        public uint quantityLeftToContain;
        public uint quantity;
        public bool generatorRequestFlag;
        public InstanceId splitItemID;
        public AAHash m_itemAppKeyHash;
        public bool noAnimFlag;
        public InstanceId mergeContainerID;
        public bool bUsedOverflowSlot;
        public bool grabItem;
        public InstanceId actualFromContainer;
        public DataId itemVDescID;
        public uint fromSlot;
        public uint status;
        public bool playedAnim;
        public bool bQuiet;
        public bool bAllowOverflowSlots;
        public InstanceId itemID;
        public bool noMoveFlag;
        public bool doContainFlag;
        public uint splitItemQty;
        public bool autoMergeFlag;
        public uint mergeSlot;

        public InvMoveDesc() {

        }

        public InvMoveDesc(AC2Reader data) {
            mergeFlag = data.ReadBoolean();
            actualFromSlot = data.ReadUInt32();
            targetID = data.ReadInstanceId();
            itemIsContainerFlag = data.ReadBoolean();
            bIgnoreAttunement = data.ReadBoolean();
            m_itemAprID = data.ReadDataId();
            noCheckFlag = data.ReadBoolean();
            actualTargetContainer = data.ReadInstanceId();
            bShouldUnlock = data.ReadBoolean();
            targetSlot = data.ReadUInt32();
            moveType = data.ReadUInt32();
            checkTakePermFlag = data.ReadBoolean();
            bHidden = data.ReadBoolean();
            targetWeenieType = data.ReadUInt32();
            fromID = data.ReadInstanceId();
            splitItemAttunedID = data.ReadInstanceId();
            tradeFlag = data.ReadBoolean();
            noRollbackFlag = data.ReadBoolean();
            splitItemEntityDID = data.ReadDataId();
            allItemUnitsTakenFlag = data.ReadBoolean();
            actualTargetSlot = data.ReadUInt32();
            quantityLeftToContain = data.ReadUInt32();
            quantity = data.ReadUInt32();
            generatorRequestFlag = data.ReadBoolean();
            splitItemID = data.ReadInstanceId();
            data.ReadPkg<AAHash>(v => m_itemAppKeyHash = v);
            noAnimFlag = data.ReadBoolean();
            mergeContainerID = data.ReadInstanceId();
            bUsedOverflowSlot = data.ReadBoolean();
            grabItem = data.ReadBoolean();
            actualFromContainer = data.ReadInstanceId();
            itemVDescID = data.ReadDataId();
            fromSlot = data.ReadUInt32();
            status = data.ReadUInt32();
            playedAnim = data.ReadBoolean();
            bQuiet = data.ReadBoolean();
            bAllowOverflowSlots = data.ReadBoolean();
            itemID = data.ReadInstanceId();
            noMoveFlag = data.ReadBoolean();
            doContainFlag = data.ReadBoolean();
            splitItemQty = data.ReadUInt32();
            autoMergeFlag = data.ReadBoolean();
            mergeSlot = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(mergeFlag);
            data.Write(actualFromSlot);
            data.Write(targetID);
            data.Write(itemIsContainerFlag);
            data.Write(bIgnoreAttunement);
            data.Write(m_itemAprID);
            data.Write(noCheckFlag);
            data.Write(actualTargetContainer);
            data.Write(bShouldUnlock);
            data.Write(targetSlot);
            data.Write(moveType);
            data.Write(checkTakePermFlag);
            data.Write(bHidden);
            data.Write(targetWeenieType);
            data.Write(fromID);
            data.Write(splitItemAttunedID);
            data.Write(tradeFlag);
            data.Write(noRollbackFlag);
            data.Write(splitItemEntityDID);
            data.Write(allItemUnitsTakenFlag);
            data.Write(actualTargetSlot);
            data.Write(quantityLeftToContain);
            data.Write(quantity);
            data.Write(generatorRequestFlag);
            data.Write(splitItemID);
            data.WritePkg(m_itemAppKeyHash);
            data.Write(noAnimFlag);
            data.Write(mergeContainerID);
            data.Write(bUsedOverflowSlot);
            data.Write(grabItem);
            data.Write(actualFromContainer);
            data.Write(itemVDescID);
            data.Write(fromSlot);
            data.Write(status);
            data.Write(playedAnim);
            data.Write(bQuiet);
            data.Write(bAllowOverflowSlots);
            data.Write(itemID);
            data.Write(noMoveFlag);
            data.Write(doContainFlag);
            data.Write(splitItemQty);
            data.Write(autoMergeFlag);
            data.Write(mergeSlot);
        }
    }
}
