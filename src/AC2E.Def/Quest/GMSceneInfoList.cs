using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class GMSceneInfoList : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.GMSCENEINFOLIST;
        public object delegatedToStringObject => contents;

        public List<GMSceneInfo> contents;

        public GMSceneInfoList() {

        }

        public GMSceneInfoList(AC2Reader data) {
            contents = data.ReadList(() => new GMSceneInfo(data));
        }

        public void write(AC2Writer data) {
            data.Write(contents, v => v.write(data));
        }
    }
}
