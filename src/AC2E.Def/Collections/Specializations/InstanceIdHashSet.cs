using System.Collections.Generic;

namespace AC2E.Def {

    public class InstanceIdHashSet : HashSet<InstanceId>, IPackage {

        public NativeType nativeType => NativeType.LAHASHSET;

        public InstanceIdHashSet() {

        }

        public InstanceIdHashSet(LAHashSet set) {
            foreach (var element in set) {
                Add(new InstanceId(element));
            }
        }

        public InstanceIdHashSet(AC2Reader data) {
            data.ReadSet(this, data.ReadInstanceId);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
