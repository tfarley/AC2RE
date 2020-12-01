using AC2RE.Definitions;
using AC2RE.Server.Database;
using AC2RE.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AC2RE.Server {

    internal partial class WorldObject {

        private List<InstanceId>? containedItemIds;

        [DatabaseIgnore]
        public IEnumerable<InstanceId> containedItemIdsEnumerable => containedItemIds ?? Enumerable.Empty<InstanceId>();

        public void initContain() {

        }

        public bool contains(InstanceId itemId) {
            return containedItemIds != null && containedItemIds.Contains(itemId);
        }

        public int setContainer(WorldObject? container, int slot = 0, Player? requester = null) {
            if (container != null) {
                if (container.containedItemIds == null) {
                    container.containedItemIds = new();
                }

                // TODO: Hack to prevent weird case where icon wraps to next line when dragged to an empty slot
                slot = Math.Clamp(slot, 0, Math.Max(container.containedItemIds.Count - (container.equippedItemIds?.Count ?? 0) - 2, 0));

                if (containerId != container.id) {
                    containerId = container.id;

                    return container.containedItemIds.InsertSafe(slot, id);
                } else {
                    int curSlot = container.containedItemIds.IndexOf(id);
                    if (slot != curSlot) {
                        // TODO: Reshuffle to correct index?
                        container.containedItemIds.RemoveAt(curSlot);
                        return container.containedItemIds.InsertSafe(slot, id);
                    }
                    return curSlot;
                }
            } else if (containerId != InstanceId.NULL) {
                WorldObject? curContainer = objectManager.get(containerId);
                if (curContainer != null) {
                    curContainer.containedItemIds!.Remove(id);

                    playerManager.sendAllVisibleExcept(id, requester, new ContainMsg {
                        childIdWithPosStamp = getInstanceIdWithStamp(++physics.timestamps[(int)PhysicsTimeStamp.POSITION]),
                    });
                }

                containerId = InstanceId.NULL;
            }

            return -1;
        }
    }
}
