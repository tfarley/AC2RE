using AC2E.Def;
using AC2E.Utils;
using System;

namespace AC2E.Server {

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
            equipper.containedItems.Add(item.id);
        }

        public bool setItemEquipped(WorldObject equipper, WorldObject item, InvLoc equipLoc) {
            equipper.containedItems.Remove(item.id);
            equipper.equippedItems[equipLoc] = item.id;

            item.qualities.weenieDesc.wielderId = equipper.id;
            item.qualities.weenieDesc.packFlags |= WeenieDesc.PackFlag.WIELDER_ID;

            return parentIfNeeded(equipper, item);
        }

        private bool parentIfNeeded(WorldObject parent, WorldObject item) {
            if (!item.qualities.ints.TryGetValue(IntStat.INV_PRIMARYPARENTINGLOCATION, out int parentingLocation)) {
                return false;
            }

            item.physics.parentId = parent.id;
            item.physics.locationId = (HoldingLocation)parentingLocation;
            item.physics.parentInstanceStamp = parent.instanceStamp;
            item.physics.packFlags |= PhysicsDesc.PackFlag.PARENT;

            if (item.qualities.ints.TryGetValue(IntStat.PARENTINGORIENTATION, out int parentingOrientation)) {
                item.physics.orientationId = (Orientation)parentingOrientation;
                item.physics.packFlags |= PhysicsDesc.PackFlag.ORIENTATION;
            }

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
                item.physics.packFlags &= ~PhysicsDesc.PackFlag.PARENT;

                item.physics.orientationId = Orientation.DEFAULT;
                item.physics.packFlags &= ~PhysicsDesc.PackFlag.ORIENTATION;
            }
        }

        public void equipItem(InvEquipDesc request, Player requester) {
            if (requester != null && request.equipperId != requester.characterId) {
                request.status = ErrorType.ITEMNOTOWNEDBYCONTAINER;
            } else {
                WorldObject? equipper = objectManager.get(request.equipperId);
                WorldObject? item = objectManager.get(request.itemId);

                if (equipper != null && item != null) {
                    if (equipper.equippedItems.ContainsKey(request.location)) {
                        request.status = ErrorType.INVSLOTFULL;
                    } else if (!item.qualities.ints.TryGetValue(IntStat.VALIDINVENTORYLOCATIONS, out int validInventoryLocations) || (validInventoryLocations & (int)request.location) == 0) {
                        request.status = ErrorType.WRONGINVSLOT;
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

                        request.status = ErrorType.NONE;
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
                request.status = ErrorType.ITEMNOTOWNEDBYCONTAINER;
            } else {
                WorldObject? equipper = objectManager.get(request.equipperId);
                WorldObject? item = objectManager.get(request.itemId);
                WorldObject? container = objectManager.get(request.containerId);

                if (equipper != null && item != null) {
                    if (!equipper.equippedItems.TryGetValue(request.location, out InstanceId equippedItemId) || item.id != equippedItemId) {
                        request.status = ErrorType.NOTEQUIPPED;
                    } else {
                        equipper.equippedItems.Remove(request.location);

                        if (container != null) {
                            // TODO: Hack to prevent weird case where icon wraps to next line when dragged to an empty slot in inventory bag
                            request.containerSlot = (uint)Math.Min((int)request.containerSlot, container.containedItems.Count - 1);

                            request.targetContainerSlot = (uint)container.containedItems.InsertSafe((int)request.containerSlot, item.id);
                            request.containerSlot = request.targetContainerSlot;
                        }

                        deparent(equipper, item);

                        item.physics.timestamps[0]++;
                        playerManager.broadcastSend(requester.clientId, new ContainMsg {
                            childIdWithPosStamp = item.getInstanceIdWithStamp(item.physics.timestamps[0]),
                        });

                        request.status = ErrorType.NONE;
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
