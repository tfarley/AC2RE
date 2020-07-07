using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class StringInfoPkg<V> : IPackage where V : IPackage {

        public NativeType nativeType => NativeType.STRINGINFO;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

        public StringInfo stringInfo;

        public void write(BinaryWriter data, List<IPackage> references) {
            stringInfo.write(data);
        }
    }
}
