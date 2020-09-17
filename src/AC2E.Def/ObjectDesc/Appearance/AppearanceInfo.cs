using System.Collections.Generic;

namespace AC2E.Def {

    public class AppearanceInfo {

        public Dictionary<AppearanceKey, float> appearances; // m_appkeyHash

        public AppearanceInfo() {

        }

        public AppearanceInfo(AC2Reader data) {
            appearances = data.ReadDictionary(() => (AppearanceKey)data.ReadUInt32(), data.ReadSingle);
        }

        public void write(AC2Writer data) {
            data.Write(appearances, v => data.Write((uint)v), data.Write);
        }
    }
}
