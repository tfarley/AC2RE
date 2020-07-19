using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class GMSceneInfoList : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.GMSCENEINFOLIST;
        public object delegatedToStringObject => contents;

        public List<GMSceneInfo> contents;

        public GMSceneInfoList() {

        }

        public GMSceneInfoList(BinaryReader data) {
            contents = data.ReadList(() => new GMSceneInfo(data));
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, v => v.write(data, registry));
        }
    }
}
