using AC2E.Def;
using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class AAMultiHashPkg : IPackage {

        public NativeType nativeType => NativeType.AAMULTIHASH;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public Dictionary<uint, List<uint>> contents;

        public AAMultiHashPkg() {

        }

        public AAMultiHashPkg(BinaryReader data) {
            contents = data.ReadMultiDictionary(data.ReadUInt32, data.ReadUInt32);
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.WriteMulti(contents, data.Write, data.Write);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
