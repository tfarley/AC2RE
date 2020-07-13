using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class AppearanceInfo {

        public Dictionary<uint, float> appKeyToValue; // m_appkeyHash

        public AppearanceInfo() {

        }

        public AppearanceInfo(BinaryReader data) {
            appKeyToValue = data.ReadDictionary(data.ReadUInt32, data.ReadSingle);
        }

        public void write(BinaryWriter data) {
            data.Write(appKeyToValue, data.Write, data.Write);
        }
    }
}
