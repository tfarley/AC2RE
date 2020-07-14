using AC2E.Def;
using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class AppInfoHashPkg : IPackage {

        public NativeType nativeType => NativeType.APPINFOHASH;

        public Dictionary<uint, AppearanceInfo> contents;

        public AppInfoHashPkg() {

        }

        public AppInfoHashPkg(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt32, () => new AppearanceInfo(data));
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents, data.Write, v => v.write(data));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
