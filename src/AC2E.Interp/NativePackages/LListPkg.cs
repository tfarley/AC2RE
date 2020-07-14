using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class LListPkg : IPackage {

        public NativeType nativeType => NativeType.LLIST;

        public List<ulong> contents;

        public LListPkg() {

        }

        public LListPkg(BinaryReader data) {
            contents = data.ReadList(data.ReadUInt64);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
