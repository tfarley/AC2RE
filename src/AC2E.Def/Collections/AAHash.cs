using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class AAHash : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.AAHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<uint, uint> contents;

        public AAHash() {

        }

        public AAHash(AC2Reader data) {
            contents = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write, data.Write);
        }
    }
}
