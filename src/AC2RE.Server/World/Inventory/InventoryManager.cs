using AC2RE.Definitions;
using AC2RE.Utils;
using System;

namespace AC2RE.Server {

    internal class InventoryManager {

        private readonly PacketHandler packetHandler;
        private readonly PlayerManager playerManager;
        private readonly WorldObjectManager objectManager;

        public InventoryManager(PacketHandler packetHandler, PlayerManager playerManager, WorldObjectManager objectManager) {
            this.packetHandler = packetHandler;
            this.playerManager = playerManager;
            this.objectManager = objectManager;
        }

        public void giveItem(WorldObject equipper, WorldObject item) {
            equipper.containedItemIds.Add(item.id);
        }

        public bool setItemEquipped(WorldObject equipper, WorldObject item, InvLoc equipLoc) {
            equipper.containedItemIds.Remove(item.id);
            equipper.equippedItemIds[equipLoc] = item.id;

            item.wielderId = equipper.id;

            return parentIfNeeded(equipper, item);
        }

        private bool parentIfNeeded(WorldObject parent, WorldObject item) {
            HoldingLocation holdLoc = item.primaryHoldLoc;
            if (holdLoc == HoldingLocation.INVALID) {
                return false;
            }

            item.physics.parentId = parent.id;
            item.physics.locationId = holdLoc;
            item.physics.parentInstanceStamp = parent.instanceStamp;
            item.physics.orientationId = item.holdOrientation;

            return true;
        }

        private void deparent(WorldObject parent, WorldObject item) {
            if (item.physics.parentId != InstanceId.NULL) {
                item.physics.timestamps[0]++;
                playerManager.broadcastSend(new DeParentMsg {
                    senderIdWithStamp = parent.getInstanceIdWithStamp(),
                    childIdWithPosStamp = item.getInstanceIdWithStamp(item.physics.timestamps[0]),
                });

                item.physics.parentId = InstanceId.NULL;
                item.physics.locationId = HoldingLocation.INVALID;
                item.physics.parentInstanceStamp = 0;
                item.physics.orientationId = Orientation.DEFAULT;
            }
        }

        public void equipItem(InvEquipDesc request, Player requester) {
            if (requester != null && request.equipperId != requester.characterId) {
                request.error = ErrorType.ITEMNOTOWNEDBYCONTAINER;
            } else {
                WorldObject? equipper = objectManager.get(request.equipperId);
                WorldObject? item = objectManager.get(request.itemId);

                if (equipper != null && item != null) {
                    if (equipper.equippedItemIds.ContainsKey(request.location)) {
                        request.error = ErrorType.INVSLOTFULL;
                    } else if (!item.validInvLocs.HasFlag(request.location)) {
                        request.error = ErrorType.WRONGINVSLOT;
                    } else {
                        if (setItemEquipped(equipper, item, request.location)) {
                            item.physics.timestamps[0]++;
                            playerManager.broadcastSend(new ParentMsg {
                                senderIdWithStamp = item.getInstanceIdWithStamp(),
                                parentIdWithChildPosStamp = equipper.getInstanceIdWithStamp(item.physics.timestamps[0]),
                                childLocation = item.physics.locationId,
                                orientationId = item.physics.orientationId,
                            });
                        }

                        request.error = ErrorType.NONE;
                    }
                }
            }

            if (requester != null) {
                packetHandler.send(requester.clientId, new InterpCEventPrivateMsg {
                    netEvent = new EquipItemDoneCEvt {
                        equipDesc = request,
                    }
                });
            }
        }

        public void unequipItem(InvEquipDesc request, Player requester) {
            if (request.equipperId != requester.characterId) {
                request.error = ErrorType.ITEMNOTOWNEDBYCONTAINER;
            } else {
                WorldObject? equipper = objectManager.get(request.equipperId);
                WorldObject? item = objectManager.get(request.itemId);
                WorldObject? container = objectManager.get(request.containerId);

                if (equipper != null && item != null) {
                    if (!equipper.equippedItemIds.TryGetValue(request.location, out InstanceId equippedItemId) || item.id != equippedItemId) {
                        request.error = ErrorType.NOTEQUIPPED;
                    } else {
                        equipper.equippedItemIds.Remove(request.location);

                        if (container != null) {
                            // TODO: Hack to prevent weird case where icon wraps to next line when dragged to an empty slot in inventory bag
                            request.containerSlot = (uint)Math.Min((int)request.containerSlot, container.containedItemIds.Count - 1);

                            request.targetContainerSlot = (uint)container.containedItemIds.InsertSafe((int)request.containerSlot, item.id);
                            request.containerSlot = request.targetContainerSlot;
                        }

                        deparent(equipper, item);

                        item.physics.timestamps[0]++;
                        playerManager.broadcastSend(requester.clientId, new ContainMsg {
                            childIdWithPosStamp = item.getInstanceIdWithStamp(item.physics.timestamps[0]),
                        });

                        request.error = ErrorType.NONE;
                    }
                }
            }

            if (requester != null) {
                packetHandler.send(requester.clientId, new InterpCEventPrivateMsg {
                    netEvent = new UnequipItemDoneCEvt {
                        equipDesc = request,
                    }
                });
            }
        }
    }
}
