﻿using AC2RE.Definitions;
using AC2RE.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AC2RE.Server;

internal partial class WorldObject {

    public PhysicsDesc physics = new() {
        pos = new(),
        visualOrderStamp = 1,
    };
    public LandblockId landblockId => physics.pos.cell.landblockId;
    private HashSet<InstanceId>? childIds;

    public IEnumerable<InstanceId> childIdsEnumerable => childIds ?? Enumerable.Empty<InstanceId>();

    public InstanceId parentId => physics.parentId;

    public Vector3 motion;
    public bool jumped;
    public bool impulsed;
    public Vector3 impulseVel;

    private CellId lastSentCell;
    private bool positionDirty;

    private void initPhysics() {
        physics.timestamps[(int)PhysicsTimeStamp.POSITION] = 1;
    }

    public void recachePhysics(IEnumerable<WorldObject> childWorldObjects) {
        if (childIds == null) {
            childIds = new();
        }

        foreach (WorldObject childWorldObject in childWorldObjects) {
            childIds.Add(childWorldObject.id);
        }
    }

    public float heading {
        get {
            Vector3 forwardDir = Vector3.Transform(new Vector3(0.0f, 1.0f, 0.0f), physics.pos.frame.rot);
            return MathF.Atan2(forwardDir.X, forwardDir.Y) * MathUtil.RAD_TO_DEG;
        }
        set {
            physics.pos.frame.rot = MathUtil.quaternionFromAxisAngleLeftHanded(new(0.0f, 0.0f, 1.0f), value * MathUtil.DEG_TO_RAD);
            positionDirty = true;
        }
    }

    public ModeId mode => physics.modeId;

    public float velScale => physics.velScale;

    public void setVelScale(float velScale) {
        physics.velScale = velScale;
        world.playerManager.sendAllVisible(id, new SetVelocityScaleMsg {
            senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
            velScale = physics.velScale,
        });
    }

    public Position pos {
        get => physics.pos;
        set {
            physics.pos.cell = value.cell;
            physics.pos.frame = value.frame;
            positionDirty = true;
        }
    }

    public PositionOffset offset {
        get => new(physics.pos.cell, physics.pos.frame.pos);
        set {
            physics.pos.cell = value.cell;
            physics.pos.frame.pos = value.offset;
            positionDirty = true;
        }
    }

    public HoldingLocation locationId => physics.locationId;
    public Orientation orientationId => physics.orientationId;

    public void setLookAt(InstanceId lookAtId) {
        physics.lookAtId = lookAtId;
        world.playerManager.sendAllVisible(id, new LookAtMsg {
            senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
            targetId = physics.lookAtId,
        });
    }

    public void setLookAt(float lookAtDirX, float lookAtDirZ) {
        physics.headingX = lookAtDirX;
        physics.headingZ = lookAtDirZ;
        world.playerManager.sendAllVisible(id, new LookAtDirMsg {
            senderIdWithStamp = getInstanceIdWithStamp(),
            x = physics.headingX,
            z = physics.headingZ,
        });
    }

    public PhysicsDesc.SliderData getSliderValue(uint slider) {
        return physics.sliders[slider];
    }

    public void setSliderValue(uint slider, float value, float velocity) {
        if (physics.sliders == null) {
            physics.sliders = new();
        }

        physics.sliders[slider] = new() {
            value = value,
            velocity = velocity,
        };

        world.playerManager.sendAllVisible(id, new DoSliderMsg {
            senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
            sliderId = slider,
            newValue = value,
            time = 0.1f, // TODO: Figure out what time means
        });
    }

    public void moveTo(InstanceId targetId, float desiredDist, float successDist, float desiredVel = 1.0f, bool toward = true) {
        world.playerManager.sendAllVisible(id, new MoveToMsg {
            senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
            movementParams = new() {
                packFlags = (toward ? MovementParameters.PackFlag.MOVE : MovementParameters.PackFlag.AWAY) | MovementParameters.PackFlag.OBJECT | MovementParameters.PackFlag.USE_CYL,
                desiredVel = desiredVel,
                failDist = float.MaxValue,
                desiredDist = desiredDist,
                maxSuccessDist = successDist,
                minSuccessDist = successDist,
                targetId = targetId,
            },
            movetoStamp = ++physics.timestamps[(int)PhysicsTimeStamp.MOVE_TO],
        });
    }

    private void broadcastPhysics() {
        if (positionDirty) {
            if (inWorld) {
                PositionPack posPack = new() {
                    time = world.serverTime.time,
                    offset = offset,
                    doMotion = motion,
                    heading = new(heading),
                    packFlags = PositionPack.PackFlag.CONTACT,
                    posStamp = ++physics.timestamps[(int)PhysicsTimeStamp.POSITION],
                    forcePosStamp = physics.timestamps[(int)PhysicsTimeStamp.FORCE_POSITION],
                    teleportStamp = physics.timestamps[(int)PhysicsTimeStamp.TELEPORT],
                };
                if (jumped || impulsed) {
                    if (jumped) {
                        posPack.packFlags |= PositionPack.PackFlag.JUMP;
                        jumped = false;
                    }
                    if (impulsed) {
                        posPack.packFlags |= PositionPack.PackFlag.IMPULSE;
                        impulsed = false;
                    }
                    posPack.impulseVel = impulseVel;
                }

                if (pos.cell != lastSentCell) {
                    world.playerManager.sendAllVisibleExcept(id, world.playerManager.get(id), new PositionCellMsg {
                        senderIdWithStamp = getInstanceIdWithStamp(),
                        posPack = posPack,
                    });
                } else {
                    world.playerManager.sendAllVisibleExcept(id, world.playerManager.get(id), new PositionMsg {
                        senderIdWithStamp = getInstanceIdWithStamp(),
                        posPack = posPack,
                    });
                }

                lastSentCell = pos.cell;
            }

            positionDirty = false;
        }
    }

    public void setParent(WorldObject? parent, HoldingLocation holdLoc = HoldingLocation.Invalid, Orientation holdOrientation = Orientation.Default) {
        if (parent != null) {
            if (physics.parentId != parent.id) {
                if (world.objectManager.tryGet(physics.parentId, out WorldObject? curParent)) {
                    if (curParent.childIds != null) {
                        curParent.childIds.Remove(id);
                    }

                    // TODO: Need to send DeParent too?
                }

                if (parent.childIds == null) {
                    parent.childIds = new();
                }

                parent.childIds.Add(id);

                physics.parentId = parent.id;
                physics.locationId = holdLoc;
                physics.parentInstanceStamp = parent.physics.instanceStamp;
                physics.orientationId = holdOrientation;

                world.landblockManager.syncObjectVisibility(this);

                world.playerManager.sendAllVisible(id, new ParentMsg {
                    senderIdWithStamp = getInstanceIdWithStamp(),
                    parentIdWithChildPosStamp = parent.getInstanceIdWithStamp(++physics.timestamps[(int)PhysicsTimeStamp.POSITION]),
                    childLocation = physics.locationId,
                    orientationId = physics.orientationId,
                });
            }
        } else if (physics.parentId != InstanceId.NULL) {
            if (world.objectManager.tryGet(physics.parentId, out WorldObject? curParent)) {
                if (curParent.childIds != null) {
                    curParent.childIds.Remove(id);
                }

                world.playerManager.sendAllVisible(id, new DeParentMsg {
                    senderIdWithStamp = curParent.getInstanceIdWithStamp(),
                    childIdWithPosStamp = getInstanceIdWithStamp(++physics.timestamps[(int)PhysicsTimeStamp.POSITION]),
                });
            }

            physics.parentId = InstanceId.NULL;
            physics.locationId = HoldingLocation.Invalid;
            physics.parentInstanceStamp = 0;
            physics.orientationId = Orientation.Default;

            world.landblockManager.syncObjectVisibility(this);
        }
    }
}
