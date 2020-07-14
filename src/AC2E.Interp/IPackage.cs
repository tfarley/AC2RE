using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public interface IPackage {

        NativeType nativeType => NativeType.UNDEF;
        PackageType packageType => PackageType.UNDEF;

        void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            throw new NotImplementedException("IPackage implementor must override write().");
        }
    }
}
