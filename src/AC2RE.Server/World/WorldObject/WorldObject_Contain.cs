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

        public int setContainer(WorldObject? container, int targetSlot = -1, Player? requester = null) {
            if (container != null) {
                if (container.containedItemIds == null) {
                    container.containedItemIds = new();
                }
                if (container.contentsItemIds == null) {
                    container.contentsItemIds = new();
                }

                int safeTargetSlot = Math.Min(targetSlot, container.capacity - 1);

                if (targetSlot == -1) {
                    targetSlot = container.contentsItemIds.Count - 1;
                }

                if (containerId != InstanceId.NULL && containerId != container.id) {
                    setContainer(null);
                }

                int curSlot = container.contentsItemIds.IndexOf(id);
                if (curSlot != -1) {
                    if (targetSlot == -2) {
                        // Moving out of contents, but still contained (e.g. autoEquip)
                        container.contentsItemIds.RemoveAt(curSlot);
                    } else {
                        // Moving within contents
                        if (safeTargetSlot != curSlot) {
                            container.contentsItemIds.RemoveAt(curSlot);
                            return container.contentsItemIds.InsertSafe(safeTargetSlot, id);
                        }
                    }
                    return curSlot;
                }

                // Moving into contents
                world.playerManager.sendAllVisible(id, new ContainMsg {
                    childIdWithPosStamp = getInstanceIdWithStamp(++physics.timestamps[(int)PhysicsTimeStamp.POSITION]),
                });

                if (container.containedItemIds.Add(id)) {
                    containerId = container.id;
                }

                return container.contentsItemIds.InsertSafe(safeTargetSlot, id);
            } else if (containerId != InstanceId.NULL) {
                if (world.objectManager.tryGet(containerId, out WorldObject? curContainer)) {
                    int curSlot = curContainer.contentsItemIds?.IndexOf(id) ?? -1;

                    // TODO: Also unequip

                    curContainer.containedItemIds!.Remove(id);
                    if (curSlot != -1) {
                        curContainer.contentsItemIds!.RemoveAt(curSlot);
                    }

                    containerId = InstanceId.NULL;

                    pos = curContainer.pos;

                    return curSlot;
                }

                containerId = InstanceId.NULL;
            }

            return -1;
        }

        public static void moveItem(World world, InvMoveDesc moveDesc, Player? requester = null) {
            WorldObject? fromContainer = null;
            WorldObject? targetContainer = null;
            if (!world.objectManager.tryGet(moveDesc.itemId, out WorldObject? item)) {
                moveDesc.error = ErrorType.GeneralInventoryFailure;
            } else {
                moveDesc.actualFromContainerId = item.containerId;
                moveDesc.actualTargetContainerId = moveDesc.targetId;

                // TODO: Check to see if requester has permissions to the from + to containers
                if ((moveDesc.actualFromContainerId != InstanceId.NULL && !world.objectManager.tryGet(moveDesc.actualFromContainerId, out fromContainer))
                    || (moveDesc.actualTargetContainerId != InstanceId.NULL && !world.objectManager.tryGet(moveDesc.actualTargetContainerId, out targetContainer))) {
                    moveDesc.error = ErrorType.GeneralInventoryFailure;
                } else if (fromContainer != null && (fromContainer.containedItemIds == null || !fromContainer.containedItemIds.Contains(item.id))) {
                    moveDesc.error = ErrorType.ContainerDoesNotContainItem;
                } else {
                    moveDesc.itemVisualDescDid = item.physicsEntityDid;
                    moveDesc.itemAprDid = item.pileAppearanceDid;

                    moveDesc.actualTargetSlot = item.setContainer(targetContainer, moveDesc.targetSlot);

                    BehaviorId behaviorId = BehaviorId.Undef;
                    WorldObject? behaviorTarget = null;

                    if (fromContainer != null && targetContainer == null) {
                        behaviorId = BehaviorId.putdown;
                        behaviorTarget = fromContainer;
                    } else if (fromContainer == null && targetContainer != null) {
                        behaviorId = BehaviorId.pickup;
                        behaviorTarget = targetContainer;
                    }

                    if (behaviorTarget != null) {
                        behaviorTarget.doBehavior(new() {
                            packFlags = BehaviorParams.PackFlag.BEHAVIOR_ID | BehaviorParams.PackFlag.FADE_CHILDREN | BehaviorParams.PackFlag.FXSCRIPT | BehaviorParams.PackFlag.TARGET | BehaviorParams.PackFlag.VDESC,
                            behaviorId = behaviorId,
                            fxScriptId = new(0x57000005),
                            targetId = item.id,
                            visualDescToClone = item.physicsEntityDid,
                            clonedAprDid = item.pileAppearanceDid,
                            fadeChildren = true,
                        });
                    }
                }
            }

            if (world.objectManager.tryGet(moveDesc.actualFromContainerId, out fromContainer) && fromContainer.player != null) {
                if (moveDesc.actualFromContainerId == moveDesc.actualTargetContainerId) {
                    world.playerManager.send(fromContainer.player, new InterpCEventPrivateMsg {
                        netEvent = new ReorganizeContentsDoneCEvt {
                            moveDesc = moveDesc,
                        }
                    });
                } else {
                    world.playerManager.send(fromContainer.player, new InterpCEventPrivateMsg {
                        netEvent = new MoveItemDoneCEvt {
                            moveDesc = moveDesc,
                        }
                    });
                }
            }

            if (world.objectManager.tryGet(moveDesc.actualTargetContainerId, out targetContainer) && targetContainer.player != null) {
                if (moveDesc.actualFromContainerId != moveDesc.actualTargetContainerId) {
                    world.playerManager.send(targetContainer.player, new InterpCEventPrivateMsg {
                        netEvent = new MoveItemDoneCEvt {
                            moveDesc = moveDesc,
                        }
                    });
                }
            }
        }
    }
}
