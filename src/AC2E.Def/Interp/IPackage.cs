using System;
using System.IO;

namespace AC2E.Def {

    public interface IPackage {

        NativeType nativeType => NativeType.UNDEF;
        PackageType packageType => PackageType.UNDEF;

        void write(BinaryWriter data, PackageRegistry registry) {
            throw new NotImplementedException("IPackage implementor must override write().");
        }
    }
}
