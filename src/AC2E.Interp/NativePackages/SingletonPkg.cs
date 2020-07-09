using AC2E.Dat;
using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class SingletonPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.SINGLETON | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public DataId did;

        public SingletonPkg() {

        }

        public SingletonPkg(BinaryReader data) {
            did = data.ReadDataId();
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(did);
        }
    }
}
