using AC2E.Def;
using AC2E.Utils;
using System.Text;

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
            id = PackageManager.getId(value);
            _value = value;
        }

        public override string ToString() {
            StringBuilder stringBuilder = new StringBuilder(id.id != PackageId.NULL.id ? $"Pkg[{id}]" : "Pkg[NULL]");
            if (value != null) {
                stringBuilder.Append(' ');
                stringBuilder.Append(Util.objectToString(value));
            }
            return stringBuilder.ToString();
        }
    }
}
