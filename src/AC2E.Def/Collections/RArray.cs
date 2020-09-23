using System.Collections.Generic;

namespace AC2E.Def {

    public class RArray<T> : List<T>, IPackage where T : IPackage {

        public virtual NativeType nativeType => NativeType.RLIST;

        public RArray<U> to<U>() where U : class, IPackage {
            RArray<U> converted = new RArray<U>(Count);
            foreach (var element in this) {
                converted.Add(element as U);
            }
            return converted;
        }

        public RArray() {

        }

        public RArray(int capacity) : base(capacity) {

        }

        public RArray(AC2Reader data) {
            foreach (var element in data.ReadList(data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => Add(data.packageRegistry.get<T>(element)));
            }
        }

        public void write(AC2Writer data) {
            data.Write(this, data.WritePkg);
        }
    }
}
