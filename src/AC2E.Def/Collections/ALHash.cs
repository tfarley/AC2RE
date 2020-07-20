using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class ALHash : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.ALHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<uint, ulong> contents;

        public ALHash() {

        }

        public ALHash(AC2Reader data) {
            contents = data.ReadDictionary(data.ReadUInt32, data.ReadUInt64);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write, data.Write);
        }
    }
}
