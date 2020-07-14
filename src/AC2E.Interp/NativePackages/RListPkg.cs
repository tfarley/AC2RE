﻿using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class RListPkg<T> : IPackage where T : IPackage {

        public NativeType nativeType => NativeType.RLIST;

        public List<T> contents;

        public RListPkg<U> to<U>() where U : class, IPackage {
            RListPkg<U> converted = new RListPkg<U>();
            if (contents != null) {
                converted.contents = new List<U>(contents.Count);
                foreach (var element in contents) {
                    converted.contents.Add(element as U);
                }
            }
            return converted;
        }

        public RListPkg() {

        }

        public RListPkg(BinaryReader data, PackageRegistry registry) {
            contents = new List<T>();
            foreach (var element in data.ReadList(data.ReadPackageId)) {
                registry.addResolver(() => contents.Add(registry.get<T>(element)));
            }
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, v => data.Write(v, registry));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
