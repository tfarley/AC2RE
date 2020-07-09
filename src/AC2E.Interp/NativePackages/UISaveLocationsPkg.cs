using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class UISaveLocationsPkg : IPackage {

        public class UILocationData {

            public float m_x0;
            public float m_y0;
            public float m_x1;
            public float m_y1;
            public bool m_shown;

            public UILocationData() {

            }

            public UILocationData(BinaryReader data) {
                m_x0 = data.ReadSingle();
                m_y0 = data.ReadSingle();
                m_x1 = data.ReadSingle();
                m_y1 = data.ReadSingle();
                m_shown = data.ReadUInt32() != 0;
            }

            public void write(BinaryWriter data) {
                data.Write(m_x0);
                data.Write(m_y0);
                data.Write(m_x1);
                data.Write(m_y1);
                data.Write(m_shown ? (uint)1 : (uint)0);
            }
        }

        public NativeType nativeType => NativeType.UISAVELOCATIONS;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public Dictionary<uint, Dictionary<uint, UILocationData>> contents;

        public UISaveLocationsPkg() {

        }

        public UISaveLocationsPkg(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt32, () => data.ReadDictionary(data.ReadUInt32, () => new UILocationData(data)));
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, data.Write, v => data.Write(v, data.Write, v => v.write(data)));
        }
    }
}
