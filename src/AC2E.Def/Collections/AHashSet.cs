using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class AHashSet : IPackage {

        public NativeType nativeType => NativeType.AHASHSET;

        public HashSet<uint> contents;

        public AHashSet() {

        }

        public AHashSet(BinaryReader data) {
            contents = data.ReadSet(data.ReadUInt32);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
