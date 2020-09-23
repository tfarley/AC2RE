using System.Collections.Generic;

namespace AC2E.Def {

    public class ADataIdHash : Dictionary<uint, DataId>, IPackage {

        public NativeType nativeType => NativeType.AAHASH;

        public ADataIdHash() {

        }

        public ADataIdHash(AAHash hash) {
            foreach (var element in hash) {
                this[element.Key] = new DataId(element.Value);
            }
        }

        public ADataIdHash(AC2Reader data) {
            data.ReadDictionary(this, data.ReadUInt32, data.ReadDataId);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.Write);
        }
    }
}
