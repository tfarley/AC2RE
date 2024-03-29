﻿using System.Collections.Generic;
using System.Numerics;

namespace AC2RE.Definitions;

public class CLightInfo {

    public class LightSourceInfo {

        // CLightInfo::LightSourceInfo
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

        // CLightInfo::LightCellInfo
        public CellId cellId; // nCellID
        public List<ushort> indices; // aIndices

        public LightCellInfo(AC2Reader data) {
            cellId = data.ReadCellId();
            int totalIndices = data.ReadInt32();
            if (totalIndices > 0) {
                indices = new(totalIndices);
                // TODO: This is wrong, these are actually RLE encoded - more RE needed
                int numIndices = (int)(data.ReadUInt32() / sizeof(ushort));
                for (int i = 0; i < numIndices; i++) {
                    indices.Add(data.ReadUInt16());
                }
                data.Align(4);
            }
        }
    }

    // CLightInfo
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
