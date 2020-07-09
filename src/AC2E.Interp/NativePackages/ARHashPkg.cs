using AC2E.Def;
using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public abstract class BaseARHashPkg : IPackage {

        public NativeType nativeType => NativeType.ARHASH;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public abstract void write(BinaryWriter data, List<PkgRef<IPackage>> references);
    }

    public class ARHashPkg : BaseARHashPkg {

        public Dictionary<uint, PackageId> contents;

        public ARHashPkg() {

        }

        public ARHashPkg(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt32, data.ReadPackageId);
        }

        public override void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, data.Write, v => data.Write(v, references));
        }
    }

    public class ARHashPkg<V> : BaseARHashPkg where V : IPackage {

        public Dictionary<uint, PkgRef<V>> contents;

        public ARHashPkg(ARHashPkg plain) {
            id = plain.id;
            if (plain.contents != null) {
                contents = new Dictionary<uint, PkgRef<V>>(plain.contents.Count);
                foreach (var element in plain.contents) {
                    contents[element.Key] = new PkgRef<V>(element.Value);
                }
            }
        }

        public ARHashPkg toPlain() {
            ARHashPkg plain = new ARHashPkg();
            plain.id = id;
            if (contents != null) {
                plain.contents = new Dictionary<uint, PackageId>(contents.Count);
                foreach (var element in contents) {
                    plain.contents[element.Key] = element.Value.id;
                }
            }
            return plain;
        }

        public ARHashPkg() {

        }

        public ARHashPkg(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt32, () => data.ReadPkgRef<V>());
        }

        public override void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, data.Write, v => data.Write(v, references));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
