using AC2E.Def;
using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class AppInfoHashPkg : IPackage {

        public NativeType nativeType => NativeType.APPINFOHASH;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public Dictionary<uint, VisualDesc.AppearanceInfo> contents;

        public AppInfoHashPkg() {

        }

        public AppInfoHashPkg(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt32, () => new VisualDesc.AppearanceInfo(data));
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, data.Write, v => v.write(data));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
