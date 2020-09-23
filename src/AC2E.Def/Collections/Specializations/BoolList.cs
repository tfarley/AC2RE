using System.Collections.Generic;

namespace AC2E.Def {

    public class BoolList : List<bool>, IPackage {

        public NativeType nativeType => NativeType.ALIST;

        public BoolList() {

        }

        public BoolList(AList list) {
            foreach (var element in list) {
                Add(element != 0);
            }
        }

        public BoolList(AC2Reader data) {
            data.ReadList(this, data.ReadBoolean);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
