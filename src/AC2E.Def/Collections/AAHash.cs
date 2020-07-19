﻿using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class AAHash : IPackage, IDelegateToString {

        public NativeType nativeType => NativeType.AAHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<uint, uint> contents;

        public AAHash() {

        }

        public AAHash(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write, data.Write);
        }
    }
}
