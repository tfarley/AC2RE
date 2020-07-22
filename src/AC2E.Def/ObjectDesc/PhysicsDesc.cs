﻿using System;
using System.Collections.Generic;

namespace AC2E.Def {

    public class PhysicsDesc {

        public class SliderData {

            public float value; // m_value
            public float velocity; // m_velocity

            public SliderData() {

            }

            public SliderData(AC2Reader data) {
                value = data.ReadSingle();
                velocity = data.ReadSingle();
            }

            public void write(AC2Writer data) {
                data.Write(value);
                data.Write(velocity);
            }
        }

        // Enum PhysicsDesc::PhysicsDescInfo
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,
            BEHAVIORS = 1 << 0, // 0x00000001
            SLIDERS = 1 << 1, // 0x00000002
            VELOCITY = 1 << 2, // 0x00000004
            ACCELERATION = 1 << 3, // 0x00000008
            OMEGA = 1 << 4, // 0x00000010
            PARENT = 1 << 5, // 0x00000020

            MODE = 1 << 7, // 0x00000080
            ORIENTATION = 1 << 8, // 0x00000100
            FX = 1 << 9, // 0x00000200
            TARGET_ID = 1 << 10, // 0x00000400
            TARGET_POS = 1 << 11, // 0x00000800
            TARGET_OFFSET = 1 << 12, // 0x00001000
            TARGET_HEIGHT = 1 << 13, // 0x00002000
            TARGET_SCALE = 1 << 14, // 0x00004000
            POSITION = 1 << 15, // 0x00008000

            ANIMFRAME_ID = 1 << 17, // 0x00020000

            MISSILE_MOVING = 1 << 19, // 0x00080000
            MISSILE_ACTIVATED = 1 << 20, // 0x00100000

            EXTERNAL_ACL = 1 << 22, // 0x00400000
            VELOCITY_SCALE = 1 << 23, // 0x00800000
            JUMP_SCALE = 1 << 24, // 0x01000000
            LOOKAT_ID = 1 << 25, // 0x02000000
            HEAD_X = 1 << 26, // 0x04000000
            HEAD_Z = 1 << 27, // 0x08000000
        }

        public PackFlag packFlags; // bitfield
        public uint animframeId; // animframe_id
        public Position pos; // pos
        public Vector vel; // m_velocity
        public Vector accel; // m_acceleration
        public Vector omega; // m_omega
        public Vector externalAccel; // m_external_acl
        public float velScale; // m_velocity_scale
        public float jumpScale; // m_jump_scale
        public InstanceId lookAtId; // m_lookAt_id
        public float headingX; // m_headx
        public float headingZ; // m_headz
        public InstanceId targetId; // target_id
        public Position targetPos; // target_pos
        public Vector targetOffset; // target_offset
        public uint targetHeight; // target_height
        public bool useTargetScale; // use_target_scale
        public bool missileIsActivated; // m_missile_is_activated
        public bool missileIsMoving; // m_missile_is_moving
        public InstanceId parentId; // m_parent_id
        public ushort parentInstanceStamp; // m_parent_instance_stamp
        public uint locationId; // m_location_id
        public uint orientationId; // m_orientation_id
        public uint modeId; // m_mode_id
        public List<BehaviorParams> behaviors; // m_behaviors
        public Dictionary<uint, SliderData> sliders; // m_sliders
        public Dictionary<uint, FXScalarAndTarget> fx; // m_fx
        public ushort[] timestamps = new ushort[4]; // timestamps
        public ushort instanceStamp; // m_instance_stamp
        public ushort visualOrderingStamp; // m_visual_ordering_stamp

        public PhysicsDesc() {

        }

        public PhysicsDesc(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            instanceStamp = data.ReadUInt16();
            visualOrderingStamp = data.ReadUInt16();
            if (packFlags.HasFlag(PackFlag.MODE)) {
                modeId = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.BEHAVIORS)) {
                behaviors = data.ReadList(() => new BehaviorParams(data));
            }
            if (packFlags.HasFlag(PackFlag.SLIDERS)) {
                sliders = data.ReadStlMap(data.ReadUInt32, () => new SliderData(data));
            }
            if (packFlags.HasFlag(PackFlag.ANIMFRAME_ID)) {
                animframeId = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.POSITION)) {
                pos = new Position(data);
            }
            if (packFlags.HasFlag(PackFlag.PARENT)) {
                parentId = data.ReadInstanceId();
                locationId = data.ReadUInt32();
                parentInstanceStamp = data.ReadUInt16();
                data.Align(4);
            }
            if (packFlags.HasFlag(PackFlag.ORIENTATION)) {
                orientationId = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.VELOCITY)) {
                vel = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.EXTERNAL_ACL)) {
                externalAccel = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.VELOCITY_SCALE)) {
                velScale = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.JUMP_SCALE)) {
                jumpScale = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.ACCELERATION)) {
                accel = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.OMEGA)) {
                omega = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.LOOKAT_ID)) {
                lookAtId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.HEAD_X)) {
                headingX = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.HEAD_Z)) {
                headingZ = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.TARGET_ID)) {
                targetId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.TARGET_POS)) {
                targetPos = new Position(data);
            }
            if (packFlags.HasFlag(PackFlag.TARGET_OFFSET)) {
                targetOffset = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.FX)) {
                fx = data.ReadStlMap(data.ReadUInt32, () => new FXScalarAndTarget(data));
            }
            missileIsActivated = packFlags.HasFlag(PackFlag.MISSILE_ACTIVATED);
            missileIsMoving = packFlags.HasFlag(PackFlag.MISSILE_MOVING);
            useTargetScale = packFlags.HasFlag(PackFlag.TARGET_SCALE);
            for (int i = 0; i < timestamps.Length; i++) {
                timestamps[i] = data.ReadUInt16();
            }
            data.Align(4);
        }

        public void write(AC2Writer data) {
            data.Write((uint)packFlags);
            data.Write(instanceStamp);
            data.Write(visualOrderingStamp);
            if (packFlags.HasFlag(PackFlag.MODE)) {
                data.Write(modeId);
            }
            if (packFlags.HasFlag(PackFlag.BEHAVIORS)) {
                data.Write(behaviors, v => v.write(data));
            }
            if (packFlags.HasFlag(PackFlag.SLIDERS)) {
                data.WriteStlMap(sliders, data.Write, v => v.write(data));
            }
            if (packFlags.HasFlag(PackFlag.ANIMFRAME_ID)) {
                data.Write(animframeId);
            }
            if (packFlags.HasFlag(PackFlag.POSITION)) {
                pos.write(data);
            }
            if (packFlags.HasFlag(PackFlag.PARENT)) {
                data.Write(parentId);
                data.Write(locationId);
                data.Write(parentInstanceStamp);
                data.Align(4);
            }
            if (packFlags.HasFlag(PackFlag.ORIENTATION)) {
                data.Write(orientationId);
            }
            if (packFlags.HasFlag(PackFlag.VELOCITY)) {
                data.Write(vel);
            }
            if (packFlags.HasFlag(PackFlag.EXTERNAL_ACL)) {
                data.Write(externalAccel);
            }
            if (packFlags.HasFlag(PackFlag.VELOCITY_SCALE)) {
                data.Write(velScale);
            }
            if (packFlags.HasFlag(PackFlag.JUMP_SCALE)) {
                data.Write(jumpScale);
            }
            if (packFlags.HasFlag(PackFlag.ACCELERATION)) {
                data.Write(accel);
            }
            if (packFlags.HasFlag(PackFlag.OMEGA)) {
                data.Write(omega);
            }
            if (packFlags.HasFlag(PackFlag.LOOKAT_ID)) {
                data.Write(lookAtId);
            }
            if (packFlags.HasFlag(PackFlag.HEAD_X)) {
                data.Write(headingX);
            }
            if (packFlags.HasFlag(PackFlag.HEAD_Z)) {
                data.Write(headingZ);
            }
            if (packFlags.HasFlag(PackFlag.TARGET_ID)) {
                data.Write(targetId);
            }
            if (packFlags.HasFlag(PackFlag.TARGET_POS)) {
                targetPos.write(data);
            }
            if (packFlags.HasFlag(PackFlag.TARGET_OFFSET)) {
                data.Write(targetOffset);
            }
            if (packFlags.HasFlag(PackFlag.FX)) {
                data.WriteStlMap(fx, data.Write, v => v.write(data));
            }
            for (int i = 0; i < timestamps.Length; i++) {
                data.Write(timestamps[i]);
            }
            data.Align(4);
        }
    }
}