using AC2RE.Definitions;
using AC2RE.Server.Database;
using System.Collections.Generic;
using System.Linq;

namespace AC2RE.Server {

    internal partial class WorldObject {

        private Dictionary<InvLoc, InstanceId>? equippedItemIds;

        [DatabaseIgnore]
        public IEnumerable<KeyValuePair<InvLoc, InstanceId>> equippedItemIdsEnumerable => equippedItemIds ?? Enumerable.Empty<KeyValuePair<InvLoc, InstanceId>>();

        private void initEquip() {

        }

        public bool isEquipped(InvLoc invLoc) {
            return equippedItemIds != null && equippedItemIds.ContainsKey(invLoc);
        }

        public bool isEquipped(InstanceId itemId) {
            return equippedItemIds != null && equippedItemIds.ContainsValue(itemId);
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

                if (equippedItemIds == null) {
                    equippedItemIds = new();
                }

                equippedItemIds[equipLoc] = item.id;

                item.wielderId = id;

                HoldingLocation holdLoc = item.primaryHoldLoc;
                if (holdLoc != HoldingLocation.INVALID) {
                    item.setParent(this, holdLoc, item.holdOrientation);
                }
            } else {
                WorldObject? curItem = objectManager.get(equippedItemIds[equipLoc]);
                if (curItem != null) {
                    if (!equippedItemIds.TryGetValue(equipLoc, out InstanceId equippedItemId) || curItem.id != equippedItemId) {
                        return ErrorType.NOTEQUIPPED;
                    } else if (!containedItemIds.Contains(curItem.id)) {
                        return ErrorType.CONTAINERDOESNOTCONTAINITEM;
                    }

                    equippedItemIds.Remove(equipLoc);

                    curItem.setParent(null);
                }
            }

            return ErrorType.NONE;
        }
    }
}
