using AC2RE.Definitions;
using System.Collections.Generic;
using System.Linq;

namespace AC2RE.Server {

    internal partial class WorldObject {

        private Dictionary<InvLoc, InstanceId>? invLocToEquippedItemId;

        public IEnumerable<KeyValuePair<InvLoc, InstanceId>> invLocToEquippedItemIdEnumerable => invLocToEquippedItemId ?? Enumerable.Empty<KeyValuePair<InvLoc, InstanceId>>();
        public IEnumerable<InstanceId> equippedItemIdsEnumerable => invLocToEquippedItemId?.Values ?? Enumerable.Empty<InstanceId>();

        private void initEquip() {

        }

        public void recacheEquip(IEnumerable<WorldObject> equippedItems) {
            if (invLocToEquippedItemId == null) {
                invLocToEquippedItemId = new();
            }

            foreach (WorldObject equippedItem in equippedItems) {
                invLocToEquippedItemId[equippedItem.equippedLocation] = equippedItem.id;
                contentsItemIds!.Remove(equippedItem.id);
            }
        }

        public InstanceId getEquipped(InvLoc invLoc) {
            return invLocToEquippedItemId?.GetValueOrDefault(invLoc, InstanceId.NULL) ?? InstanceId.NULL;
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
                } else if (!contains(item.id)) {
                    return ErrorType.USAGE_INTERACTIONTARGETNOTCONTAINED;
                } else if (!item.validInvLocs.HasFlag(equipLoc)) {
                    return ErrorType.WRONGINVSLOT;
                } else if (containedItemIds == null || !containedItemIds.Contains(item.id)) {
                    return ErrorType.CONTAINERDOESNOTCONTAINITEM;
                }

                if (invLocToEquippedItemId == null) {
                    invLocToEquippedItemId = new();
                }

                invLocToEquippedItemId[equipLoc] = item.id;

                item.equipperId = id;
                item.equippedLocation = equipLoc;

                HoldingLocation holdLoc = item.primaryHoldLoc;
                if (holdLoc != HoldingLocation.INVALID) {
                    item.setParent(this, holdLoc, item.holdOrientation);
                }

                contentsItemIds!.Remove(item.id);
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

            syncMode();

            return ErrorType.NONE;
        }
    }
}
