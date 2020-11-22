using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class InvMoveDesc : IPackage {

        public PackageType packageType => PackageType.InvMoveDesc;

        public bool merge; // mergeFlag
        public uint actualFromSlot; // actualFromSlot
        public InstanceId targetId; // targetID
        public bool itemIsContainer; // itemIsContainerFlag
        public bool ignoreAttunement; // bIgnoreAttunement
        public DataId itemAprDid; // m_itemAprID
        public bool noCheck; // noCheckFlag
        public InstanceId actualTargetContainerId; // actualTargetContainer
        public bool shouldUnlock; // bShouldUnlock
        public uint targetSlot; // targetSlot
        public uint moveType; // moveType
        public bool checkTakePerm; // checkTakePermFlag
        public bool hidden; // bHidden
        public uint targetWeenieType; // targetWeenieType
        public InstanceId fromId; // fromID
        public InstanceId splitItemAttunedId; // splitItemAttunedID
        public bool trade; // tradeFlag
        public bool noRollback; // noRollbackFlag
        public DataId splitItemEntityDid; // splitItemEntityDID
        public bool allItemUnitsTaken; // allItemUnitsTakenFlag
        public uint actualTargetSlot; // actualTargetSlot
        public uint quantityLeftToContain; // quantityLeftToContain
        public uint quantity; // quantity
        public bool generatorRequest; // generatorRequestFlag
        public InstanceId splitItemId; // splitItemID
        public Dictionary<uint, uint> itemAppearances; // m_itemAppKeyHash
        public bool noAnim; // noAnimFlag
        public InstanceId mergeContainerId; // mergeContainerID
        public bool usedOverflowSlot; // bUsedOverflowSlot
        public bool grabItem; // grabItem
        public InstanceId actualFromContainerId; // actualFromContainer
        public DataId itemVisualDescDid; // itemVDescID
        public uint fromSlot; // fromSlot
        public ErrorType error; // status
        public bool playedAnim; // playedAnim
        public bool quiet; // bQuiet
        public bool allowOverflowSlots; // bAllowOverflowSlots
        public InstanceId itemId; // itemID
        public bool noMove; // noMoveFlag
        public bool doContain; // doContainFlag
        public uint splitItemQuantity; // splitItemQty
        public bool autoMerge; // autoMergeFlag
        public uint mergeSlot; // mergeSlot

        public InvMoveDesc() {

        }

        public InvMoveDesc(AC2Reader data) {
            merge = data.ReadBoolean();
            actualFromSlot = data.ReadUInt32();
            targetId = data.ReadInstanceId();
            itemIsContainer = data.ReadBoolean();
            ignoreAttunement = data.ReadBoolean();
            itemAprDid = data.ReadDataId();
            noCheck = data.ReadBoolean();
            actualTargetContainerId = data.ReadInstanceId();
            shouldUnlock = data.ReadBoolean();
            targetSlot = data.ReadUInt32();
            moveType = data.ReadUInt32();
            checkTakePerm = data.ReadBoolean();
            hidden = data.ReadBoolean();
            targetWeenieType = data.ReadUInt32();
            fromId = data.ReadInstanceId();
            splitItemAttunedId = data.ReadInstanceId();
            trade = data.ReadBoolean();
            noRollback = data.ReadBoolean();
            splitItemEntityDid = data.ReadDataId();
            allItemUnitsTaken = data.ReadBoolean();
            actualTargetSlot = data.ReadUInt32();
            quantityLeftToContain = data.ReadUInt32();
            quantity = data.ReadUInt32();
            generatorRequest = data.ReadBoolean();
            splitItemId = data.ReadInstanceId();
            data.ReadPkg<AAHash>(v => itemAppearances = v);
            noAnim = data.ReadBoolean();
            mergeContainerId = data.ReadInstanceId();
            usedOverflowSlot = data.ReadBoolean();
            grabItem = data.ReadBoolean();
            actualFromContainerId = data.ReadInstanceId();
            itemVisualDescDid = data.ReadDataId();
            fromSlot = data.ReadUInt32();
            error = (ErrorType)data.ReadUInt32();
            playedAnim = data.ReadBoolean();
            quiet = data.ReadBoolean();
            allowOverflowSlots = data.ReadBoolean();
            itemId = data.ReadInstanceId();
            noMove = data.ReadBoolean();
            doContain = data.ReadBoolean();
            splitItemQuantity = data.ReadUInt32();
            autoMerge = data.ReadBoolean();
            mergeSlot = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(merge);
            data.Write(actualFromSlot);
            data.Write(targetId);
            data.Write(itemIsContainer);
            data.Write(ignoreAttunement);
            data.Write(itemAprDid);
            data.Write(noCheck);
            data.Write(actualTargetContainerId);
            data.Write(shouldUnlock);
            data.Write(targetSlot);
            data.Write(moveType);
            data.Write(checkTakePerm);
            data.Write(hidden);
            data.Write(targetWeenieType);
            data.Write(fromId);
            data.Write(splitItemAttunedId);
            data.Write(trade);
            data.Write(noRollback);
            data.Write(splitItemEntityDid);
            data.Write(allItemUnitsTaken);
            data.Write(actualTargetSlot);
            data.Write(quantityLeftToContain);
            data.Write(quantity);
            data.Write(generatorRequest);
            data.Write(splitItemId);
            data.WritePkg(AAHash.from(itemAppearances));
            data.Write(noAnim);
            data.Write(mergeContainerId);
            data.Write(usedOverflowSlot);
            data.Write(grabItem);
            data.Write(actualFromContainerId);
            data.Write(itemVisualDescDid);
            data.Write(fromSlot);
            data.Write((uint)error);
            data.Write(playedAnim);
            data.Write(quiet);
            data.Write(allowOverflowSlots);
            data.Write(itemId);
            data.Write(noMove);
            data.Write(doContain);
            data.Write(splitItemQuantity);
            data.Write(autoMerge);
            data.Write(mergeSlot);
        }
    }
}
