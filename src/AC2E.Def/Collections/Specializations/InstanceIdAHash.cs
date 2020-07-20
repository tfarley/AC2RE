using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class InstanceIdAHash : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.LAHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<InstanceId, uint> contents;

        public InstanceIdAHash() {

        }

        public InstanceIdAHash(LAHash hash) {
            contents = new Dictionary<InstanceId, uint>();
            foreach (var element in hash.contents) {
                contents[new InstanceId(element.Key)] = element.Value;
            }
        }

        public InstanceIdAHash(AC2Reader data) {
            contents = data.ReadDictionary(data.ReadInstanceId, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write, data.Write);
        }
    }
}
