using AC2RE.Definitions;

namespace AC2RE.Server {

    internal class InventoryManager {

        private readonly World world;

        public InventoryManager(World world) {
            this.world = world;
        }

        public void equipItem(InvEquipDesc request, Player requester) {
            if (request.equipperId != requester.characterId) {
                request.error = ErrorType.ITEMNOTOWNEDBYCONTAINER;
            } else if (world.objectManager.tryGet(request.equipperId, out WorldObject? equipper) && world.objectManager.tryGet(request.itemId, out WorldObject? item)) {
                request.error = equipper.equip(request.location, item);
            }

            world.playerManager.send(requester, new InterpCEventPrivateMsg {
                netEvent = new EquipItemDoneCEvt {
                    equipDesc = request,
                }
            });
        }

        public void unequipItem(InvEquipDesc request, Player requester) {
            if (request.equipperId != requester.characterId) {
                request.error = ErrorType.ITEMNOTOWNEDBYCONTAINER;
            } else {
                if (world.objectManager.tryGet(request.equipperId, out WorldObject? equipper) && world.objectManager.tryGet(request.itemId, out WorldObject? item)) {
                    request.error = equipper.equip(request.location, null);
                    if (request.error == ErrorType.NONE) {
                        request.targetContainerSlot = (uint)item.setContainer(world.objectManager.get(request.containerId), (int)request.containerSlot);
                        request.containerSlot = request.targetContainerSlot;
                    }
                }
            }

            world.playerManager.send(requester, new InterpCEventPrivateMsg {
                netEvent = new UnequipItemDoneCEvt {
                    equipDesc = request,
                }
            });
        }
    }
}
