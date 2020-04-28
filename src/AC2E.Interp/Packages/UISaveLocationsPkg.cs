using AC2E.Def.Extensions;
using AC2E.Interp.Extensions;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Packages {

    public class UISaveLocationsPkg : IPackage {

        public class UILocationData {

            public float m_x0;
            public float m_y0;
            public float m_x1;
            public float m_y1;
            public bool m_shown;

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
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAP_OBJECT);

        public uint id { get; set; }

        public Dictionary<uint, Dictionary<uint, UILocationData>> contents;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents, data.Write, v => data.Write(v, data.Write, v => v.write(data)));
        }
    }
}
