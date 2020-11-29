using AC2RE.Definitions;

namespace AC2RE.Server {

    internal class InventoryManager {

        private readonly PlayerManager playerManager;
        private readonly WorldObjectManager objectManager;

        public InventoryManager(PlayerManager playerManager, WorldObjectManager objectManager) {
            this.playerManager = playerManager;
            this.objectManager = objectManager;
        }

        public ErrorType setItemEquipped(WorldObject equipper, InvLoc equipLoc, WorldObject? item) {
            if (item != null) {
                if (equipper.equippedItemIds.ContainsKey(equipLoc)) {
                    return ErrorType.INVSLOTFULL;
                } else if (!item.validInvLocs.HasFlag(equipLoc)) {
                    return ErrorType.WRONGINVSLOT;
                } else if (!equipper.containedItemIds.Contains(item.id)) {
                    return ErrorType.CONTAINERDOESNOTCONTAINITEM;
                }

                equipper.equippedItemIds[equipLoc] = item.id;

                item.wielderId = equipper.id;

                HoldingLocation holdLoc = item.primaryHoldLoc;
                if (holdLoc != HoldingLocation.INVALID) {
                    item.setParent(equipper, holdLoc, item.holdOrientation);
                }
            } else {
                WorldObject? curItem = objectManager.get(equipper.equippedItemIds[equipLoc]);
                if (curItem != null) {
                    if (!equipper.equippedItemIds.TryGetValue(equipLoc, out InstanceId equippedItemId) || curItem.id != equippedItemId) {
                        return ErrorType.NOTEQUIPPED;
                    } else if (!equipper.containedItemIds.Contains(curItem.id)) {
                        return ErrorType.CONTAINERDOESNOTCONTAINITEM;
                    }

                    equipper.equippedItemIds.Remove(equipLoc);

                    curItem.setParent(null);
                }
            }

            return ErrorType.NONE;
        }

        public void equipItem(InvEquipDesc request, Player requester) {
            if (request.equipperId != requester.characterId) {
                request.error = ErrorType.ITEMNOTOWNEDBYCONTAINER;
            } else {
                WorldObject? equipper = objectManager.get(request.equipperId);
                WorldObject? item = objectManager.get(request.itemId);

                if (equipper != null && item != null) {
                    request.error = setItemEquipped(equipper, request.location, item);
                }
            }

            playerManager.send(requester, new InterpCEventPrivateMsg {
                netEvent = new EquipItemDoneCEvt {
                    equipDesc = request,
                }
            });
        }

        public void unequipItem(InvEquipDesc request, Player requester) {
            if (request.equipperId != requester.characterId) {
                request.error = ErrorType.ITEMNOTOWNEDBYCONTAINER;
            } else {
                WorldObject? equipper = objectManager.get(request.equipperId);
                WorldObject? item = objectManager.get(request.itemId);
                WorldObject? container = objectManager.get(request.containerId);

                if (equipper != null && item != null) {
                    request.error = setItemEquipped(equipper, request.location, null);
                    if (request.error == ErrorType.NONE) {
                        request.targetContainerSlot = (uint)item.setContainer(container, (int)request.containerSlot);
                        request.containerSlot = request.targetContainerSlot;
                    }
                }
            }

            playerManager.send(requester, new InterpCEventPrivateMsg {
                netEvent = new UnequipItemDoneCEvt {
                    equipDesc = request,
                }
            });
        }
    }
}
