using AC2E.Def;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public interface IPackage {

        NativeType nativeType { get; }
        PackageType packageType { get; }
        InterpReferenceMeta referenceMeta { get; }

        PackageId id { get; }

        void write(BinaryWriter data, List<IPackage> referenceList) {
            throw new NotImplementedException("IPackage implementor must override write().");
        }
    }
}
