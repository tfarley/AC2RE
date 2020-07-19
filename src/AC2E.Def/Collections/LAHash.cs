using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class LAHash : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.LAHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<ulong, uint> contents;

        public LAHash() {

        }

        public LAHash(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt64, data.ReadUInt32);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write, data.Write);
        }
    }
}
