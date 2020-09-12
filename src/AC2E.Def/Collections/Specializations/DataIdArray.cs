using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class DataIdArray : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.AARRAY;
        public object delegatedToStringObject => contents;

        public List<DataId> contents;

        public DataIdArray() {

        }

        public DataIdArray(AArray array) {
            contents = new List<DataId>();
            foreach (var element in array.contents) {
                contents.Add(new DataId(element));
            }
        }

        public DataIdArray(AC2Reader data) {
            contents = data.ReadList(data.ReadDataId);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write);
        }
    }
}
