using AC2RE.Definitions;

namespace AC2RE.Server {

    internal class UsageMessageProcessor : BaseMessageProcessor {

        public UsageMessageProcessor(World world) : base(world) {

        }

        public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
            switch (genericMsg.opcode) {
                case MessageOpcode.Interp__InterpSEvent: {
                        InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                        if (msg.netEvent.funcId == ServerEventFunctionId.Usage__TryToUseItem) {
                            TryToUseItemSEvt sEvent = (TryToUseItemSEvt)msg.netEvent;
                            if (tryGetCharacter(player, out WorldObject? character) && world.objectManager.tryGet(sEvent.usageDesc.itemId, out WorldObject? item)) {
                                if (item.containerId != character.id && item.takeable) {
                                    InvMoveDesc moveDesc = new() {
                                        itemId = item.id,
                                        splitItemId = item.id,
                                        fromId = item.containerId,
                                        actualFromContainerId = item.containerId,
                                        targetId = character.id,
                                        actualTargetContainerId = character.id,
                                        mergeContainerId = character.id,
                                        itemVisualDescDid = item.physicsEntityDid,
                                        splitItemEntityDid = item.entityDid,
                                        allItemUnitsTaken = true,
                                        doContain = true,
                                        moveType = 12,
                                        itemAprDid = item.pileAppearanceDid,
                                    };
                                    WorldObject.moveItem(world, moveDesc, player);
                                } else {
                                    WState weenieState = world.contentManager.getWeenieStateFromEntityDid(item.entityDid);
                                    if (weenieState.package is gmEntity entity) {
                                        DataId usageActionDid = entity.usageAction?.did ?? DataId.NULL;
                                        if (usageActionDid != DataId.NULL) {
                                            WState usageActionWeenieState = world.contentManager.getWeenieState(usageActionDid);

                                            if (PackageManager.isPackageType(usageActionWeenieState.packageType, PackageType.EquippableUsageAction) && item.containerId == character.id) {
                                                WorldObject.toggleAutoEquip(world, character, item, player);
                                            } else if (usageActionWeenieState.packageType == PackageType.PortalUsageAction || usageActionWeenieState.packageType == PackageType.LifestoneUsageAction || usageActionWeenieState.packageType == PackageType.NPCUsageAction) {
                                                character.moveTo(sEvent.usageDesc.itemId, 1.0f, 1.0f);
                                            } else {
                                                sEvent.usageDesc.error = ErrorType.Usage_GeneralError;

                                                send(player, new InterpCEventPrivateMsg {
                                                    netEvent = new TryToUseItemDoneCEvt {
                                                        usageDesc = sEvent.usageDesc,
                                                    }
                                                });
                                            }
                                        } else {
                                            sEvent.usageDesc.error = ErrorType.Usage_CantUse;

                                            send(player, new InterpCEventPrivateMsg {
                                                netEvent = new TryToUseItemDoneCEvt {
                                                    usageDesc = sEvent.usageDesc,
                                                }
                                            });
                                        }
                                    }
                                }
                            } else {
                                sEvent.usageDesc.error = ErrorType.Usage_GeneralError;

                                send(player, new InterpCEventPrivateMsg {
                                    netEvent = new TryToUseItemDoneCEvt {
                                        usageDesc = sEvent.usageDesc,
                                    }
                                });
                            }
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
