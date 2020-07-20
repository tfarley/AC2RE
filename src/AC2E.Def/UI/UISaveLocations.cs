using System.Collections.Generic;

namespace AC2E.Def {

    public class UISaveLocations : IPackage {

        public class UILocationData {

            public float m_x0;
            public float m_y0;
            public float m_x1;
            public float m_y1;
            public bool m_shown;

            public UILocationData() {

            }

            public UILocationData(AC2Reader data) {
                m_x0 = data.ReadSingle();
                m_y0 = data.ReadSingle();
                m_x1 = data.ReadSingle();
                m_y1 = data.ReadSingle();
                m_shown = data.ReadBoolean();
            }

            public void write(AC2Writer data) {
                data.Write(m_x0);
                data.Write(m_y0);
                data.Write(m_x1);
                data.Write(m_y1);
                data.Write(m_shown);
            }
        }

        public NativeType nativeType => NativeType.UISAVELOCATIONS;

        public Dictionary<uint, Dictionary<uint, UILocationData>> contents;

        public UISaveLocations() {

        }

        public UISaveLocations(AC2Reader data) {
            contents = data.ReadDictionary(data.ReadUInt32, () => data.ReadDictionary(data.ReadUInt32, () => new UILocationData(data)));
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(contents, data.Write, v => data.Write(v, data.Write, v => v.write(data)));
        }
    }
}
