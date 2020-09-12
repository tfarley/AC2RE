using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class LAMultiHash : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.LAMULTIHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<ulong, List<uint>> contents;

        public LAMultiHash() {

        }

        public LAMultiHash(AC2Reader data) {
            contents = data.ReadMultiDictionary(data.ReadUInt64, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.WriteMulti(contents, data.Write, data.Write);
        }
    }
}
