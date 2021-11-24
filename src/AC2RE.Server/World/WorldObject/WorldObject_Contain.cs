using AC2RE.Definitions;
using AC2RE.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AC2RE.Server {

    internal partial class WorldObject {

        private HashSet<InstanceId>? containedItemIds;
        private List<InstanceId>? contentsItemIds;

        public IEnumerable<InstanceId> containedItemIdsEnumerable => containedItemIds ?? Enumerable.Empty<InstanceId>();
        public IEnumerable<InstanceId> contentsItemIdsEnumerable => contentsItemIds ?? Enumerable.Empty<InstanceId>();

        public void initContain() {

        }

        public void recacheContain(IEnumerable<WorldObject> containedItems) {
            if (containedItemIds == null) {
                containedItemIds = new();
            }
            if (contentsItemIds == null) {
                contentsItemIds = new();
            }

            foreach (WorldObject containedItem in containedItems) {
                containedItemIds.Add(containedItem.id);
                contentsItemIds.Add(containedItem.id);
            }
        }

        public bool contains(InstanceId itemId) {
            return containedItemIds != null && containedItemIds.Contains(itemId);
        }

        public int setContainer(WorldObject? container, int slot = 0, Player? requester = null) {
            if (container != null) {
                if (container.containedItemIds == null) {
                    container.containedItemIds = new();
                }
                if (container.contentsItemIds == null) {
                    container.contentsItemIds = new();
                }

                slot = Math.Min(slot, container.capacity - 1);

                if (container.containedItemIds.Contains(id)) {
                    int curSlot = container.contentsItemIds.IndexOf(id);
                    if (curSlot != -1) {
                        // Moving within contents
                        if (curSlot < slot) {
                            // Removal will shift everything after the current slot down by 1 - adjust so that the item is placed to the left of the blocking item
                            slot--;
                        }
                        if (slot != curSlot) {
                            container.contentsItemIds.RemoveAt(curSlot);
                            return container.contentsItemIds.InsertSafe(slot, id);
                        }
                        return curSlot;
                    } else {
                        // Moving from equipped into contents
                        return container.contentsItemIds.InsertSafe(slot, id);
                    }
                }

                // Adding to container
                containerId = container.id;

                container.containedItemIds.Add(id);
                return container.contentsItemIds.InsertSafe(slot, id);
            } else if (containerId != InstanceId.NULL) {
                if (world.objectManager.tryGet(containerId, out WorldObject? curContainer)) {
                    curContainer.containedItemIds!.Remove(id);
                    curContainer.contentsItemIds!.Remove(id);

                    world.playerManager.sendAllVisibleExcept(id, requester, new ContainMsg {
                        childIdWithPosStamp = getInstanceIdWithStamp(++physics.timestamps[(int)PhysicsTimeStamp.POSITION]),
                    });
                }

                containerId = InstanceId.NULL;
            }

            return -1;
        }
    }
}
