using System;

namespace AC2RE.Definitions {

    public class CameraFX {

        // Const - globals
        [Flags]
        public enum PackFlag : ulong {
            NONE = 0,
            SHAKE = 1 << 0, // SHAKE 0x00000001
            LOOKAT = 1 << 1, // LOOKAT 0x00000002
            FREE_CAMERA = 1 << 2, // FREE_CAMERA 0x00000004
            PARENT_CAMERA = 1 << 3, // PARENT_CAMERA 0x00000008
            TARGET_CAMERA = 1 << 4, // TARGET_CAMERA 0x00000010
            RESTORE_CAMERA = 1 << 5, // RESTORE_CAMERA 0x00000020
            INTENSITY = 1 << 6, // INTENSITY 0x00000040
            PERIOD = 1 << 7, // PERIOD 0x00000080
            RAMP_UP = 1 << 8, // RAMP_UP 0x00000100
            MOTION_BLUR = 1 << 9, // MOTION_BLUR 0x00000200
            SCREEN_FLASH = 1 << 10, // SCREEN_FLASH 0x00000400
            PLAYER_ONLY = 1 << 11, // PLAYER_ONLY 0x00000800
            FOV = 1 << 12, // FOV 0x00001000
            RAMP_DOWN = 1 << 13, // RAMP_DOWN 0x00002000
            DISTANCE = 1 << 14, // DISTANCE 0x00004000
        }

        public PackFlag packFlags;
        public DataId did; // m_DID
        public float intensity; // mIntensity
        public float period; // mPeriod
        public float rampUpTime; // mRampUpTime
        public float rampDownTime; // mRampDownTime
        public bool lookAt; // mLookAt
        public bool freeCamera; // mFreeCamera
        public bool parentCamera; // mParentCamera
        public bool targetCamera; // mTargetCamera
        public bool restoreCamera; // mRestoreCamera
        public bool playerOnly; // mPlayerOnly
        public bool shake; // mShake
        public bool cameraDist; // mCameraDistance
        public bool blur; // mBlur
        public bool hasScreenFlash; // mHasScreenFlash
        public RGBAColor screenFlash; // mScreenFlash
        public bool hasFOV; // mHasFOV

        public CameraFX() {

        }

        public CameraFX(AC2Reader data) {
            did = data.ReadDataId();
            packFlags = (PackFlag)data.ReadUInt64();
            if (packFlags.HasFlag(PackFlag.SHAKE)) {
                shake = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.LOOKAT)) {
                lookAt = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.FREE_CAMERA)) {
                freeCamera = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.PARENT_CAMERA)) {
                parentCamera = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.TARGET_CAMERA)) {
                targetCamera = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.RESTORE_CAMERA)) {
                restoreCamera = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.INTENSITY)) {
                intensity = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.PERIOD)) {
                period = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.RAMP_UP)) {
                rampUpTime = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.RAMP_DOWN)) {
                rampDownTime = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.MOTION_BLUR)) {
                blur = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.FOV)) {
                hasFOV = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.SCREEN_FLASH)) {
                hasScreenFlash = data.ReadBoolean();
                screenFlash = data.ReadRGBAColor();
            }
            if (packFlags.HasFlag(PackFlag.PLAYER_ONLY)) {
                playerOnly = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.DISTANCE)) {
                cameraDist = data.ReadBoolean();
            }
        }

        public void write(AC2Writer data) {
            data.Write(did);
            data.Write((ulong)packFlags);
            if (packFlags.HasFlag(PackFlag.SHAKE)) {
                data.Write(shake);
            }
            if (packFlags.HasFlag(PackFlag.LOOKAT)) {
                data.Write(lookAt);
            }
            if (packFlags.HasFlag(PackFlag.FREE_CAMERA)) {
                data.Write(freeCamera);
            }
            if (packFlags.HasFlag(PackFlag.PARENT_CAMERA)) {
                data.Write(parentCamera);
            }
            if (packFlags.HasFlag(PackFlag.TARGET_CAMERA)) {
                data.Write(targetCamera);
            }
            if (packFlags.HasFlag(PackFlag.RESTORE_CAMERA)) {
                data.Write(restoreCamera);
            }
            if (packFlags.HasFlag(PackFlag.INTENSITY)) {
                data.Write(intensity);
            }
            if (packFlags.HasFlag(PackFlag.PERIOD)) {
                data.Write(period);
            }
            if (packFlags.HasFlag(PackFlag.RAMP_UP)) {
                data.Write(rampUpTime);
            }
            if (packFlags.HasFlag(PackFlag.RAMP_DOWN)) {
                data.Write(rampDownTime);
            }
            if (packFlags.HasFlag(PackFlag.MOTION_BLUR)) {
                data.Write(blur);
            }
            if (packFlags.HasFlag(PackFlag.FOV)) {
                data.Write(hasFOV);
            }
            if (packFlags.HasFlag(PackFlag.SCREEN_FLASH)) {
                data.Write(hasScreenFlash);
                data.Write(screenFlash);
            }
            if (packFlags.HasFlag(PackFlag.PLAYER_ONLY)) {
                data.Write(playerOnly);
            }
            if (packFlags.HasFlag(PackFlag.DISTANCE)) {
                data.Write(cameraDist);
            }
        }
    }
}
