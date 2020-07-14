using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class AAHashPkg : IPackage {

        public NativeType nativeType => NativeType.AAHASH;

        public Dictionary<uint, uint> contents;

        public AAHashPkg() {

        }

        public AAHashPkg(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write, data.Write);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
