using AC2RE.Definitions;
using AC2RE.Server.Database;
using AC2RE.Utils;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2RE.Server {

    internal partial class WorldObject {

        public PhysicsDesc physics;

        public Vector3 motion;
        public bool jumped;
        public bool impulsed;
        public Vector3 impulseVel;

        [DatabaseIgnore]
        public bool modeDirty;

        [DatabaseIgnore]
        public bool velScaleDirty;

        [DatabaseIgnore]
        public bool positionDirty;

        [DatabaseIgnore]
        public bool lookAtDirty;

        [DatabaseIgnore]
        private HashSet<uint> dirtySliders = new();

        private void initPhysics() {
            physics = new();
        }

        [DatabaseIgnore]
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

        [DatabaseIgnore]
        public ModeId mode {
            get => physics.modeId;
            set {
                physics.modeId = value;
                modeDirty = true;
            }
        }

        [DatabaseIgnore]
        public float velScale {
            get => physics.velScale;
            set {
                physics.velScale = value;
                velScaleDirty = true;
            }
        }

        [DatabaseIgnore]
        public Position pos {
            get => physics.pos;
            set {
                physics.pos = value;
                positionDirty = true;
            }
        }

        [DatabaseIgnore]
        public PositionOffset offset {
            get => new PositionOffset(physics.pos.cell, physics.pos.frame.pos);
            set {
                physics.pos.cell = value.cell;
                physics.pos.frame.pos = value.offset;
                positionDirty = true;
            }
        }

        [DatabaseIgnore]
        public InstanceId lookAtId {
            get => physics.lookAtId;
            set {
                physics.lookAtId = value;
                lookAtDirty = true;
            }
        }

        [DatabaseIgnore]
        public Vector2 lookAtDir {
            get => new Vector2(physics.headingX, physics.headingZ);
            set {
                physics.headingX = value.X;
                physics.headingZ = value.Y;
                lookAtDirty = true;
            }
        }

        public PhysicsDesc.SliderData getSliderValue(uint slider) {
            return physics.sliders[slider];
        }

        public void setSliderValue(uint slider, float value, float velocity) {
            if (physics.sliders == null) {
                physics.sliders = new();
            }

            physics.sliders[slider] = new PhysicsDesc.SliderData {
                value = value,
                velocity = velocity,
            };

            dirtySliders.Add(slider);
        }

        public void broadcastPhysics(double time) {
            if (modeDirty) {
                if (inWorld) {
                    // TODO: Should this be SetModeMsg?
                    playerManager.sendAllVisible(id, new DoModeMsg {
                        senderIdWithStamp = getInstanceIdWithStamp(),
                        modeId = physics.modeId,
                    });
                }

                modeDirty = false;
            }

            if (velScaleDirty) {
                if (inWorld) {
                    // TODO: Should this be SetModeMsg?
                    playerManager.sendAllVisible(id, new SetVelocityScaleMsg {
                        senderIdWithStamp = getInstanceIdWithStamp(),
                        velScale = physics.velScale,
                    });
                }

                velScaleDirty = false;
            }

            if (positionDirty) {
                if (inWorld) {
                    PositionPack posPack = new() {
                        time = time,
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

                    playerManager.sendAllVisibleExcept(id, playerManager.get(id), new PositionMsg {
                        senderIdWithStamp = getInstanceIdWithStamp(),
                        posPack = posPack,
                    });
                }

                positionDirty = false;
            }

            if (lookAtDirty) {
                if (inWorld) {
                    if (physics.lookAtId != InstanceId.NULL) {
                        playerManager.sendAllVisibleExcept(id, playerManager.get(id), new LookAtMsg {
                            senderIdWithStamp = getInstanceIdWithStamp(),
                            targetId = physics.lookAtId,
                        });
                    } else {
                        playerManager.sendAllVisibleExcept(id, playerManager.get(id), new LookAtDirMsg {
                            senderIdWithStamp = getInstanceIdWithStamp(),
                            x = physics.headingX,
                            z = physics.headingZ,
                        });
                    }
                }

                lookAtDirty = false;
            }

            if (inWorld) {
                foreach (uint dirtySlider in dirtySliders) {
                    PhysicsDesc.SliderData sliderValue = physics.sliders[dirtySlider];

                    playerManager.sendAllVisible(id, new DoSliderMsg {
                        senderIdWithStamp = getInstanceIdWithStamp(),
                        sliderId = dirtySlider,
                        newValue = sliderValue.value,
                        time = 0.1f, // TODO: Figure out what time means
                    });
                }
            }

            dirtySliders.Clear();
        }

        public void setParent(WorldObject? parent, HoldingLocation holdLoc = HoldingLocation.INVALID, Orientation holdOrientation = Orientation.DEFAULT) {
            if (parent != null) {
                if (physics.parentId != parent.id) {
                    physics.parentId = parent.id;
                    physics.locationId = holdLoc;
                    physics.parentInstanceStamp = parent.instanceStamp;
                    physics.orientationId = holdOrientation;

                    playerManager.sendAllVisible(id, new ParentMsg {
                        senderIdWithStamp = getInstanceIdWithStamp(),
                        parentIdWithChildPosStamp = parent.getInstanceIdWithStamp(++physics.timestamps[(int)PhysicsTimeStamp.POSITION]),
                        childLocation = physics.locationId,
                        orientationId = physics.orientationId,
                    });
                }
            } else if (physics.parentId != InstanceId.NULL) {
                WorldObject? curParent = objectManager.get(physics.parentId);
                if (curParent != null) {
                    playerManager.sendAllVisible(id, new DeParentMsg {
                        senderIdWithStamp = curParent.getInstanceIdWithStamp(),
                        childIdWithPosStamp = getInstanceIdWithStamp(++physics.timestamps[(int)PhysicsTimeStamp.POSITION]),
                    });
                }

                physics.parentId = InstanceId.NULL;
                physics.locationId = HoldingLocation.INVALID;
                physics.parentInstanceStamp = 0;
                physics.orientationId = Orientation.DEFAULT;
            }
        }
    }
}
