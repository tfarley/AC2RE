using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class AHashSetPkg : IPackage {

        public NativeType nativeType => NativeType.AHASHSET;

        public HashSet<uint> contents;

        public AHashSetPkg() {

        }

        public AHashSetPkg(BinaryReader data) {
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
