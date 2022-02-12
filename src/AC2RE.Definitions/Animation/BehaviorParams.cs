using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2RE.Definitions;

public class BehaviorParams : IHeapObject {

    public NativeType nativeType => NativeType.BehaviorParams;

    // Enum BehaviorRepPack::Flags
    [Flags]
    public enum PackFlag : uint {
        NONE = 0,
        BEHAVIOR_ID = 1 << 0, // BEHAVIOR_ID 0x00000001
        MODE_ID = 1 << 1, // MODE_ID 0x00000002
        TIME_SCALE = 1 << 2, // TIME_SCALE 0x00000004
        EARLYCALLBACK = 1 << 3, // EARLYCALLBACK 0x00000008
        HOLDCYLE = 1 << 4, // HOLDCYLE 0x00000010
        MOVETOCANCELS = 1 << 5, // MOVETOCANCELS 0x00000020
        TARGETED_CAMERA = 1 << 6, // TARGETED_CAMERA 0x00000040
        PARENTED_CAMERA = 1 << 7, // PARENTED_CAMERA 0x00000080
        FADE_CHILDREN = 1 << 8, // FADE_CHILDREN 0x00000100
        SEND_EVENT = 1 << 9, // SEND_EVENT 0x00000200
        PROPAGATE = 1 << 10, // PROPAGATE 0x00000400
        LOCKACTIONS = 1 << 11, // LOCKACTIONS 0x00000800
        FXSCRIPT = 1 << 12, // FXSCRIPT 0x00001000
        TARGET = 1 << 13, // TARGET 0x00002000
        IMPULSE = 1 << 14, // IMPULSE 0x00004000
        SELF_DESTRUCT = 1 << 15, // SELF_DESTRUCT 0x00008000
        VDESC = 1 << 16, // VDESC 0x00010000
        LEAVE_IDLE = 1 << 17, // LEAVE_IDLE 0x00020000
        HOLD_CAMERA = 1 << 18, // HOLD_CAMERA 0x00040000
        RESTORE_CAMERA = 1 << 19, // RESTORE_CAMERA 0x00080000
        FXTODO = 1 << 20, // FXTODO 0x00100000
        NEW_CAMERA_OBJ = 1 << 21, // NEW_CAMERA_OBJ 0x00200000
        CONTEXTID = 1 << 22, // CONTEXTID 0x00400000
        WEENIE_EMOTE_ID = 1 << 23, // WEENIE_EMOTE_ID 0x00800000
    }

    public PackFlag packFlags;
    public BehaviorId behaviorId; // mBehaviorID
    public float timeScale; // mTimeScale
    public ModeId modeId; // mModeID
    public uint holdCycles; // mHoldCycles
    public DataId fxScriptId; // mFXScriptID
    public FxId fxId; // mFxID
    public InstanceId targetId; // mTargetID
    public InstanceId cameraTargetId; // mCameraTargetID
    public uint cameraBehavior; // mCameraBehavior
    public DataId visualDescToClone; // mVDescToClone
    public DataId clonedAprDid; // m_clonedAprID
    public Dictionary<AppearanceKey, float> clonedAppHash; // m_clonedAppAprHash
    public Vector3 impulse; // mImpulse
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

    public BehaviorParams() {

    }

    public BehaviorParams(AC2Reader data) {
        packFlags = data.ReadEnum<PackFlag>();
        if (packFlags.HasFlag(PackFlag.BEHAVIOR_ID)) {
            behaviorId = data.ReadEnum<BehaviorId>();
        }
        if (packFlags.HasFlag(PackFlag.TIME_SCALE)) {
            timeScale = data.ReadSingle();
        }
        if (packFlags.HasFlag(PackFlag.MODE_ID)) {
            modeId = data.ReadEnum<ModeId>();
        }
        if (packFlags.HasFlag(PackFlag.HOLDCYLE)) {
            holdCycles = data.ReadUInt32();
        }
        if (packFlags.HasFlag(PackFlag.FXSCRIPT)) {
            fxScriptId = data.ReadDataId();
        }
        if (packFlags.HasFlag(PackFlag.FXTODO)) {
            fxId = data.ReadEnum<FxId>();
        }
        if (packFlags.HasFlag(PackFlag.TARGET)) {
            targetId = data.ReadInstanceId();
        }
        if (packFlags.HasFlag(PackFlag.NEW_CAMERA_OBJ)) {
            cameraTargetId = data.ReadInstanceId();
            cameraBehavior = data.ReadUInt32();
        }
        if (packFlags.HasFlag(PackFlag.VDESC)) {
            visualDescToClone = data.ReadDataId();
            clonedAprDid = data.ReadDataId();
            clonedAppHash = data.ReadDictionary(data.ReadEnum<AppearanceKey>, data.ReadSingle);
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
        data.WriteEnum(packFlags);
        if (packFlags.HasFlag(PackFlag.BEHAVIOR_ID)) {
            data.WriteEnum(behaviorId);
        }
        if (packFlags.HasFlag(PackFlag.TIME_SCALE)) {
            data.Write(timeScale);
        }
        if (packFlags.HasFlag(PackFlag.MODE_ID)) {
            data.WriteEnum(modeId);
        }
        if (packFlags.HasFlag(PackFlag.HOLDCYLE)) {
            data.Write(holdCycles);
        }
        if (packFlags.HasFlag(PackFlag.FXSCRIPT)) {
            data.Write(fxScriptId);
        }
        if (packFlags.HasFlag(PackFlag.FXTODO)) {
            data.WriteEnum(fxId);
        }
        if (packFlags.HasFlag(PackFlag.TARGET)) {
            data.Write(targetId);
        }
        if (packFlags.HasFlag(PackFlag.NEW_CAMERA_OBJ)) {
            data.Write(cameraTargetId);
            data.Write(cameraBehavior);
        }
        if (packFlags.HasFlag(PackFlag.VDESC)) {
            data.Write(visualDescToClone);
            data.Write(clonedAprDid);
            data.Write(clonedAppHash, v => data.Write((uint)v), data.Write);
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
