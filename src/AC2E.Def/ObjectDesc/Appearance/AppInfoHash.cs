using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class AppInfoHash : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.APPINFOHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<DataId, Dictionary<AppearanceKey, float>> contents;

        public AppInfoHash() {

        }

        public AppInfoHash(AC2Reader data) {
            contents = data.ReadDictionary(data.ReadDataId, () => data.ReadDictionary(() => (AppearanceKey)data.ReadUInt32(), data.ReadSingle));
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write, v => data.Write(v, v => data.Write((uint)v), data.Write));
        }
    }
}
