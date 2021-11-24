using AC2RE.Definitions;

namespace AC2RE.Server {

    internal class InventoryMessageProcessor : BaseMessageProcessor {

        public InventoryMessageProcessor(World world) : base(world) {

        }

        public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
            switch (genericMsg.opcode) {
                case MessageOpcode.Interp__InterpSEvent: {
                        InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                        if (msg.netEvent.funcId == ServerEventFunctionId.Inventory__DirectiveEquipItem) {
                            DirectiveEquipItemSEvt sEvent = (DirectiveEquipItemSEvt)msg.netEvent;

                            WorldObject.equip(world, sEvent.equipDesc, player);
                        } else if (msg.netEvent.funcId == ServerEventFunctionId.Inventory__DirectiveUnEquipItem) {
                            DirectiveUnequipItemSEvt sEvent = (DirectiveUnequipItemSEvt)msg.netEvent;
                            
                            WorldObject.unequip(world, sEvent.equipDesc, player);
                        } else if (msg.netEvent.funcId == ServerEventFunctionId.Inventory__DirectiveMoveItem) {
                            DirectiveMoveItemSEvt sEvent = (DirectiveMoveItemSEvt)msg.netEvent;

                            WorldObject.moveItem(world, sEvent.moveDesc, player);
                        } else if (msg.netEvent.funcId == ServerEventFunctionId.Inventory__DirectiveReorganizeContents) {
                            DirectiveReorganizeContentsSEvt sEvent = (DirectiveReorganizeContentsSEvt)msg.netEvent;

                            WorldObject.moveItem(world, sEvent.moveDesc, player);
                        } else {
                            return false;
                        }
                        break;
                    }
                default:
                    return false;
            }
            return true;
        }
    }
}
