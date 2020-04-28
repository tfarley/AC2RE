using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public interface IPackage {

        static readonly uint NULL = 0xFFFFFFFF;

        NativeType nativeType { get; }
        PackageType packageType { get; }
        InterpReferenceMeta referenceMeta { get; }

        uint id { get; }

        void write(BinaryWriter data, List<IPackage> referenceList) {
            throw new NotImplementedException();
        }
    }
}
