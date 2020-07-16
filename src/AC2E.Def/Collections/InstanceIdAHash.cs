using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class InstanceIdAHash : IPackage {

        public NativeType nativeType => NativeType.LAHASH;

        public Dictionary<InstanceId, uint> contents;

        public InstanceIdAHash() {

        }

        public InstanceIdAHash(LAHash hash) {
            contents = new Dictionary<InstanceId, uint>();
            foreach (var element in hash.contents) {
                contents[new InstanceId(element.Key)] = element.Value;
            }
        }

        public InstanceIdAHash(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadInstanceId, data.ReadUInt32);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write, data.Write);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
