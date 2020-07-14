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

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents, v => v.write(data, references));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
