﻿using System.Collections.Generic;

namespace AC2E.Def {

    public class AppearanceInfo {

        public Dictionary<uint, float> appearances; // m_appkeyHash

        public AppearanceInfo() {

        }

        public AppearanceInfo(AC2Reader data) {
            appearances = data.ReadDictionary(data.ReadUInt32, data.ReadSingle);
        }

        public void write(AC2Writer data) {
            data.Write(appearances, data.Write, data.Write);
        }
    }
}