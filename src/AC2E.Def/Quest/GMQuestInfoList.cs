using System.Collections.Generic;

namespace AC2E.Def {

    public class GMQuestInfoList : List<GMQuestInfo>, IPackage {

        public NativeType nativeType => NativeType.GMQUESTINFOLIST;

        public GMQuestInfoList() {

        }

        public GMQuestInfoList(AC2Reader data) {
            data.ReadList(this, () => new GMQuestInfo(data));
        }

        public void write(AC2Writer data) {
            data.Write(this, v => v.write(data));
        }
    }
}
