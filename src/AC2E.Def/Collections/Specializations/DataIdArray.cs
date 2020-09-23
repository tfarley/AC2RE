using System.Collections.Generic;

namespace AC2E.Def {

    public class DataIdArray : List<DataId>, IPackage {

        public NativeType nativeType => NativeType.AARRAY;

        public DataIdArray() {

        }

        public DataIdArray(AArray array) {
            foreach (var element in array) {
                Add(new DataId(element));
            }
        }

        public DataIdArray(AC2Reader data) {
            data.ReadList(this, data.ReadDataId);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
