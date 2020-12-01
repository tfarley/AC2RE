using AC2RE.Definitions;

namespace AC2RE.Server {

    internal class InventoryManager {

        private readonly PlayerManager playerManager;
        private readonly WorldObjectManager objectManager;

        public InventoryManager(PlayerManager playerManager, WorldObjectManager objectManager) {
            this.playerManager = playerManager;
            this.objectManager = objectManager;
        }

        public void equipItem(InvEquipDesc request, Player requester) {
            if (request.equipperId != requester.characterId) {
                request.error = ErrorType.ITEMNOTOWNEDBYCONTAINER;
            } else {
                WorldObject? equipper = objectManager.get(request.equipperId);
                WorldObject? item = objectManager.get(request.itemId);

                if (equipper != null && item != null) {
                    request.error = equipper.equip(request.location, item);
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
                    request.error = equipper.equip(request.location, null);
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
