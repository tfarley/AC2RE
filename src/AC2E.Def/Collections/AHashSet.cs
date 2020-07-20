using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class AHashSet : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.AHASHSET;
        public object delegatedToStringObject => contents;

        public HashSet<uint> contents;

        public AHashSet() {

        }

        public AHashSet(AC2Reader data) {
            contents = data.ReadSet(data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write);
        }
    }
}
