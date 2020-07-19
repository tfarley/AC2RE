using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class InstanceIdList : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.LLIST;
        public object delegatedToStringObject => contents;

        public List<InstanceId> contents;

        public InstanceIdList() {

        }

        public InstanceIdList(LList list) {
            contents = new List<InstanceId>();
            foreach (var element in list.contents) {
                contents.Add(new InstanceId(element));
            }
        }

        public InstanceIdList(BinaryReader data) {
            contents = data.ReadList(data.ReadInstanceId);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write);
        }
    }
}
