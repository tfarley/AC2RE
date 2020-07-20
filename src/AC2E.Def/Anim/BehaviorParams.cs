using System;
using System.Collections.Generic;

namespace AC2E.Def {

    public class BehaviorParams {

        // Const - globals
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,
            BEHAVIOR_ID = 1 << 0, // 0x00000001
            MODE_ID = 1 << 1, // 0x00000002
            TIME_SCALE = 1 << 2, // 0x00000004
            EARLYCALLBACK = 1 << 3, // 0x00000008
            HOLDCYLE = 1 << 4, // 0x00000010
            MOVETOCANCELS = 1 << 5, // 0x00000020
            TARGETED_CAMERA = 1 << 6, // 0x00000040
            PARENTED_CAMERA = 1 << 7, // 0x00000080
            FADE_CHILDREN = 1 << 8, // 0x00000100
            SEND_EVENT = 1 << 9, // 0x00000200
            PROPAGATE = 1 << 10, // 0x00000400
            LOCKACTIONS = 1 << 11, // 0x00000800
            FXSCRIPT = 1 << 12, // 0x00001000
            TARGET = 1 << 13, // 0x00002000
            IMPULSE = 1 << 14, // 0x00004000
            SELF_DESTRUCT = 1 << 15, // 0x00008000
            VDESC = 1 << 16, // 0x00010000
            LEAVE_IDLE = 1 << 17, // 0x00020000
            HOLD_CAMERA = 1 << 18, // 0x00040000
            RESTORE_CAMERA = 1 << 19, // 0x00080000
            FXTODO = 1 << 20, // 0x00100000
            NEW_CAMERA_OBJ = 1 << 21, // 0x00200000
            CONTEXTID = 1 << 22, // 0x00400000
            WEENIE_EMOTE_ID = 1 << 23, // 0x00800000
        }

        public PackFlag packFlags;
        public uint behaviorId; // mBehaviorID
        public float timeScale; // mTimeScale
        public uint modeID; // mModeID
        public uint holdCycles; // mHoldCycles
        public uint fxScriptId; // mFXScriptID
        public uint fxId; // mFxID
        public InstanceId targetId; // mTargetID
        public InstanceId cameraTargetId; // mCameraTargetID
        public uint cameraBehavior; // mCameraBehavior
        public uint vDescToClone; // mVDescToClone
        public uint clonedAprId; // m_clonedAprID
        public Dictionary<uint, float> clonedAppAprHash; // m_clonedAppAprHash
        public Vector impulse; // mImpulse
        public bool earlyCallback; // mEarlyCallBack
        public bool moveToCancels; // mMoveToCancels
        public bool cameraParent; // mCameraParent
        public bool cameraTarget; // mCameraTarget
        public bool cameraHold; // mCameraHold
        public bool cameraRestore; // mCameraRestore
        public bool fadeChildren; // mFadeChildren
        public bool propagate; // mPropagate
        public bool lockActions; // mLockActions
        public bool sendEvent; // mSendEvent
        public uint weenieEmoteId; // mWeenieEmoteID
        public bool leaveIdleAlone; // mLeaveIdleAlone
        public bool destroyOnCompletion; // mDestroyOnCompletion
        public uint contextId; // mContextID

        public BehaviorParams(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            if (packFlags.HasFlag(PackFlag.BEHAVIOR_ID)) {
                behaviorId = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.TIME_SCALE)) {
                timeScale = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.MODE_ID)) {
                modeID = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.HOLDCYLE)) {
                holdCycles = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.FXSCRIPT)) {
                fxScriptId = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.FXTODO)) {
                fxId = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.TARGET)) {
                targetId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.NEW_CAMERA_OBJ)) {
                cameraTargetId = data.ReadInstanceId();
                cameraBehavior = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.VDESC)) {
                vDescToClone = data.ReadUInt32();
                clonedAprId = data.ReadUInt32();
                clonedAppAprHash = data.ReadDictionary(data.ReadUInt32, data.ReadSingle);
            }
            if (packFlags.HasFlag(PackFlag.IMPULSE)) {
                impulse = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.EARLYCALLBACK)) {
                earlyCallback = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.MOVETOCANCELS)) {
                moveToCancels = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.PARENTED_CAMERA)) {
                cameraParent = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.TARGETED_CAMERA)) {
                cameraTarget = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.HOLD_CAMERA)) {
                cameraHold = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.RESTORE_CAMERA)) {
                cameraRestore = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.FADE_CHILDREN)) {
                fadeChildren = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.PROPAGATE)) {
                propagate = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.LOCKACTIONS)) {
                lockActions = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.SEND_EVENT)) {
                sendEvent = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.LEAVE_IDLE)) {
                leaveIdleAlone = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.SELF_DESTRUCT)) {
                destroyOnCompletion = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.WEENIE_EMOTE_ID)) {
                weenieEmoteId = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.CONTEXTID)) {
                contextId = data.ReadUInt32();
            }
        }

        public void write(AC2Writer data) {
            data.Write((uint)packFlags);
            if (packFlags.HasFlag(PackFlag.BEHAVIOR_ID)) {
                data.Write(behaviorId);
            }
            if (packFlags.HasFlag(PackFlag.TIME_SCALE)) {
                data.Write(timeScale);
            }
            if (packFlags.HasFlag(PackFlag.MODE_ID)) {
                data.Write(modeID);
            }
            if (packFlags.HasFlag(PackFlag.HOLDCYLE)) {
                data.Write(holdCycles);
            }
            if (packFlags.HasFlag(PackFlag.FXSCRIPT)) {
                data.Write(fxScriptId);
            }
            if (packFlags.HasFlag(PackFlag.FXTODO)) {
                data.Write(fxId);
            }
            if (packFlags.HasFlag(PackFlag.TARGET)) {
                data.Write(targetId);
            }
            if (packFlags.HasFlag(PackFlag.NEW_CAMERA_OBJ)) {
                data.Write(cameraTargetId);
                data.Write(cameraBehavior);
            }
            if (packFlags.HasFlag(PackFlag.VDESC)) {
                data.Write(vDescToClone);
                data.Write(clonedAprId);
                data.Write(clonedAppAprHash, data.Write, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.IMPULSE)) {
                data.Write(impulse);
            }
            if (packFlags.HasFlag(PackFlag.EARLYCALLBACK)) {
                data.Write(earlyCallback);
            }
            if (packFlags.HasFlag(PackFlag.MOVETOCANCELS)) {
                data.Write(moveToCancels);
            }
            if (packFlags.HasFlag(PackFlag.PARENTED_CAMERA)) {
                data.Write(cameraParent);
            }
            if (packFlags.HasFlag(PackFlag.TARGETED_CAMERA)) {
                data.Write(cameraTarget);
            }
            if (packFlags.HasFlag(PackFlag.HOLD_CAMERA)) {
                data.Write(cameraHold);
            }
            if (packFlags.HasFlag(PackFlag.RESTORE_CAMERA)) {
                data.Write(cameraRestore);
            }
            if (packFlags.HasFlag(PackFlag.FADE_CHILDREN)) {
                data.Write(fadeChildren);
            }
            if (packFlags.HasFlag(PackFlag.PROPAGATE)) {
                data.Write(propagate);
            }
            if (packFlags.HasFlag(PackFlag.LOCKACTIONS)) {
                data.Write(lockActions);
            }
            if (packFlags.HasFlag(PackFlag.SEND_EVENT)) {
                data.Write(sendEvent);
            }
            if (packFlags.HasFlag(PackFlag.LEAVE_IDLE)) {
                data.Write(leaveIdleAlone);
            }
            if (packFlags.HasFlag(PackFlag.SELF_DESTRUCT)) {
                data.Write(destroyOnCompletion);
            }
            if (packFlags.HasFlag(PackFlag.WEENIE_EMOTE_ID)) {
                data.Write(weenieEmoteId);
            }
            if (packFlags.HasFlag(PackFlag.CONTEXTID)) {
                data.Write(contextId);
            }
        }
    }
}
