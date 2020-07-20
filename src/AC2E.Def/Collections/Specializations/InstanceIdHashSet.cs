using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class InstanceIdHashSet : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.LAHASHSET;
        public object delegatedToStringObject => contents;

        public HashSet<InstanceId> contents;

        public InstanceIdHashSet() {

        }

        public InstanceIdHashSet(LAHashSet set) {
            contents = new HashSet<InstanceId>();
            foreach (var element in set.contents) {
                contents.Add(new InstanceId(element));
            }
        }

        public InstanceIdHashSet(AC2Reader data) {
            contents = data.ReadSet(data.ReadInstanceId);
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write);
        }
    }
}
