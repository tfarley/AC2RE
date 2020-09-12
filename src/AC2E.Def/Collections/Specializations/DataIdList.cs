using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class DataIdList : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.ALIST;
        public object delegatedToStringObject => contents;

        public List<DataId> contents;

        public DataIdList() {

        }

        public DataIdList(AList list) {
            contents = new List<DataId>();
            foreach (var element in list.contents) {
                contents.Add(new DataId(element));
            }
        }

        public DataIdList(AC2Reader data) {
            contents = data.ReadList(data.ReadDataId);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write);
        }
    }
}
