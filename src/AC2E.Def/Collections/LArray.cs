using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class LArray : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.LARRAY;
        public object delegatedToStringObject => contents;

        public List<ulong> contents;

        public LArray() {

        }

        public LArray(AC2Reader data) {
            contents = data.ReadList(data.ReadUInt64);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write);
        }
    }
}
