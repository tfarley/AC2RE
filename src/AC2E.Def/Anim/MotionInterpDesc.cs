using System.Collections.Generic;

namespace AC2E.Def {

    public class MotionInterpDesc {

        public struct MotionValues {

            public float lift; // mLift
            public float drag; // mDrag
            public float terminalVel; // mTerminalVel
            public float sinkOffset; // mSinkOffset
            public float maxForwardVel; // mMaxForwardVel
            public float maxBackwardVel; // mMaxBackwardVel
            public float maxStrafeVel; // mMaxStrafeVel
            public float maxVertVel; // mMaxVertVel
            public float stillTurnVel; // mStillTurnVel
            public float runningTurnVel; // mRunningTurnVel
            public float forwardAcl; // mForwardAcl
            public float backwardAcl; // mBackwardAcl
            public float strafeAcl; // mStrafeAcl
            public float turnAcl; // mTurnAcl
            public float vertAcl; // mVertAcl
            public float forwardSB; // mForwardSB
            public float backwardSB; // mBackwardSB
            public float strafeSB; // mStrafeSB
            public float turnSB; // mTurnSB
            public float jumpImpulse; // mJumpImpulse
            public float hoverHeight; // mHoverHeight
            public bool gravity; // mGravity

            public MotionValues(AC2Reader data) {
                lift = data.ReadSingle();
                drag = data.ReadSingle();
                terminalVel = data.ReadSingle();
                gravity = data.ReadBoolean();
                sinkOffset = data.ReadSingle();
                hoverHeight = data.ReadSingle();
                jumpImpulse = data.ReadSingle();
                maxForwardVel = data.ReadSingle();
                maxBackwardVel = data.ReadSingle();
                maxStrafeVel = data.ReadSingle();
                stillTurnVel = data.ReadSingle();
                runningTurnVel = data.ReadSingle();
                maxVertVel = data.ReadSingle();
                forwardSB = data.ReadSingle();
                backwardSB = data.ReadSingle();
                strafeSB = data.ReadSingle();
                turnSB = data.ReadSingle();
                forwardAcl = data.ReadSingle();
                backwardAcl = data.ReadSingle();
                strafeAcl = data.ReadSingle();
                turnAcl = data.ReadSingle();
                vertAcl = data.ReadSingle();
            }
        }

        public struct TerrainList {

            public Dictionary<uint, MotionValues> modifierHash; // mTerrainModifierHash
            public MotionValues defaultValues; // mDefault

            public TerrainList(AC2Reader data) {
                modifierHash = data.ReadDictionary(data.ReadUInt32, () => new MotionValues(data));
                defaultValues = new MotionValues(data);
            }
        }

        public struct MSList {

            public MotionValues defaultValues; // mDefault
            public Dictionary<uint, TerrainList> terrainHash; // mTerrainHash
            public Dictionary<uint, MotionValues> modifierHash; // mModifierHash

            public MSList(AC2Reader data) {
                terrainHash = data.ReadDictionary(data.ReadUInt32, () => new TerrainList(data));
                modifierHash = data.ReadDictionary(data.ReadUInt32, () => new MotionValues(data));
                defaultValues = new MotionValues(data);
            }
        }

        public DataId did; // m_DID
        public Dictionary<ModeStateKey, uint> motionValues; // mMotionValues
        public List<MSList> valueArray; // mValueArray
        public float seaFloorMultiplier; // mSeaFloorMultiplier
        public float waterMultiplier; // mWaterMultiplier

        public MotionInterpDesc(AC2Reader data) {
            did = data.ReadDataId();
            motionValues = data.ReadDictionary(() => new ModeStateKey(data), data.ReadUInt32);
            valueArray = data.ReadList(() => new MSList(data));
            seaFloorMultiplier = data.ReadSingle();
            waterMultiplier = data.ReadSingle();
        }
    }
}
