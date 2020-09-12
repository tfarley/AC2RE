using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class AArray : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.AARRAY;
        public object delegatedToStringObject => contents;

        public List<uint> contents;

        public AArray() {

        }

        public AArray(AC2Reader data) {
            contents = data.ReadList(data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write);
        }
    }
}
