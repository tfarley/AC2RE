using System.Collections.Generic;

namespace AC2E.Def {

    public class LAHash : Dictionary<ulong, uint>, IPackage {

        public NativeType nativeType => NativeType.LAHASH;

        public LAHash() {

        }

        public LAHash(AC2Reader data) {
            data.ReadDictionary(this, data.ReadUInt64, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.Write);
        }
    }
}
