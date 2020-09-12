using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class RArray<T> : IPackage, IDelegateToString where T : IPackage {

        public virtual NativeType nativeType => NativeType.RLIST;
        public object delegatedToStringObject => contents;

        public List<T> contents;

        public RArray<U> to<U>() where U : class, IPackage {
            RArray<U> converted = new RArray<U>();
            if (contents != null) {
                converted.contents = new List<U>(contents.Count);
                foreach (var element in contents) {
                    converted.contents.Add(element as U);
                }
            }
            return converted;
        }

        public RArray() {

        }

        public RArray(AC2Reader data) {
            contents = new List<T>();
            foreach (var element in data.ReadList(data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => contents.Add(data.packageRegistry.get<T>(element)));
            }
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.WritePkg);
        }
    }
}
