using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class LList : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.LLIST;
        public object delegatedToStringObject => contents;

        public List<ulong> contents;

        public LList() {

        }

        public LList(AC2Reader data) {
            contents = data.ReadList(data.ReadUInt64);
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(contents, data.Write);
        }
    }
}
