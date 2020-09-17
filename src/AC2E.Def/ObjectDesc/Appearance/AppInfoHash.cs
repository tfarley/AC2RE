using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class AppInfoHash : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.APPINFOHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<DataId, AppearanceInfo> contents;

        public AppInfoHash() {

        }

        public AppInfoHash(AC2Reader data) {
            contents = data.ReadDictionary(data.ReadDataId, () => new AppearanceInfo(data));
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write, v => v.write(data));
        }
    }
}
