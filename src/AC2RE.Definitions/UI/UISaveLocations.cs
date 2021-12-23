using System.Collections.Generic;

namespace AC2RE.Definitions;

public class UISaveLocations : Dictionary<uint, Dictionary<uint, UILocationData>>, IPackage {

    public NativeType nativeType => NativeType.UISaveLocations;

    public UISaveLocations() {

    }

    public UISaveLocations(AC2Reader data) {
        data.ReadDictionary(this, data.ReadUInt32, () => data.ReadDictionary(data.ReadUInt32, () => new UILocationData(data)));
    }

    public void write(AC2Writer data) {
        data.Write(this, data.Write, v => data.Write(v, data.Write, v => v.write(data)));
    }
}
