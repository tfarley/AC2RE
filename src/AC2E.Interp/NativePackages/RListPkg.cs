using AC2E.Def;
using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public abstract class BaseRListPkg : IPackage {

        public NativeType nativeType => NativeType.RLIST;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public abstract void write(BinaryWriter data, List<PkgRef<IPackage>> references);
    }

    public class RListPkg : BaseRListPkg {

        public List<PackageId> contents;

        public RListPkg() {

        }

        public RListPkg(BinaryReader data) {
            contents = data.ReadList(data.ReadPackageId);
        }

        public override void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, v => data.Write(v, references));
        }
    }

    public class RListPkg<T> : BaseRListPkg where T : IPackage {

        public List<PkgRef<T>> contents;

        public RListPkg(RListPkg plain) {
            id = plain.id;
            if (plain.contents != null) {
                contents = new List<PkgRef<T>>(plain.contents.Count);
                foreach (var element in plain.contents) {
                    contents.Add(new PkgRef<T>(element));
                }
            }
        }

        public RListPkg toPlain() {
            RListPkg plain = new RListPkg();
            plain.id = id;
            if (contents != null) {
                plain.contents = new List<PackageId>(contents.Count);
                foreach (var element in contents) {
                    plain.contents.Add(element.id);
                }
            }
            return plain;
        }

        public RListPkg() {

        }

        public RListPkg(BinaryReader data) {
            contents = data.ReadList(() => data.ReadPkgRef<T>());
        }

        public override void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, v => data.Write(v, references));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
