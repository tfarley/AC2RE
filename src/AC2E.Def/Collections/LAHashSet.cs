using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class LAHashSet : IPackage {

        public NativeType nativeType => NativeType.LAHASHSET;

        public HashSet<ulong> contents;

        public LAHashSet() {

        }

        public LAHashSet(BinaryReader data) {
            contents = data.ReadSet(data.ReadUInt64);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
