using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class InstanceIdRHash<V> : IPackage, IDelegateToString where V : IPackage {

        public NativeType nativeType => NativeType.LRHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<InstanceId, V> contents;

        public InstanceIdRHash() {

        }

        public InstanceIdRHash(LRHash<V> hash) {
            contents = new Dictionary<InstanceId, V>();
            foreach (var element in hash.contents) {
                contents[new InstanceId(element.Key)] = element.Value;
            }
        }

        public InstanceIdRHash(BinaryReader data, PackageRegistry registry) {
            contents = new Dictionary<InstanceId, V>();
            foreach (var element in data.ReadDictionary(data.ReadInstanceId, data.ReadPackageId)) {
                registry.addResolver(() => contents[element.Key] = registry.get<V>(element.Value));
            }
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write, v => data.Write(v, registry));
        }
    }
}
