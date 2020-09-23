using System.Collections.Generic;

namespace AC2E.Def {

    public class InstanceIdAHash : Dictionary<InstanceId, uint>, IPackage {

        public NativeType nativeType => NativeType.LAHASH;

        public InstanceIdAHash() {

        }

        public InstanceIdAHash(LAHash hash) {
            foreach (var element in hash) {
                this[new InstanceId(element.Key)] = element.Value;
            }
        }

        public InstanceIdAHash(AC2Reader data) {
            data.ReadDictionary(this, data.ReadInstanceId, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.Write);
        }
    }
}
