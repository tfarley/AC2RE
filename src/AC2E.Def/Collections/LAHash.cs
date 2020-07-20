using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class LAHash : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.LAHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<ulong, uint> contents;

        public LAHash() {

        }

        public LAHash(AC2Reader data) {
            contents = data.ReadDictionary(data.ReadUInt64, data.ReadUInt32);
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(contents, data.Write, data.Write);
        }
    }
}
