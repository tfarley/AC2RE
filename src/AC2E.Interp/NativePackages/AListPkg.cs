using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class AListPkg : IPackage {

        public NativeType nativeType => NativeType.ALIST;

        public List<uint> contents;

        public AListPkg() {

        }

        public AListPkg(BinaryReader data) {
            contents = data.ReadList(data.ReadUInt32);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
