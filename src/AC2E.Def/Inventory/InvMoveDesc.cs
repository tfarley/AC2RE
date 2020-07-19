using System.IO;

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

        public InvMoveDesc(BinaryReader data, PackageRegistry registry) {
            mergeFlag = data.ReadUInt32() != 0;
            actualFromSlot = data.ReadUInt32();
            targetID = data.ReadInstanceId();
            itemIsContainerFlag = data.ReadUInt32() != 0;
            bIgnoreAttunement = data.ReadUInt32() != 0;
            m_itemAprID = data.ReadDataId();
            noCheckFlag = data.ReadUInt32() != 0;
            actualTargetContainer = data.ReadInstanceId();
            bShouldUnlock = data.ReadUInt32() != 0;
            targetSlot = data.ReadUInt32();
            moveType = data.ReadUInt32();
            checkTakePermFlag = data.ReadUInt32() != 0;
            bHidden = data.ReadUInt32() != 0;
            targetWeenieType = data.ReadUInt32();
            fromID = data.ReadInstanceId();
            splitItemAttunedID = data.ReadInstanceId();
            tradeFlag = data.ReadUInt32() != 0;
            noRollbackFlag = data.ReadUInt32() != 0;
            splitItemEntityDID = data.ReadDataId();
            allItemUnitsTakenFlag = data.ReadUInt32() != 0;
            actualTargetSlot = data.ReadUInt32();
            quantityLeftToContain = data.ReadUInt32();
            quantity = data.ReadUInt32();
            generatorRequestFlag = data.ReadUInt32() != 0;
            splitItemID = data.ReadInstanceId();
            data.ReadPkgRef<AAHash>(v => m_itemAppKeyHash = v, registry);
            noAnimFlag = data.ReadUInt32() != 0;
            mergeContainerID = data.ReadInstanceId();
            bUsedOverflowSlot = data.ReadUInt32() != 0;
            grabItem = data.ReadUInt32() != 0;
            actualFromContainer = data.ReadInstanceId();
            itemVDescID = data.ReadDataId();
            fromSlot = data.ReadUInt32();
            status = data.ReadUInt32();
            playedAnim = data.ReadUInt32() != 0;
            bQuiet = data.ReadUInt32() != 0;
            bAllowOverflowSlots = data.ReadUInt32() != 0;
            itemID = data.ReadInstanceId();
            noMoveFlag = data.ReadUInt32() != 0;
            doContainFlag = data.ReadUInt32() != 0;
            splitItemQty = data.ReadUInt32();
            autoMergeFlag = data.ReadUInt32() != 0;
            mergeSlot = data.ReadUInt32();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(mergeFlag ? (uint)1 : (uint)0);
            data.Write(actualFromSlot);
            data.Write(targetID);
            data.Write(itemIsContainerFlag ? (uint)1 : (uint)0);
            data.Write(bIgnoreAttunement ? (uint)1 : (uint)0);
            data.Write(m_itemAprID);
            data.Write(noCheckFlag ? (uint)1 : (uint)0);
            data.Write(actualTargetContainer);
            data.Write(bShouldUnlock ? (uint)1 : (uint)0);
            data.Write(targetSlot);
            data.Write(moveType);
            data.Write(checkTakePermFlag ? (uint)1 : (uint)0);
            data.Write(bHidden ? (uint)1 : (uint)0);
            data.Write(targetWeenieType);
            data.Write(fromID);
            data.Write(splitItemAttunedID);
            data.Write(tradeFlag ? (uint)1 : (uint)0);
            data.Write(noRollbackFlag ? (uint)1 : (uint)0);
            data.Write(splitItemEntityDID);
            data.Write(allItemUnitsTakenFlag ? (uint)1 : (uint)0);
            data.Write(actualTargetSlot);
            data.Write(quantityLeftToContain);
            data.Write(quantity);
            data.Write(generatorRequestFlag ? (uint)1 : (uint)0);
            data.Write(splitItemID);
            data.Write(m_itemAppKeyHash, registry);
            data.Write(noAnimFlag ? (uint)1 : (uint)0);
            data.Write(mergeContainerID);
            data.Write(bUsedOverflowSlot ? (uint)1 : (uint)0);
            data.Write(grabItem ? (uint)1 : (uint)0);
            data.Write(actualFromContainer);
            data.Write(itemVDescID);
            data.Write(fromSlot);
            data.Write(status);
            data.Write(playedAnim ? (uint)1 : (uint)0);
            data.Write(bQuiet ? (uint)1 : (uint)0);
            data.Write(bAllowOverflowSlots ? (uint)1 : (uint)0);
            data.Write(itemID);
            data.Write(noMoveFlag ? (uint)1 : (uint)0);
            data.Write(doContainFlag ? (uint)1 : (uint)0);
            data.Write(splitItemQty);
            data.Write(autoMergeFlag ? (uint)1 : (uint)0);
            data.Write(mergeSlot);
        }
    }
}
