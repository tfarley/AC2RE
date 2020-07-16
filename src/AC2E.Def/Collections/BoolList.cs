using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class BoolList : IPackage {

        public NativeType nativeType => NativeType.ALIST;

        public List<bool> contents;

        public BoolList() {

        }

        public BoolList(AList list) {
            contents = new List<bool>();
            foreach (var element in list.contents) {
                contents.Add(element != 0);
            }
        }

        public BoolList(BinaryReader data) {
            contents = data.ReadList(() => data.ReadUInt32() != 0);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, v => data.Write(v ? (uint)1 : (uint)0));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
