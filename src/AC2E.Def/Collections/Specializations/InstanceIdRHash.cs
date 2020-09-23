using System.Collections.Generic;

namespace AC2E.Def {

    public class InstanceIdRHash<V> : Dictionary<InstanceId, V>, IPackage where V : IPackage {

        public NativeType nativeType => NativeType.LRHASH;

        public InstanceIdRHash() {

        }

        public InstanceIdRHash(LRHash<V> hash) {
            foreach (var element in hash) {
                this[new InstanceId(element.Key)] = element.Value;
            }
        }

        public InstanceIdRHash(AC2Reader data) {
            foreach (var element in data.ReadDictionary(data.ReadInstanceId, data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => this[element.Key] = data.packageRegistry.get<V>(element.Value));
            }
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.WritePkg);
        }
    }
}
