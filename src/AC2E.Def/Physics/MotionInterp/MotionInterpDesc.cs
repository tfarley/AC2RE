using System.Collections.Generic;

namespace AC2E.Def {

    public class MotionInterpDesc {

        public struct TerrainList {

            public Dictionary<uint, MotionValues> modifierHash; // mTerrainModifierHash
            public MotionValues defaultValues; // mDefault

            public TerrainList(AC2Reader data) {
                modifierHash = data.ReadDictionary(data.ReadUInt32, () => new MotionValues(data));
                defaultValues = new(data);
            }
        }

        public struct MSList {

            public MotionValues defaultValues; // mDefault
            public Dictionary<uint, TerrainList> terrainHash; // mTerrainHash
            public Dictionary<uint, MotionValues> modifierHash; // mModifierHash

            public MSList(AC2Reader data) {
                terrainHash = data.ReadDictionary(data.ReadUInt32, () => new TerrainList(data));
                modifierHash = data.ReadDictionary(data.ReadUInt32, () => new MotionValues(data));
                defaultValues = new(data);
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
