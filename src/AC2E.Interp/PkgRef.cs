using AC2E.Def;
using AC2E.Utils;

namespace AC2E.Interp {

    public interface IPkgRef {

    }

    public class PkgRef<T> : IPkgRef where T : IPackage {

        public readonly PackageId id;
        private T _value;
        public T value {
            get {
                if (_value == null) {
                    _value = PackageManager.get<T>(id);
                }
                return _value;
            }
        }
        public IPackage rawValue => PackageManager.get<IPackage>(id);

        public PkgRef(PackageId id) {
            this.id = id;
        }

        public PkgRef(T value) {
            id = value != null ? value.id : PackageId.NULL;
            _value = value;
        }

        public override string ToString() {
            return value != null ? Util.objectToString(value) : (id.id != PackageId.NULL.id ? $"PkgRef[{id}]" : "PkgRef[NULL]");
        }
    }
}
