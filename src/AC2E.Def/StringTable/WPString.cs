using System.IO;
using System.Text;

namespace AC2E.Def {

    public class WPString : IPackage {

        public NativeType nativeType => NativeType.WPSTRING;

        public string contents;

        public WPString() {

        }

        public WPString(BinaryReader data) {
            contents = data.ReadEncryptedString(Encoding.Unicode);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.WriteEncryptedString(contents, Encoding.Unicode);
        }

        public override string ToString() {
            return contents;
        }
    }
}
