using System.Collections.Generic;

namespace AC2RE.Definitions;

public class AppInfoHash : Dictionary<DataId, Dictionary<AppearanceKey, float>>, IHeapObject {

    public NativeType nativeType => NativeType.AppInfoHash;

    public AppInfoHash() {

    }

    public AppInfoHash(AC2Reader data) {
        data.ReadDictionary(this, data.ReadDataId, () => data.ReadDictionary(data.ReadEnum<AppearanceKey>, data.ReadSingle));
    }

    public void write(AC2Writer data) {
        data.Write(this, data.Write, v => data.Write(v, data.WriteEnum, data.Write));
    }
}
