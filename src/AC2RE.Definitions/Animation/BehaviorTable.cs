using System.Collections.Generic;

namespace AC2RE.Definitions;

public class BehaviorTable {

    public struct ComboKey {

        // ComboKey
        public uint elementId; // elementID
        public uint modeId; // modeID
        public uint behaviorId; // behaviorID

        public ComboKey(AC2Reader data) {
            elementId = data.ReadUInt32();
            modeId = data.ReadUInt32();
            behaviorId = data.ReadUInt32();
        }
    }

    public struct ComboInfo {

        // ComboInfo
        public uint animKey; // animKey
        public uint layerNum; // layerNum

        public ComboInfo(AC2Reader data) {
            animKey = data.ReadUInt32();
            layerNum = data.ReadUInt32();
        }
    }

    public struct ModeTransitionKey {

        // ModeTransitionKey
        public uint elementSrcId; // elementSrcID
        public uint modeSrcId; // modeSrcID
        public uint elementDstId; // elementDstID
        public uint modeDstId; // modeDstID

        public ModeTransitionKey(AC2Reader data) {
            elementSrcId = data.ReadUInt32();
            modeSrcId = data.ReadUInt32();
            elementDstId = data.ReadUInt32();
            modeDstId = data.ReadUInt32();
        }
    }

    public struct TransitionInfo {

        // TransitionInfo
        public uint behavior; // mBehavior
        public uint effect; // mEffect
        public uint stopEffect; // mStopEffect

        public TransitionInfo(AC2Reader data) {
            behavior = data.ReadUInt32();
            effect = data.ReadUInt32();
            stopEffect = data.ReadUInt32();
        }
    }

    public struct BehaviorInfo {

        // BehaviorInfo
        public float rampTime; // rampTime
        public uint effect; // effect

        public BehaviorInfo(AC2Reader data) {
            rampTime = data.ReadSingle();
            effect = data.ReadUInt32();
        }
    }

    // BehaviorTable
    public DataId did; // m_DID
    public Dictionary<ModeTransitionKey, float> modeTransitionRampTimes; // mModeTransitionRampTimes
    public Dictionary<ComboKey, ComboInfo> comboLookup; // mComboLookup
    public Dictionary<ModeTransitionKey, TransitionInfo> transitionLookup; // mTransitionLookup
    public List<uint> layers; // mLayers
    public List<uint> tweekLayers; // mTweekLayers
    public Dictionary<ModeStateKey, List<ModeStateKey>> modeTransitionTable; // mModeTransitionTable
    public Dictionary<ModeStateKey, uint> modeList; // mModeList
    public Dictionary<uint, BehaviorInfo> behaviorInfo; // mBehaviorInfo
    public List<uint> initBehaviors; // mInitBehaviors
    public uint initMode; // mInitMode

    public BehaviorTable(AC2Reader data) {
        did = data.ReadDataId();
        modeTransitionRampTimes = data.ReadDictionary(() => new ModeTransitionKey(data), data.ReadSingle);
        comboLookup = data.ReadDictionary(() => new ComboKey(data), () => new ComboInfo(data));
        transitionLookup = data.ReadDictionary(() => new ModeTransitionKey(data), () => new TransitionInfo(data));
        layers = data.ReadList(data.ReadUInt32);
        tweekLayers = data.ReadList(data.ReadUInt32);
        modeTransitionTable = data.ReadDictionary(() => new ModeStateKey(data), () => data.ReadList(() => new ModeStateKey(data)));
        modeList = data.ReadDictionary(() => new ModeStateKey(data), data.ReadUInt32);
        behaviorInfo = data.ReadDictionary(data.ReadUInt32, () => new BehaviorInfo(data));
        initBehaviors = data.ReadList(data.ReadUInt32);
        initMode = data.ReadUInt32();
    }
}
