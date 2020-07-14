using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class AppInfoHash : IPackage {

        public NativeType nativeType => NativeType.APPINFOHASH;

        public Dictionary<uint, AppearanceInfo> contents;

        public AppInfoHash() {

        }

        public AppInfoHash(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt32, () => new AppearanceInfo(data));
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write, v => v.write(data));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
