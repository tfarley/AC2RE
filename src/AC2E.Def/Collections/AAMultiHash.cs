using System.Collections.Generic;

namespace AC2E.Def {

    public class AAMultiHash : Dictionary<uint, List<uint>>, IPackage {

        public NativeType nativeType => NativeType.AAMULTIHASH;

        public AAMultiHash() {

        }

        public AAMultiHash(AC2Reader data) {
            data.ReadMultiDictionary(this, data.ReadUInt32, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.WriteMulti(this, data.Write, data.Write);
        }
    }
}
