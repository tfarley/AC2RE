using AC2RE.Definitions;
using System.Collections.Generic;
using System.Linq;

namespace AC2RE.Server {

    internal partial class WorldObject {

        private Dictionary<InvLoc, InstanceId>? invLocToEquippedItemId;

        public IEnumerable<KeyValuePair<InvLoc, InstanceId>> invLocToEquippedItemIdEnumerable => invLocToEquippedItemId ?? Enumerable.Empty<KeyValuePair<InvLoc, InstanceId>>();
        public IEnumerable<InstanceId> equippedItemIdsEnumerable => invLocToEquippedItemId?.Values ?? Enumerable.Empty<InstanceId>();

        private void initEquip() {

        }

        public void recacheEquip(IEnumerable<WorldObject> equippedItems) {
            if (invLocToEquippedItemId == null) {
                invLocToEquippedItemId = new();
            }

            foreach (WorldObject equippedItem in equippedItems) {
                invLocToEquippedItemId[equippedItem.equippedLocation] = equippedItem.id;
                contentsItemIds!.Remove(equippedItem.id);
            }
        }

        public InstanceId getEquipped(InvLoc invLoc) {
            return invLocToEquippedItemId?.GetValueOrDefault(invLoc, InstanceId.NULL) ?? InstanceId.NULL;
        }

        public bool isEquipped(InvLoc invLoc) {
            return invLocToEquippedItemId != null && invLocToEquippedItemId.ContainsKey(invLoc);
        }

        public bool isEquipped(InstanceId itemId) {
            return invLocToEquippedItemId != null && invLocToEquippedItemId.ContainsValue(itemId);
        }

        public static void toggleAutoEquip(World world, WorldObject equipper, WorldObject item, Player? requester = null) {
            if (item.equipperId != equipper.id) {
                autoEquip(world, equipper, item, requester);
            } else {
                autoUnequip(world, equipper, item, requester);
            }
        }

        public static void autoEquip(World world, WorldObject equipper, WorldObject item, Player? requester = null) {
            if (item.equipperId != equipper.id) {
                equip(world, new InvEquipDesc() {
                    itemId = item.id,
                    location = item.preferredInvLoc,
                    equipperId = equipper.id,
                    containerId = item.containerId,
                }, requester);
            }
        }

        public static void autoUnequip(World world, WorldObject equipper, WorldObject item, Player? requester = null) {
            if (item.equipperId == equipper.id) {
                unequip(world, new InvEquipDesc() {
                    itemId = item.id,
                    location = item.equippedLocation,
                    equipperId = equipper.id,
                    containerId = item.containerId,
                }, requester);
            }
        }

        public static void equip(World world, InvEquipDesc equipDesc, Player? requester = null) {
            if (!world.objectManager.tryGet(equipDesc.equipperId, out WorldObject? equipper) || !world.objectManager.tryGet(equipDesc.itemId, out WorldObject? item)) {
                equipDesc.error = ErrorType.GeneralInventoryFailure;
            } else if (requester != null && requester != equipper.player) {
                equipDesc.error = ErrorType.Usage_NoPermission;
            } else if (equipper.isEquipped(equipDesc.location)) {
                equipDesc.error = ErrorType.InvSlotFull;
            } else if (!equipper.contains(equipDesc.itemId)) {
                equipDesc.error = ErrorType.Usage_InteractionTargetNotContained;
            } else if (!item.validInvLocs.HasFlag(equipDesc.location)) {
                equipDesc.error = ErrorType.WrongInvSlot;
            } else if (equipper.containedItemIds == null || !equipper.containedItemIds.Contains(item.id)) {
                equipDesc.error = ErrorType.ContainerDoesNotContainItem;
            } else {
                if (equipper.invLocToEquippedItemId == null) {
                    equipper.invLocToEquippedItemId = new();
                }

                equipDesc.actualLocation = equipDesc.location;
                equipper.invLocToEquippedItemId[equipDesc.actualLocation] = item.id;

                item.equipperId = equipper.id;
                item.equippedLocation = equipDesc.actualLocation;

                HoldingLocation holdLoc = item.primaryHoldLoc;
                if (holdLoc != HoldingLocation.Invalid) {
                    item.setParent(equipper, holdLoc, item.holdOrientation);
                }

                // Remove from contents, but stay contained
                equipDesc.containerSlot = item.setContainer(equipper, -2);

                equipper.setItemVisualApplied(item, true);
                equipper.syncMode();
            }

            if (world.objectManager.tryGet(equipDesc.equipperId, out equipper) && equipper.player != null) {
                world.playerManager.send(equipper.player, new InterpCEventPrivateMsg {
                    netEvent = new EquipItemDoneCEvt {
                        equipDesc = equipDesc,
                    }
                });
            }
        }

        public static void unequip(World world, InvEquipDesc equipDesc, Player? requester = null) {
            if (!world.objectManager.tryGet(equipDesc.equipperId, out WorldObject? equipper) || !world.objectManager.tryGet(equipDesc.itemId, out WorldObject? item)) {
                equipDesc.error = ErrorType.GeneralInventoryFailure;
            } else if (equipper.invLocToEquippedItemId == null || !equipper.invLocToEquippedItemId.TryGetValue(equipDesc.location, out InstanceId equippedItemId) || item.id != equippedItemId || item.equippedLocation != equipDesc.location || item.equipperId != equipDesc.equipperId) {
                equipDesc.error = ErrorType.NotEquipped;
            } else if (requester != null && requester != equipper.player) {
                equipDesc.error = ErrorType.Usage_NoPermission;
            } else if (equipper.containedItemIds == null || !equipper.containedItemIds.Contains(item.id)) {
                equipDesc.error = ErrorType.ContainerDoesNotContainItem;
            } else {
                equipper.invLocToEquippedItemId.Remove(equipDesc.location);

                equipDesc.takenSlots = item.equippedLocation;

                item.equipperId = InstanceId.NULL;
                item.equippedLocation = InvLoc.None;

                item.setParent(null);

                equipDesc.containerSlot = item.setContainer(equipper, equipDesc.targetContainerSlot);

                equipper.setItemVisualApplied(item, false);
                equipper.syncMode();
            }

            if (world.objectManager.tryGet(equipDesc.equipperId, out equipper) && equipper.player != null) {
                world.playerManager.send(equipper.player, new InterpCEventPrivateMsg {
                    netEvent = new UnequipItemDoneCEvt {
                        equipDesc = equipDesc,
                    }
                });
            }
        }

        private void setItemVisualApplied(WorldObject item, bool applied) {
            WState clothingWeenieState = world.contentManager.getWeenieStateFromEntityDid(item.entityDid);
            if (clothingWeenieState.package is Clothing clothing) {
                PhysiqueSpeciesSexId speciesSexKey = new(species, sex);
                if (clothing.wornAppearanceDidHash.TryGetValue(speciesSexKey, out DataId appearanceDid)) {
                    if (applied) {
                        // TODO: contentManager.getInheritedVisualDesc(item.visual)? But it seems wrong, since the topmost parent of human starter pants is 0x1F00003E which is actually overriding skin color which doesn't make sense - not sure if that's a special override that just needs to be blocked or if inheritance isn't the correct thing to do...
                        if (item.globalAppearanceModifiers?.appearanceInfos?.TryGetValue(appearanceDid, out Dictionary<AppearanceKey, float>? appearanceModifiers) ?? false) {
                            appearanceModifiers = new(appearanceModifiers);
                        } else {
                            appearanceModifiers = new();
                        }
                        appearanceModifiers[AppearanceKey.Worn] = 1.0f;
                        globalAppearanceModifiers.appearanceInfos[appearanceDid] = appearanceModifiers;
                    } else {
                        globalAppearanceModifiers.appearanceInfos.Remove(appearanceDid);
                    }
                    visualDirty = true;
                }
            }
        }
    }
}
