using System.Collections.Generic;

namespace AC2RE.Definitions;

public class AppInfoHash : Dictionary<DataId, Dictionary<AppearanceKey, float>>, IPackage {

    public NativeType nativeType => NativeType.AppInfoHash;

    public AppInfoHash() {

    }

    public AppInfoHash(AC2Reader data) {
        data.ReadDictionary(this, data.ReadDataId, () => data.ReadDictionary(() => (AppearanceKey)data.ReadUInt32(), data.ReadSingle));
    }

    public void write(AC2Writer data) {
        data.Write(this, data.Write, v => data.Write(v, v => data.Write((uint)v), data.Write));
    }
}
