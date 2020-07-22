﻿using System.Collections.Generic;

namespace AC2E.Def {

    public class FXData {

        public FXNode defaultNode; // m_default
        public Dictionary<uint, FXNode> terrainData; // m_terrainData

        public FXData() {

        }

        public FXData(AC2Reader data) {
            defaultNode = new FXNode(data);
            terrainData = data.ReadDictionary(data.ReadUInt32, () => new FXNode(data));
        }

        public void write(AC2Writer data) {
            defaultNode.write(data);
            data.Write(terrainData, data.Write, v => v.write(data));
        }
    }
}