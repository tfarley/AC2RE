using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class AHashSet : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.AHASHSET;
        public object delegatedToStringObject => contents;

        public HashSet<uint> contents;

        public AHashSet() {

        }

        public AHashSet(BinaryReader data) {
            contents = data.ReadSet(data.ReadUInt32);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write);
        }
    }
}
