using AC2E.Def.Structs;
using System.IO;

namespace AC2E.Interp {

    public class EffectPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.Effect;
        public InterpReference reference => new InterpReference(InterpReference.Flag.LOADED | InterpReference.Flag.SINGLETON | InterpReference.Flag.RECURSE, ReferenceType.HEAP_OBJECT);

        public uint id { get; set; }
        public IPackage[] references => new IPackage[] { };

        public DataId did;

        public void write(BinaryWriter data) {
            data.Write(did);
        }
    }
}
