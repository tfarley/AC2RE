using System.Collections.Generic;

namespace AC2E.Def {

    public class AList : List<uint>, IPackage {

        public NativeType nativeType => NativeType.ALIST;

        public AList() {

        }

        public AList(AC2Reader data) {
            data.ReadList(this, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
