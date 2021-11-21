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

        public ErrorType equip(InvLoc equipLoc, WorldObject? item) {
            if (item != null) {
                if (isEquipped(equipLoc)) {
                    return ErrorType.InvSlotFull;
                } else if (!contains(item.id)) {
                    return ErrorType.Usage_InteractionTargetNotContained;
                } else if (!item.validInvLocs.HasFlag(equipLoc)) {
                    return ErrorType.WrongInvSlot;
                } else if (containedItemIds == null || !containedItemIds.Contains(item.id)) {
                    return ErrorType.ContainerDoesNotContainItem;
                }

                if (invLocToEquippedItemId == null) {
                    invLocToEquippedItemId = new();
                }

                invLocToEquippedItemId[equipLoc] = item.id;

                item.equipperId = id;
                item.equippedLocation = equipLoc;

                HoldingLocation holdLoc = item.primaryHoldLoc;
                if (holdLoc != HoldingLocation.Invalid) {
                    item.setParent(this, holdLoc, item.holdOrientation);
                }

                contentsItemIds!.Remove(item.id);

                setItemVisualApplied(item, true);
            } else {
                if (invLocToEquippedItemId != null && world.objectManager.tryGet(invLocToEquippedItemId[equipLoc], out WorldObject? curItem)) {
                    if (!invLocToEquippedItemId.TryGetValue(equipLoc, out InstanceId equippedItemId) || curItem.id != equippedItemId) {
                        return ErrorType.NotEquipped;
                    } else if (containedItemIds == null || !containedItemIds.Contains(curItem.id)) {
                        return ErrorType.ContainerDoesNotContainItem;
                    }

                    invLocToEquippedItemId.Remove(equipLoc);

                    curItem.setParent(null);

                    setItemVisualApplied(curItem, false);
                }
            }

            syncMode();

            return ErrorType.None;
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
