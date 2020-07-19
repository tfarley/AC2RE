using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class BoolList : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.ALIST;
        public object delegatedToStringObject => contents;

        public List<bool> contents;

        public BoolList() {

        }

        public BoolList(AList list) {
            contents = new List<bool>();
            foreach (var element in list.contents) {
                contents.Add(element != 0);
            }
        }

        public BoolList(BinaryReader data) {
            contents = data.ReadList(() => data.ReadUInt32() != 0);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, v => data.Write(v ? (uint)1 : (uint)0));
        }
    }
}
