using System.Collections.Generic;
using System.Numerics;

namespace AC2RE.Definitions;

public class CLightInfo {

    public class LightSourceInfo {

        public CellId cellId; // nCellID
        public PropertyCollection properties; // pLightSource.?
        public Vector3 origin; // pLightSource.m_vOrigin
        public List<LightCellInfo> lightCellInfos; // cells

        public LightSourceInfo(AC2Reader data) {
            cellId = data.ReadCellId();
            properties = new(data);
            origin = data.ReadVector();
            lightCellInfos = data.ReadList(() => new LightCellInfo(data));
        }
    }

    public class LightCellInfo {

        public CellId cellId; // nCellID
        public List<ushort> indices; // aIndices

        public LightCellInfo(AC2Reader data) {
            cellId = data.ReadCellId();
            uint unk1 = data.ReadUInt32();
            if (unk1 > 0) {
                int numIndices = (int)(data.ReadUInt32() / sizeof(ushort));
                indices = new(numIndices);
                for (int i = 0; i < numIndices; i++) {
                    indices.Add(data.ReadUInt16());
                }
                data.Align(4);
            }
        }
    }

    public DataId did; // m_DID
    public Dictionary<uint, List<uint>> lookupHash; // m_lookupHash
    public List<LightSourceInfo> lightSourceInfos; // m_aInfos
    public bool sunlightEnabled; // m_bSunlightEnabled
    public List<List<LightCellInfo>> sunlights; // m_aSunlights

    public CLightInfo(AC2Reader data) {
        did = data.ReadDataId();
        lightSourceInfos = data.ReadList(() => new LightSourceInfo(data));
        lookupHash = data.ReadDictionary(data.ReadUInt32, () => data.ReadList(data.ReadUInt32));
        sunlightEnabled = data.ReadBoolean();
        if (sunlightEnabled) {
            sunlights = new(9);
            for (int i = 0; i < 9; i++) {
                sunlights.Add(data.ReadList(() => new LightCellInfo(data)));
            }
        }
    }
}
