using AC2E.Def;
using AC2E.Utils;

namespace AC2E.Interp {

    public interface IPkgRef {

    }

    public class PkgRef<T> : IPkgRef where T : IPackage {

        public PackageId id;
        public T value;

        public PkgRef(PackageId id) {
            this.id = id;
        }

        public PkgRef(T value) {
            id = PackageManager.registry.getId(value);
            this.value = value;
        }

        public override string ToString() {
            return id.id != PackageId.NULL.id
                ? $"Pkg[{id}] {Util.objectToString(value)}"
                : "Pkg[NULL]";
        }
    }
}
