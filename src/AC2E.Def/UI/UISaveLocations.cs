using System.Collections.Generic;

namespace AC2E.Def {

    public class UISaveLocations : IPackage {

        public class UILocationData {

            public float x0; // m_x0
            public float y0; // m_y0
            public float x1; // m_x1
            public float y1; // m_y1
            public bool shown; // m_shown

            public UILocationData() {

            }

            public UILocationData(AC2Reader data) {
                x0 = data.ReadSingle();
                y0 = data.ReadSingle();
                x1 = data.ReadSingle();
                y1 = data.ReadSingle();
                shown = data.ReadBoolean();
            }

            public void write(AC2Writer data) {
                data.Write(x0);
                data.Write(y0);
                data.Write(x1);
                data.Write(y1);
                data.Write(shown);
            }
        }

        public NativeType nativeType => NativeType.UISAVELOCATIONS;

        public Dictionary<uint, Dictionary<uint, UILocationData>> contents;

        public UISaveLocations() {

        }

        public UISaveLocations(AC2Reader data) {
            contents = data.ReadDictionary(data.ReadUInt32, () => data.ReadDictionary(data.ReadUInt32, () => new UILocationData(data)));
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write, v => data.Write(v, data.Write, v => v.write(data)));
        }
    }
}
