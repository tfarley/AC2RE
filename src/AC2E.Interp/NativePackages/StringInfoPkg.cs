using AC2E.Def;
using AC2E.Utils;
using System.IO;

namespace AC2E.Interp {

    public class StringInfoPkg : IPackage {

        public NativeType nativeType => NativeType.STRINGINFO;

        public StringInfo contents;

        public StringInfoPkg() {

        }

        public StringInfoPkg(BinaryReader data) {
            contents = new StringInfo(data);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            contents.write(data);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
