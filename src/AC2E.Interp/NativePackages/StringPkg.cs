using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E.Interp {

    public class StringPkg : IPackage {

        public NativeType nativeType => NativeType.WPSTRING;

        public string contents;

        public StringPkg() {

        }

        public StringPkg(BinaryReader data) {
            contents = data.ReadEncryptedString(Encoding.Unicode);
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            data.WriteEncryptedString(contents, Encoding.Unicode);
        }

        public override string ToString() {
            return contents;
        }
    }
}
