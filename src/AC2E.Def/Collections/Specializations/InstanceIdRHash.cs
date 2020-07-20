using AC2E.Utils;
using System.Collections.Generic;

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

        public InstanceIdRHash(AC2Reader data) {
            contents = new Dictionary<InstanceId, V>();
            foreach (var element in data.ReadDictionary(data.ReadInstanceId, data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => contents[element.Key] = data.packageRegistry.get<V>(element.Value));
            }
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write, data.WritePkg);
        }
    }
}
