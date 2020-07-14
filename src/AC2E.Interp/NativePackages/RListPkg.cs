using AC2E.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class RListPkg<T> : IPackage where T : IPackage {

        public NativeType nativeType => NativeType.RLIST;

        public List<PkgRef<T>> contents;

        public RListPkg<U> to<U>() where U : class, IPackage {
            RListPkg<U> converted = new RListPkg<U>();
            if (contents != null) {
                converted.contents = new List<PkgRef<U>>(contents.Count);
                foreach (var element in contents) {
                    PkgRef<U> convertedPkgRef = new PkgRef<U>(element.id);
                    convertedPkgRef.value = element.value as U;
                    converted.contents.Add(convertedPkgRef);
                }
            }
            return converted;
        }

        public RListPkg<U> to<U>(PackageRegistry registry, Func<T, U> converter) where U : IPackage {
            RListPkg<U> converted = new RListPkg<U>();
            if (contents != null) {
                converted.contents = new List<PkgRef<U>>(contents.Count);
                foreach (var element in contents) {
                    PkgRef<U> convertedPkgRef = new PkgRef<U>(element.id);
                    convertedPkgRef.value = registry.convert(convertedPkgRef.id, converter);
                    converted.contents.Add(convertedPkgRef);
                }
            }
            return converted;
        }

        public RListPkg() {

        }

        public RListPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            contents = data.ReadList(() => data.ReadPkgRef<T>(resolvers));
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, v => data.Write(v, references));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
