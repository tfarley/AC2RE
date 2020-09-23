using System.Collections.Generic;

namespace AC2E.Def {

    public class RList<T> : List<T>, IPackage where T : IPackage {

        public virtual NativeType nativeType => NativeType.RLIST;

        public RList<U> to<U>() where U : class, IPackage {
            RList<U> converted = new RList<U>(Count);
            foreach (var element in this) {
                converted.Add(element as U);
            }
            return converted;
        }

        public RList() {

        }

        public RList(int capacity) : base(capacity) {

        }

        public RList(AC2Reader data) {
            foreach (var element in data.ReadList(data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => Add(data.packageRegistry.get<T>(element)));
            }
        }

        public void write(AC2Writer data) {
            data.Write(this, data.WritePkg);
        }
    }
}
