using AC2E.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class LRHashPkg<V> : IPackage where V : IPackage {

        public NativeType nativeType => NativeType.LRHASH;

        public Dictionary<ulong, PkgRef<V>> contents;

        public LRHashPkg<T> to<T>() where T : class, IPackage {
            LRHashPkg<T> converted = new LRHashPkg<T>();
            if (contents != null) {
                converted.contents = new Dictionary<ulong, PkgRef<T>>(contents.Count);
                foreach (var element in contents) {
                    PkgRef<T> convertedPkgRef = new PkgRef<T>(element.Value.id);
                    convertedPkgRef.value = element.Value.value as T;
                    converted.contents[element.Key] = convertedPkgRef;
                }
            }
            return converted;
        }

        public LRHashPkg<T> to<T>(PackageRegistry registry, Func<V, T> converter) where T : IPackage {
            LRHashPkg<T> converted = new LRHashPkg<T>();
            if (contents != null) {
                converted.contents = new Dictionary<ulong, PkgRef<T>>(contents.Count);
                foreach (var element in contents) {
                    PkgRef<T> convertedPkgRef = new PkgRef<T>(element.Value.id);
                    convertedPkgRef.value = registry.convert(convertedPkgRef.id, converter);
                    converted.contents[element.Key] = convertedPkgRef;
                }
            }
            return converted;
        }

        public LRHashPkg() {

        }

        public LRHashPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            contents = data.ReadDictionary(data.ReadUInt64, () => data.ReadPkgRef<V>(resolvers));
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, data.Write, v => data.Write(v, references));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
