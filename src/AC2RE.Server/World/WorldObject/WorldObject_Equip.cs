using AC2RE.Definitions;
using AC2RE.Server.Database;
using System.Collections.Generic;
using System.Linq;

namespace AC2RE.Server {

    internal partial class WorldObject {

        [DbPersist]
        private Dictionary<InvLoc, InstanceId>? invLocToEquippedItemId;

        public IEnumerable<KeyValuePair<InvLoc, InstanceId>> invLocToEquippedItemIdEnumerable => invLocToEquippedItemId ?? Enumerable.Empty<KeyValuePair<InvLoc, InstanceId>>();
        public IEnumerable<InstanceId> equippedItemIdsEnumerable => invLocToEquippedItemId?.Values ?? Enumerable.Empty<InstanceId>();

        private void initEquip() {

        }

        public bool isEquipped(InvLoc invLoc) {
            return invLocToEquippedItemId != null && invLocToEquippedItemId.ContainsKey(invLoc);
        }

        public bool isEquipped(InstanceId itemId) {
            return invLocToEquippedItemId != null && invLocToEquippedItemId.ContainsValue(itemId);
        }

        public ErrorType equip(InvLoc equipLoc, WorldObject? item) {
            if (item != null) {
                if (isEquipped(equipLoc)) {
                    return ErrorType.INVSLOTFULL;
                } else if (!item.validInvLocs.HasFlag(equipLoc)) {
                    return ErrorType.WRONGINVSLOT;
                } else if (containedItemIds == null || !containedItemIds.Contains(item.id)) {
                    return ErrorType.CONTAINERDOESNOTCONTAINITEM;
                }

                if (invLocToEquippedItemId == null) {
                    invLocToEquippedItemId = new();
                }

                invLocToEquippedItemId[equipLoc] = item.id;

                item.wielderId = id;

                HoldingLocation holdLoc = item.primaryHoldLoc;
                if (holdLoc != HoldingLocation.INVALID) {
                    item.setParent(this, holdLoc, item.holdOrientation);
                }
            } else {
                if (invLocToEquippedItemId != null && world.objectManager.tryGet(invLocToEquippedItemId[equipLoc], out WorldObject? curItem)) {
                    if (!invLocToEquippedItemId.TryGetValue(equipLoc, out InstanceId equippedItemId) || curItem.id != equippedItemId) {
                        return ErrorType.NOTEQUIPPED;
                    } else if (containedItemIds == null || !containedItemIds.Contains(curItem.id)) {
                        return ErrorType.CONTAINERDOESNOTCONTAINITEM;
                    }

                    invLocToEquippedItemId.Remove(equipLoc);

                    curItem.setParent(null);
                }
            }

            return ErrorType.NONE;
        }
    }
}
