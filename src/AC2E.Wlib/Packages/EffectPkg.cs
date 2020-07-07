using AC2E.Dat;
using AC2E.Def;
using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class EffectPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.Effect;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.SINGLETON | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public DataId did;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(did);
        }
    }
}
