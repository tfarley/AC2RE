using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class ARHashPkg<V> : IPackage where V : IPackage {

        public NativeType nativeType => NativeType.ARHASH;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

        public Dictionary<uint, V> contents;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents, data.Write, v => data.Write(v, references));
        }
    }
}
