using AC2E.Def;
using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public abstract class BaseLRHashPkg : IPackage {

        public NativeType nativeType => NativeType.LRHASH;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public abstract void write(BinaryWriter data, List<PkgRef<IPackage>> references);
    }

    public class LRHashPkg : BaseLRHashPkg {

        public Dictionary<ulong, PackageId> contents;

        public LRHashPkg() {

        }

        public LRHashPkg(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt64, data.ReadPackageId);
        }

        public override void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, data.Write, v => data.Write(v, references));
        }
    }

    public class LRHashPkg<V> : BaseLRHashPkg where V : IPackage {

        public Dictionary<ulong, PkgRef<V>> contents;

        public LRHashPkg(LRHashPkg plain) {
            id = plain.id;
            if (plain.contents != null) {
                contents = new Dictionary<ulong, PkgRef<V>>(plain.contents.Count);
                foreach (var element in plain.contents) {
                    contents[element.Key] = new PkgRef<V>(element.Value);
                }
            }
        }

        public LRHashPkg toPlain() {
            LRHashPkg plain = new LRHashPkg();
            plain.id = id;
            if (contents != null) {
                plain.contents = new Dictionary<ulong, PackageId>(contents.Count);
                foreach (var element in contents) {
                    plain.contents[element.Key] = element.Value.id;
                }
            }
            return plain;
        }

        public LRHashPkg() {

        }

        public LRHashPkg(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt64, () => data.ReadPkgRef<V>());
        }

        public override void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, data.Write, v => data.Write(v, references));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
