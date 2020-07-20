using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class GMQuestInfoList : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.GMQUESTINFOLIST;
        public object delegatedToStringObject => contents;

        public List<GMQuestInfo> contents;

        public GMQuestInfoList() {

        }

        public GMQuestInfoList(AC2Reader data) {
            contents = data.ReadList(() => new GMQuestInfo(data));
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(contents, v => v.write(data, registry));
        }
    }
}
