using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class LListPkg : IPackage {

        public NativeType nativeType => NativeType.LLIST;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public List<ulong> contents;

        public LListPkg() {

        }

        public LListPkg(BinaryReader data) {
            contents = data.ReadList(data.ReadUInt64);
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents, data.Write);
        }
    }
}
