﻿using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

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

        public InstanceIdHashSet(BinaryReader data) {
            contents = data.ReadSet(data.ReadInstanceId);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write);
        }
    }
}
