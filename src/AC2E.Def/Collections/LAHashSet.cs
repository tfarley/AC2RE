using System.Collections.Generic;

namespace AC2E.Def {

    public class LAHashSet : HashSet<ulong>, IPackage {

        public NativeType nativeType => NativeType.LAHASHSET;

        public LAHashSet() {

        }

        public LAHashSet(AC2Reader data) {
            data.ReadSet(this, data.ReadUInt64);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
