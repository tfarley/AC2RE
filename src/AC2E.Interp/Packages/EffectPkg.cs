using AC2E.Def.Extensions;
using AC2E.Def.Structs;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Packages {

    public class EffectPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.Effect;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.SINGLETON | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAP_OBJECT);

        public uint id { get; set; }

        public DataId did;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(did);
        }
    }
}
