using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class LRHashPkg<V> : IPackage where V : IPackage {

        public NativeType nativeType => NativeType.LRHASH;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public Dictionary<ulong, V> contents;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents, data.Write, v => data.Write(v, references));
        }
    }
}
