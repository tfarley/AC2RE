using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class GMQuestInfoList : IPackage {

        public NativeType nativeType => NativeType.GMQUESTINFOLIST;

        public List<GMQuestInfo> contents;

        public GMQuestInfoList() {

        }

        public GMQuestInfoList(BinaryReader data) {
            contents = data.ReadList(() => new GMQuestInfo(data));
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, v => v.write(data, registry));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
