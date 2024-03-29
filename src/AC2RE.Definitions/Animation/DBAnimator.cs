﻿using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace AC2RE.Definitions;

public class DBAnimator {

    public class AnimAsset : IWritable {

        public class AnimAssetScaleKey : IWritable {

            // AnimAssetScaleKey
            public Vector3 scale; // scale
            public float time; // sTime

            public AnimAssetScaleKey() {

            }

            public AnimAssetScaleKey(AC2Reader data) {
                scale = data.ReadVector();
                time = data.ReadSingle();
            }

            public void write(AC2Writer data) {
                data.Write(scale);
                data.Write(time);
            }
        }

        public class AnimAssetOffsetKey : IWritable {

            // AnimAssetOffsetKey
            public Vector3 offset; // offset
            public float time; // sTime

            public AnimAssetOffsetKey() {

            }

            public AnimAssetOffsetKey(AC2Reader data) {
                offset = data.ReadVector();
                time = data.ReadSingle();
            }

            public void write(AC2Writer data) {
                data.Write(offset);
                data.Write(time);
            }
        }

        public class AnimAssetQuatKey : IWritable {

            // AnimAssetQuatKey
            public Quaternion rot; // qt
            public float time; // sTime

            public AnimAssetQuatKey() {

            }

            public AnimAssetQuatKey(AC2Reader data) {
                rot = data.ReadQuaternion();
                time = data.ReadSingle();
            }

            public void write(AC2Writer data) {
                data.Write(rot);
                data.Write(time);
            }
        }

        public class AnimAssetWeightKey : IWritable {

            // AnimAssetWeightKey
            public float weight; // weight
            public float time; // sTime

            public AnimAssetWeightKey() {

            }

            public AnimAssetWeightKey(AC2Reader data) {
                weight = data.ReadSingle();
                time = data.ReadSingle();
            }

            public void write(AC2Writer data) {
                data.Write(weight);
                data.Write(time);
            }
        }

        public class BoneInfo : IWritable {

            public List<AnimAssetScaleKey> scaleKeys; // m_scaleKeys
            public List<AnimAssetOffsetKey> offsetKeys; // m_offsetKeys
            public List<AnimAssetQuatKey> rotKeys; // m_quatKeys
            public List<AnimAssetWeightKey> weightKeys; // m_weightKeys

            public BoneInfo() {

            }

            public BoneInfo(AC2Reader data) {
                scaleKeys = data.ReadList(() => new AnimAssetScaleKey(data));
                offsetKeys = data.ReadList(() => new AnimAssetOffsetKey(data));
                rotKeys = data.ReadList(() => new AnimAssetQuatKey(data));
                weightKeys = data.ReadList(() => new AnimAssetWeightKey(data));
            }

            public void write(AC2Writer data) {
                data.Write(scaleKeys, v => v.write(data));
                data.Write(offsetKeys, v => v.write(data));
                data.Write(rotKeys, v => v.write(data));
                data.Write(weightKeys, v => v.write(data));
            }
        }

        public class AnimInfo : IWritable {

            // AnimInfo
            public List<Trigger> fxTriggers; // mEffectsTriggers
            public List<WeenieHook> weenieHooks; // mWeenieHooks
            public List<ImpulseTrigger> impulseTriggers; // mImpulseTriggers
            public bool hasHoldLoop; // mHasHoldLoop
            public float loopStart; // mLoopStart
            public float loopEnd; // mLoopEnd

            public AnimInfo() {

            }

            public AnimInfo(AC2Reader data) {
                fxTriggers = data.ReadList(() => new Trigger(data));
                weenieHooks = data.ReadList(() => new WeenieHook(data));
                impulseTriggers = data.ReadList(() => new ImpulseTrigger(data));
                hasHoldLoop = data.ReadBoolean();
                loopStart = data.ReadSingle();
                loopEnd = data.ReadSingle();
            }

            public void write(AC2Writer data) {
                data.Write(fxTriggers, v => v.write(data));
                data.Write(weenieHooks, v => v.write(data));
                data.Write(impulseTriggers, v => v.write(data));
                data.Write(hasHoldLoop);
                data.Write(loopStart);
                data.Write(loopEnd);
            }
        }

        // AnimAsset
        public float period; // period
        public List<BoneInfo> boneInfos; // numBones + m_scaleKeys + m_offsetKeys + m_quatKeys + m_weightKeys
        public AnimInfo animInfo; // animInfo

        public AnimAsset() {

        }

        public AnimAsset(AC2Reader data) {
            period = data.ReadSingle();
            boneInfos = data.ReadList(() => new BoneInfo(data));
            bool hasAnimInfo = data.ReadBoolean();
            if (hasAnimInfo) {
                animInfo = new(data);
            }
        }

        public void write(AC2Writer data) {
            data.Write(period);
            data.Write(boneInfos, v => v.write(data));
            bool hasAnimInfo = animInfo != null;
            data.Write(hasAnimInfo);
            if (hasAnimInfo) {
                animInfo.write(data);
            }
        }
    }

    public class AnimCube : IWritable {

        public class SliderAndTickList : IWritable {

            // AnimCube::SliderAndTickList
            public uint slider; // mSlider
            public List<float> tickList; // mTickList

            public SliderAndTickList() {

            }

            public SliderAndTickList(AC2Reader data) {
                slider = data.ReadUInt32();
                tickList = data.ReadList(data.ReadSingle);
            }

            public void write(AC2Writer data) {
                data.Write(slider);
                data.Write(tickList, data.Write);
            }
        }

        // AnimCube
        public List<SliderAndTickList> sliderData; // mSliderData
        public List<DataId> dbAnimatorDids; // mDBAnimatorIDs

        public AnimCube() {

        }

        public AnimCube(AC2Reader data) {
            sliderData = data.ReadList(() => new SliderAndTickList(data));
            dbAnimatorDids = data.ReadList(data.ReadDataId);
        }

        public void write(AC2Writer data) {
            data.Write(sliderData, v => v.write(data));
            data.Write(dbAnimatorDids, data.Write);
        }
    }

    public class AMHuman : IWritable {

        // AMHuman
        public DataId dbAnimatorDid; // dbAnimatorID

        public AMHuman() {

        }

        public AMHuman(AC2Reader data) {
            dbAnimatorDid = data.ReadDataId();
        }

        public void write(AC2Writer data) {
            data.Write(dbAnimatorDid);
        }
    }

    public class AxisCube : IWritable {

        // AxisCube
        public List<uint> sliderIds; // SlidersIDs
        public List<DataId> dbAnimatorDids; // dbAnimatorIDs
        public uint originId; // originID

        public AxisCube() {

        }

        public AxisCube(AC2Reader data) {
            originId = data.ReadUInt32();
            dbAnimatorDids = data.ReadList(data.ReadDataId);
            sliderIds = data.ReadList(data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(originId);
            data.Write(dbAnimatorDids, data.Write);
            data.Write(sliderIds, data.Write);
        }
    }

    public class AnimInterpolator : IWritable {

        // AnimInterpolator
        public List<DataId> dbAnimatorDids; // dbAnimatorIDs
        public uint slider; // mSlider

        public AnimInterpolator() {

        }

        public AnimInterpolator(AC2Reader data) {
            slider = data.ReadUInt32();
            dbAnimatorDids = data.ReadList(data.ReadDataId);
        }

        public void write(AC2Writer data) {
            data.Write(slider);
            data.Write(dbAnimatorDids, data.Write);
        }
    }

    public class AMRandom : IWritable {

        // AMRandom
        public AnimInterpolator interpolator; // mInterp
        public float variation; // mVariation
        public float frequency; // mFrequency
        public float rampTime; // mRampTime
        public uint slider; // mVariation

        public AMRandom() {

        }

        public AMRandom(AC2Reader data) {
            interpolator = new(data);
            variation = data.ReadSingle();
            frequency = data.ReadSingle();
            rampTime = data.ReadSingle();
            slider = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            interpolator.write(data);
            data.Write(variation);
            data.Write(frequency);
            data.Write(rampTime);
            data.Write(slider);
        }
    }

    public class AMSwitchME : IWritable {

        // Enum AMSwitchME::SwitchType
        public enum SwitchType : uint {
            mode, // mode
            element, // element
            terrain, // terrain
        }

        // AMSwitchME
        public SwitchType switchType; // mSwitchType
        public uint defaultVal; // mDefault
        public Dictionary<uint, uint> overrides; // mOverrides

        public AMSwitchME() {

        }

        public AMSwitchME(AC2Reader data) {
            defaultVal = data.ReadUInt32();
            switchType = data.ReadEnum<SwitchType>();
            overrides = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(defaultVal);
            data.WriteEnum(switchType);
            data.Write(overrides, data.Write, data.Write);
        }
    }

    public class AMAngle : IWritable {

        public class BoneAngleMod : IWritable {

            // BoneAngleMod
            public uint bone; // mBone
            public float x; // x
            public float y; // y

            public BoneAngleMod() {

            }

            public BoneAngleMod(AC2Reader data) {
                bone = data.ReadUInt32();
                x = data.ReadSingle();
                y = data.ReadSingle();
            }

            public void write(AC2Writer data) {
                data.Write(bone);
                data.Write(x);
                data.Write(y);
            }
        }

        // AMAngle
        public List<BoneAngleMod> boneAngleMods; // mBoneList
        public float minX; // mMinX
        public float maxX; // mMaxX
        public float minY; // mMinY
        public float maxY; // mMaxY

        public AMAngle() {

        }

        public AMAngle(AC2Reader data) {
            boneAngleMods = data.ReadList(() => new BoneAngleMod(data));
            maxX = data.ReadSingle();
            minX = data.ReadSingle();
            maxY = data.ReadSingle();
            minY = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(boneAngleMods, v => v.write(data));
            data.Write(maxX);
            data.Write(minX);
            data.Write(maxY);
            data.Write(minY);
        }
    }

    // Const - globals
    public enum AnimatorType : uint {
        undef = 0,

        asset = 0x40000001, // asset
        cube = 0x40000002, // cube
        human = 0x40000003, // human

        look = 0x40000005, // look
        axis = 0x40000006, // axis
        random = 0x40000007, // random
        switchME = 0x40000008, // switchME

        angle = 0x4000000A, // angle
    }

    // DBAnimator
    public DataId did; // m_DID
    public AnimatorType type;
    public IWritable animatorData;

    public DBAnimator() {

    }

    public DBAnimator(AC2Reader data) {
        did = data.ReadDataId();
        type = data.ReadEnum<AnimatorType>();
        switch (type) {
            case AnimatorType.asset:
                animatorData = new AnimAsset(data);
                break;
            case AnimatorType.cube:
                animatorData = new AnimCube(data);
                break;
            case AnimatorType.human:
                animatorData = new AMHuman(data);
                break;
            case AnimatorType.look:
                break;
            case AnimatorType.axis:
                animatorData = new AxisCube(data);
                break;
            case AnimatorType.random:
                animatorData = new AMRandom(data);
                break;
            case AnimatorType.switchME:
                animatorData = new AMSwitchME(data);
                break;
            case AnimatorType.angle:
                animatorData = new AMAngle(data);
                break;
            default:
                throw new InvalidDataException(type.ToString());
        }
    }

    public void write(AC2Writer data) {
        data.Write(did);
        data.WriteEnum(type);
        if (animatorData != null) {
            animatorData.write(data);
        }
    }
}
