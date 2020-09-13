using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class ADataIdHash : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.AAHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<uint, DataId> contents;

        public ADataIdHash() {

        }

        public ADataIdHash(AAHash hash) {
            contents = new Dictionary<uint, DataId>();
            foreach (var element in hash.contents) {
                contents[element.Key] = new DataId(element.Value);
            }
        }

        public ADataIdHash(AC2Reader data) {
            contents = data.ReadDictionary(data.ReadUInt32, data.ReadDataId);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write, data.Write);
        }
    }
}
