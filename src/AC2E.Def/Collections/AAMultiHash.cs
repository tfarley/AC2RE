using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class AAMultiHash : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.AAMULTIHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<uint, List<uint>> contents;

        public AAMultiHash() {

        }

        public AAMultiHash(AC2Reader data) {
            contents = data.ReadMultiDictionary(data.ReadUInt32, data.ReadUInt32);
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.WriteMulti(contents, data.Write, data.Write);
        }
    }
}
