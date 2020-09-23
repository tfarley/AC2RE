using System.Collections.Generic;

namespace AC2E.Def {

    public class LAMultiHash : Dictionary<ulong, List<uint>>, IPackage {

        public NativeType nativeType => NativeType.LAMULTIHASH;

        public LAMultiHash() {

        }

        public LAMultiHash(AC2Reader data) {
            data.ReadMultiDictionary(this, data.ReadUInt64, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.WriteMulti(this, data.Write, data.Write);
        }
    }
}
