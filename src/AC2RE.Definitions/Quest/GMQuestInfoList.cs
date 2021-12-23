using System.Collections.Generic;

namespace AC2RE.Definitions;

public class GMQuestInfoList : List<GMQuestInfo>, IPackage {

    public NativeType nativeType => NativeType.gmQuestInfoList;

    public GMQuestInfoList() {

    }

    public GMQuestInfoList(AC2Reader data) {
        data.ReadList(this, () => new GMQuestInfo(data));
    }

    public void write(AC2Writer data) {
        data.Write(this, v => v.write(data));
    }
}
