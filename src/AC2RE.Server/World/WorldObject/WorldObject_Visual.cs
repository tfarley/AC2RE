﻿using AC2RE.Definitions;
using System.Numerics;

namespace AC2RE.Server;

internal partial class WorldObject {

    public VisualDesc visual;

    private bool visualDirty;

    private void initVisual() {
        visual = new();
    }

    public Vector3 visualScale {
        get => visual.scale;
        set {
            visual.scale = value;
            visualDirty = true;
        }
    }

    public PartGroupDataDesc globalAppearanceModifiers {
        get => visual.globalAppearanceModifiers;
        set {
            visual.globalAppearanceModifiers = value;
            visualDirty = true;
        }
    }

    private void broadcastVisuals() {
        if (visualDirty) {
            if (inWorld) {
                world.playerManager.sendAllVisible(id, new UpdateVisualDescMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), visual));
            }

            visualDirty = false;
        }
    }

    public void doFx(FxId fxId, float scalar) {
        world.playerManager.sendAllVisible(id, new DoFxMsg {
            senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
            fxId = fxId,
            scalar = scalar,
        }, true);
    }

    public void stopFx(FxId fxId) {
        world.playerManager.sendAllVisible(id, new StopFxMsg {
            senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
            fxId = fxId,
        }, true);
    }

    public void doMode(ModeId modeId) {
        world.playerManager.sendAllVisible(id, new DoModeMsg {
            senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
            modeId = modeId,
        }, true);
    }

    public void doBehavior(BehaviorParams behaviorParams) {
        world.playerManager.sendAllVisible(id, new DoBehaviorMsg {
            senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
            behaviorParams = behaviorParams,
        }, true);
    }

    public void stopBehavior(BehaviorId behaviorId) {
        world.playerManager.sendAllVisible(id, new StopBehaviorMsg {
            senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
            behaviorId = behaviorId,
        }, true);
    }

    public void doStory(PhysicsStory story) {
        world.playerManager.sendAllVisible(id, new DoStoryMsg {
            senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
            story = story,
        }, true);
    }

    private void syncMode() {
        ModeId newMode = mode;
        if (attacking == false) {
            newMode = ModeId.peace;
        } else {
            WorldObject? primaryWeapon = world.objectManager.get(getEquipped(InvLoc.PrimaryHand));
            WorldObject? secondaryWeapon = world.objectManager.get(getEquipped(InvLoc.SecondaryHand));
            if (primaryWeapon != null && secondaryWeapon == null) {
                newMode = primaryWeapon.singleWeaponMode;
            } else if (primaryWeapon == null && secondaryWeapon != null) {
                newMode = secondaryWeapon.singleWeaponMode;
            } else if (primaryWeapon != null && secondaryWeapon != null) {
                newMode = secondaryWeapon.implementType == ImplementType.Shield
                    ? primaryWeapon.withShieldMode
                    : primaryWeapon.dualWieldMode;
            } else {
                newMode = ModeId.combat_martialarts;
            }
        }

        if (newMode != mode) {
            doMode(newMode);
        }
    }
}
