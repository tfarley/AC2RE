using System.Collections.Generic;

namespace AC2E.Def {

    public class InstanceIdList : List<InstanceId>, IPackage {

        public NativeType nativeType => NativeType.LLIST;

        public InstanceIdList() {

        }

        public InstanceIdList(LList list) {
            foreach (var element in list) {
                Add(new InstanceId(element));
            }
        }

        public InstanceIdList(List<InstanceId> list) {
            foreach (var element in list) {
                Add(element);
            }
        }

        public InstanceIdList(AC2Reader data) {
            data.ReadList(this, data.ReadInstanceId);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
