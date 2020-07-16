using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class ALHash : IPackage {

        public NativeType nativeType => NativeType.ALHASH;

        public Dictionary<uint, ulong> contents;

        public ALHash() {

        }

        public ALHash(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt32, data.ReadUInt64);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write, data.Write);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
