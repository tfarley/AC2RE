using System.Collections.Generic;

namespace AC2E.Def {

    public class AHashSet : HashSet<uint>, IPackage {

        public NativeType nativeType => NativeType.AHASHSET;

        public AHashSet() {

        }

        public AHashSet(AC2Reader data) {
            data.ReadSet(this, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
