using System.Collections.Generic;

namespace AC2E.Def {

    public class AAHash : Dictionary<uint, uint>, IPackage {

        public NativeType nativeType => NativeType.AAHASH;

        public AAHash() {

        }

        public AAHash(AC2Reader data) {
            data.ReadDictionary(this, data.ReadUInt32, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.Write);
        }
    }
}
