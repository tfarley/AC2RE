using System.Collections.Generic;

namespace AC2RE.Definitions;

public class GMSceneInfoList : List<GMSceneInfo>, IHeapObject {

    public NativeType nativeType => NativeType.gmSceneInfoList;

    public GMSceneInfoList() {

    }

    public GMSceneInfoList(AC2Reader data) {
        data.ReadList(this, () => new GMSceneInfo(data));
    }

    public void write(AC2Writer data) {
        data.Write(this, v => v.write(data));
    }
}
