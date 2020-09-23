using System.Collections.Generic;

namespace AC2E.Def {

    public class LList : List<ulong>, IPackage {

        public NativeType nativeType => NativeType.LLIST;

        public LList() {

        }

        public LList(AC2Reader data) {
            data.ReadList(this, data.ReadUInt64);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
