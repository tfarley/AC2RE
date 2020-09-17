using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2E.Def {

    public class FXNode {

        // Const - globals
        [Flags]
        public enum PackFlag : ulong {
            NONE = 0,
            SOUND_LIST = 1 << 0, // 0x00000001
            PARTICLE_LIST = 1 << 1, // 0x00000002
            CAMERA_ID = 1 << 2, // 0x00000004
            APEAR_ID = 1 << 3, // 0x00000008
            APEAR_KEY = 1 << 4, // 0x00000010
            RAMP_TIME = 1 << 5, // 0x00000020
            INTENSITY = 1 << 6, // 0x00000040
            HOLDING_LOCATION = 1 << 7, // 0x00000080
            BONE_NAME = 1 << 8, // 0x00000100
            BONE_OFFSET = 1 << 9, // 0x00000200
            HAS_STREAK = 1 << 10, // 0x00000400
            START_STREAK = 1 << 11, // 0x00000800
            STOP_STREAK = 1 << 12, // 0x00001000
            SLIDER = 1 << 13, // 0x00002000
            RESTORE = 1 << 14, // 0x00004000
            HAS_COLOR = 1 << 15, // 0x00008000
            PROPAGATE_COLOR = 1 << 16, // 0x00010000
            VDESC_ID = 1 << 17, // 0x00020000
            LIFE_TIME = 1 << 18, // 0x00040000
            BEHAVIOR = 1 << 19, // 0x00080000
            PARENT = 1 << 20, // 0x00100000
            UNPARENT = 1 << 21, // 0x00200000
            RETARGET = 1 << 22, // 0x00400000
            DESTROY_TARGET = 1 << 23, // 0x00800000
            TARGET_FX = 1 << 24, // 0x01000000
            DAY_TIME = 1 << 25, // 0x02000000
            NIGHT_TIME = 1 << 26, // 0x04000000
            CHANNEL_HASH = 1 << 27, // 0x08000000
            FIRE_AND_FORGET = 1 << 28, // 0x10000000
            WATER_PLANE = 1 << 29, // 0x20000000
            CONDITIONAL_CLONE = 1 << 30, // 0x40000000
            HAS_DECAL = 1u << 31, // 0x80000000
            HAS_LIGHT = 1ul << 32, // 0x00000001
            HAS_REPULSOR = 1ul << 33, // 0x00000002
            PLAYER_ONLY = 1ul << 34, // 0x00000004
            HIDE_TARGET = 1ul << 35, // 0x00000008
            FX_SCRIPT = 1ul << 36, // 0x00000010
            COLOR_LIFE_TIME = 1ul << 37, // 0x00000020
            REMOVE_ROTATION = 1ul << 38, // 0x00000040
            MUSIC_LIST = 1ul << 39, // 0x00000080
        }

        public PackFlag packFlags;
        public bool dayTime; // m_day_time
        public bool nightTime; // m_night_time
        public bool playerOnly; // m_player_only
        public Dictionary<uint, uint> channelKeys; // m_channel_keys
        public float rampTime; // m_ramp_time
        public List<DataId> soundList; // m_sound_list
        public List<DataId> musicList; // m_music_list
        public bool startStreak; // m_start_streak
        public bool stopStreak; // m_stop_streak
        public StreakFX streak; // m_streak
        public bool hasStreak; // m_has_streak
        public DecalFX decal; // m_decal
        public bool hasDecal; // m_has_decal
        public LightSourceFX light; // m_light
        public bool hasLight; // m_has_light
        public RepulsorFX repulsor; // m_repulsor
        public bool hasRepulsor; // m_has_repulsor
        public DataId fxScriptDid; // m_fx_script_id
        public DataId cameraFxDid; // m_cameraFX_id
        public DataId aprDid; // m_apr_id
        public AppliedAppearanceKey appKey; // m_apr_key
        public DataId sliderDid; // m_slider
        public float intensity; // m_intensity
        public bool restore; // m_restore
        public bool hasColor; // m_has_color
        public RGBAColor color; // m_color
        public float colorLifetime; // m_color_lifetime
        public bool propagateColor; // m_prop_color
        public List<DataId> particleSystemDids; // m_psdesc_list
        public HoldingLocation holdingLocation; // m_holding_location
        public uint boneName; // m_bone_name
        public Vector3 boneOffset; // m_bone_offset
        public bool waterPlane; // m_water_plane
        public bool fireAndForget; // m_fire_and_forget
        public bool removeRot; // m_remove_rotation
        public DataId visualDescDid; // m_vdesc_id
        public float lifetime; // m_lifetime
        public DataId behaviorDid; // m_behavior
        public bool parent; // m_parent
        public bool unparent; // m_unparent
        public bool retarget; // m_retarget
        public bool conditional; // m_conditional
        public bool hideTarget; // m_hide_target
        public bool destroyTarget; // m_destroy_target
        public DataId targetFxDid; // m_target_fx_id

        public FXNode() {

        }

        public FXNode(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt64();
            if (packFlags.HasFlag(PackFlag.SOUND_LIST)) {
                soundList = data.ReadList(data.ReadDataId);
            }
            if (packFlags.HasFlag(PackFlag.MUSIC_LIST)) {
                musicList = data.ReadList(data.ReadDataId);
            }
            if (packFlags.HasFlag(PackFlag.PARTICLE_LIST)) {
                particleSystemDids = data.ReadList(data.ReadDataId);
            }
            if (packFlags.HasFlag(PackFlag.CAMERA_ID)) {
                cameraFxDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.APEAR_ID)) {
                aprDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.APEAR_KEY)) {
                appKey = new AppliedAppearanceKey(data);
            }
            if (packFlags.HasFlag(PackFlag.RAMP_TIME)) {
                rampTime = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.INTENSITY)) {
                intensity = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.HOLDING_LOCATION)) {
                holdingLocation = (HoldingLocation)data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.BONE_NAME)) {
                boneName = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.BONE_OFFSET)) {
                boneOffset = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.HAS_STREAK)) {
                hasStreak = data.ReadBoolean();
                streak = new StreakFX(data);
            }
            if (packFlags.HasFlag(PackFlag.START_STREAK)) {
                startStreak = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.STOP_STREAK)) {
                stopStreak = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.SLIDER)) {
                sliderDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.RESTORE)) {
                restore = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.HAS_COLOR)) {
                hasColor = data.ReadBoolean();
                color = data.ReadRGBAColorFull();
            }
            if (packFlags.HasFlag(PackFlag.PROPAGATE_COLOR)) {
                propagateColor = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.VDESC_ID)) {
                visualDescDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.LIFE_TIME)) {
                lifetime = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.COLOR_LIFE_TIME)) {
                colorLifetime = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.BEHAVIOR)) {
                behaviorDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.PARENT)) {
                parent = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.UNPARENT)) {
                unparent = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.RETARGET)) {
                retarget = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.DESTROY_TARGET)) {
                destroyTarget = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.HIDE_TARGET)) {
                hideTarget = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.TARGET_FX)) {
                targetFxDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.DAY_TIME)) {
                dayTime = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.NIGHT_TIME)) {
                nightTime = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.CHANNEL_HASH)) {
                channelKeys = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
            }
            if (packFlags.HasFlag(PackFlag.FIRE_AND_FORGET)) {
                fireAndForget = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.WATER_PLANE)) {
                waterPlane = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.CONDITIONAL_CLONE)) {
                conditional = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.HAS_DECAL)) {
                hasDecal = data.ReadBoolean();
                decal = new DecalFX(data);
            }
            if (packFlags.HasFlag(PackFlag.HAS_REPULSOR)) {
                hasRepulsor = data.ReadBoolean();
                repulsor = new RepulsorFX(data);
            }
            if (packFlags.HasFlag(PackFlag.HAS_LIGHT)) {
                hasLight = data.ReadBoolean();
                light = new LightSourceFX(data);
            }
            if (packFlags.HasFlag(PackFlag.PLAYER_ONLY)) {
                playerOnly = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.FX_SCRIPT)) {
                fxScriptDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.REMOVE_ROTATION)) {
                removeRot = data.ReadBoolean();
            }
        }

        public void write(AC2Writer data) {
            data.Write((ulong)packFlags);
            if (packFlags.HasFlag(PackFlag.SOUND_LIST)) {
                data.Write(soundList, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.MUSIC_LIST)) {
                data.Write(musicList, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.PARTICLE_LIST)) {
                data.Write(particleSystemDids, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.CAMERA_ID)) {
                data.Write(cameraFxDid);
            }
            if (packFlags.HasFlag(PackFlag.APEAR_ID)) {
                data.Write(aprDid);
            }
            if (packFlags.HasFlag(PackFlag.APEAR_KEY)) {
                appKey.write(data);
            }
            if (packFlags.HasFlag(PackFlag.RAMP_TIME)) {
                data.Write(rampTime);
            }
            if (packFlags.HasFlag(PackFlag.INTENSITY)) {
                data.Write(intensity);
            }
            if (packFlags.HasFlag(PackFlag.HOLDING_LOCATION)) {
                data.Write((uint)holdingLocation);
            }
            if (packFlags.HasFlag(PackFlag.BONE_NAME)) {
                data.Write(boneName);
            }
            if (packFlags.HasFlag(PackFlag.BONE_OFFSET)) {
                data.Write(boneOffset);
            }
            if (packFlags.HasFlag(PackFlag.HAS_STREAK)) {
                data.Write(hasStreak);
                streak.write(data);
            }
            if (packFlags.HasFlag(PackFlag.START_STREAK)) {
                data.Write(startStreak);
            }
            if (packFlags.HasFlag(PackFlag.STOP_STREAK)) {
                data.Write(stopStreak);
            }
            if (packFlags.HasFlag(PackFlag.SLIDER)) {
                data.Write(sliderDid);
            }
            if (packFlags.HasFlag(PackFlag.RESTORE)) {
                data.Write(restore);
            }
            if (packFlags.HasFlag(PackFlag.HAS_COLOR)) {
                data.Write(hasColor);
                data.WriteFull(color);
            }
            if (packFlags.HasFlag(PackFlag.PROPAGATE_COLOR)) {
                data.Write(propagateColor);
            }
            if (packFlags.HasFlag(PackFlag.VDESC_ID)) {
                data.Write(visualDescDid);
            }
            if (packFlags.HasFlag(PackFlag.LIFE_TIME)) {
                data.Write(lifetime);
            }
            if (packFlags.HasFlag(PackFlag.COLOR_LIFE_TIME)) {
                data.Write(colorLifetime);
            }
            if (packFlags.HasFlag(PackFlag.BEHAVIOR)) {
                data.Write(behaviorDid);
            }
            if (packFlags.HasFlag(PackFlag.PARENT)) {
                data.Write(parent);
            }
            if (packFlags.HasFlag(PackFlag.UNPARENT)) {
                data.Write(unparent);
            }
            if (packFlags.HasFlag(PackFlag.RETARGET)) {
                data.Write(retarget);
            }
            if (packFlags.HasFlag(PackFlag.DESTROY_TARGET)) {
                data.Write(destroyTarget);
            }
            if (packFlags.HasFlag(PackFlag.HIDE_TARGET)) {
                data.Write(hideTarget);
            }
            if (packFlags.HasFlag(PackFlag.TARGET_FX)) {
                data.Write(targetFxDid);
            }
            if (packFlags.HasFlag(PackFlag.DAY_TIME)) {
                data.Write(dayTime);
            }
            if (packFlags.HasFlag(PackFlag.NIGHT_TIME)) {
                data.Write(nightTime);
            }
            if (packFlags.HasFlag(PackFlag.CHANNEL_HASH)) {
                data.Write(channelKeys, data.Write, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.FIRE_AND_FORGET)) {
                data.Write(fireAndForget);
            }
            if (packFlags.HasFlag(PackFlag.WATER_PLANE)) {
                data.Write(waterPlane);
            }
            if (packFlags.HasFlag(PackFlag.CONDITIONAL_CLONE)) {
                data.Write(conditional);
            }
            if (packFlags.HasFlag(PackFlag.HAS_DECAL)) {
                data.Write(hasDecal);
                decal.write(data);
            }
            if (packFlags.HasFlag(PackFlag.HAS_REPULSOR)) {
                data.Write(hasRepulsor);
                repulsor.write(data);
            }
            if (packFlags.HasFlag(PackFlag.HAS_LIGHT)) {
                data.Write(hasLight);
                light.write(data);
            }
            if (packFlags.HasFlag(PackFlag.PLAYER_ONLY)) {
                data.Write(playerOnly);
            }
            if (packFlags.HasFlag(PackFlag.FX_SCRIPT)) {
                data.Write(fxScriptDid);
            }
            if (packFlags.HasFlag(PackFlag.REMOVE_ROTATION)) {
                data.Write(removeRot);
            }
        }
    }
}
