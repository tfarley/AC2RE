using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class GMQuestInfoListPkg : IPackage {

        public NativeType nativeType => NativeType.GMQUESTINFOLIST;

        public List<GMQuestInfoPkg> contents;

        public GMQuestInfoListPkg() {

        }

        public GMQuestInfoListPkg(BinaryReader data) {
            contents = data.ReadList(() => new GMQuestInfoPkg(data));
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, v => v.write(data, registry));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
