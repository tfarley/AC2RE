using System.Collections.Generic;

namespace AC2E.Def {

    public class DataIdList : List<DataId>, IPackage {

        public NativeType nativeType => NativeType.ALIST;

        public DataIdList() {

        }

        public DataIdList(AList list) {
            foreach (var element in list) {
                Add(new DataId(element));
            }
        }

        public DataIdList(AC2Reader data) {
            data.ReadList(this, data.ReadDataId);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
