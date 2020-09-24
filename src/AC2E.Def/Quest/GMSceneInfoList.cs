using System.Collections.Generic;

namespace AC2E.Def {

    public class GMSceneInfoList : List<GMSceneInfo>, IPackage {

        public NativeType nativeType => NativeType.GMSCENEINFOLIST;

        public GMSceneInfoList() {

        }

        public GMSceneInfoList(AC2Reader data) {
            data.ReadList(this, () => new GMSceneInfo(data));
        }

        public void write(AC2Writer data) {
            data.Write(this, v => v.write(data));
        }
    }
}
