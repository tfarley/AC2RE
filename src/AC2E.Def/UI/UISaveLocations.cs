using System.Collections.Generic;

namespace AC2E.Def {

    public class UISaveLocations : IPackage {

        public NativeType nativeType => NativeType.UISAVELOCATIONS;

        public Dictionary<uint, Dictionary<uint, UILocationData>> contents;

        public UISaveLocations() {

        }

        public UISaveLocations(AC2Reader data) {
            contents = data.ReadDictionary(data.ReadUInt32, () => data.ReadDictionary(data.ReadUInt32, () => new UILocationData(data)));
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write, v => data.Write(v, data.Write, v => v.write(data)));
        }
    }
}
