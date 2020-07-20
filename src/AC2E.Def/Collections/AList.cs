using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class AList : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.ALIST;
        public object delegatedToStringObject => contents;

        public List<uint> contents;

        public AList() {

        }

        public AList(AC2Reader data) {
            contents = data.ReadList(data.ReadUInt32);
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(contents, data.Write);
        }
    }
}
