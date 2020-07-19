﻿using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class RList<T> : IPackage, IDelegateToString where T : IPackage {

        public NativeType nativeType => NativeType.RLIST;
        public object delegatedToStringObject => contents;

        public List<T> contents;

        public RList<U> to<U>() where U : class, IPackage {
            RList<U> converted = new RList<U>();
            if (contents != null) {
                converted.contents = new List<U>(contents.Count);
                foreach (var element in contents) {
                    converted.contents.Add(element as U);
                }
            }
            return converted;
        }

        public RList() {

        }

        public RList(BinaryReader data, PackageRegistry registry) {
            contents = new List<T>();
            foreach (var element in data.ReadList(data.ReadPackageId)) {
                registry.addResolver(() => contents.Add(registry.get<T>(element)));
            }
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, v => data.Write(v, registry));
        }
    }
}
