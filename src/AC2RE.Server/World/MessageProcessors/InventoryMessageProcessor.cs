﻿using AC2RE.Definitions;

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
                            if (sEvent.equipDesc.equipperId != player.characterId) {
                                sEvent.equipDesc.error = ErrorType.ItemNotOwnedByContainer;
                            } else if (tryGetInWorld(sEvent.equipDesc.equipperId, out WorldObject? equipper) && tryGetInWorld(sEvent.equipDesc.itemId, out WorldObject? item)) {
                                sEvent.equipDesc.error = equipper.equip(sEvent.equipDesc.location, item);
                            }

                            send(player, new InterpCEventPrivateMsg {
                                netEvent = new EquipItemDoneCEvt {
                                    equipDesc = sEvent.equipDesc,
                                }
                            });
                        } else if (msg.netEvent.funcId == ServerEventFunctionId.Inventory__DirectiveUnEquipItem) {
                            DirectiveUnequipItemSEvt sEvent = (DirectiveUnequipItemSEvt)msg.netEvent;
                            if (sEvent.equipDesc.equipperId != player.characterId) {
                                sEvent.equipDesc.error = ErrorType.ItemNotOwnedByContainer;
                            } else {
                                if (tryGetInWorld(sEvent.equipDesc.equipperId, out WorldObject? equipper) && tryGetInWorld(sEvent.equipDesc.itemId, out WorldObject? item)) {
                                    sEvent.equipDesc.error = equipper.equip(sEvent.equipDesc.location, null);
                                    if (sEvent.equipDesc.error == ErrorType.None) {
                                        sEvent.equipDesc.targetContainerSlot = (uint)item.setContainer(world.objectManager.get(sEvent.equipDesc.containerId), (int)sEvent.equipDesc.containerSlot);
                                        sEvent.equipDesc.containerSlot = sEvent.equipDesc.targetContainerSlot;
                                    }
                                }
                            }

                            send(player, new InterpCEventPrivateMsg {
                                netEvent = new UnequipItemDoneCEvt {
                                    equipDesc = sEvent.equipDesc,
                                }
                            });
                        } else if (msg.netEvent.funcId == ServerEventFunctionId.Inventory__DirectiveMoveItem) {
                            DirectiveMoveItemSEvt sEvent = (DirectiveMoveItemSEvt)msg.netEvent;

                            send(player, new InterpCEventPrivateMsg {
                                netEvent = new MoveItemDoneCEvt {
                                    moveDesc = sEvent.moveDesc,
                                }
                            });
                        } else if (msg.netEvent.funcId == ServerEventFunctionId.Inventory__DirectiveReorganizeContents) {
                            DirectiveReorganizeContentsSEvt sEvent = (DirectiveReorganizeContentsSEvt)msg.netEvent;

                            send(player, new InterpCEventPrivateMsg {
                                netEvent = new ReorganizeContentsDoneCEvt {
                                    moveDesc = sEvent.moveDesc,
                                }
                            });
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
