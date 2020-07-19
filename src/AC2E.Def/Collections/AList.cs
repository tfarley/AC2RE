﻿using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class AList : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.ALIST;
        public object delegatedToStringObject => contents;

        public List<uint> contents;

        public AList() {

        }

        public AList(BinaryReader data) {
            contents = data.ReadList(data.ReadUInt32);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write);
        }
    }
}
