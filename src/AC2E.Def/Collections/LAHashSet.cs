using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class LAHashSet : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.LAHASHSET;
        public object delegatedToStringObject => contents;

        public HashSet<ulong> contents;

        public LAHashSet() {

        }

        public LAHashSet(AC2Reader data) {
            contents = data.ReadSet(data.ReadUInt64);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write);
        }
    }
}
