using System.Collections.Generic;

namespace AC2E.Def {

    public class ALHash : Dictionary<uint, ulong>, IPackage {

        public NativeType nativeType => NativeType.ALHASH;

        public ALHash() {

        }

        public ALHash(AC2Reader data) {
            data.ReadDictionary(this, data.ReadUInt32, data.ReadUInt64);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.Write);
        }
    }
}
