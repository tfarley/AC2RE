using AC2E.Utils;
using System.Collections.Generic;

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

        public BoolList(AC2Reader data) {
            contents = data.ReadList(data.ReadBoolean);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write);
        }
    }
}
