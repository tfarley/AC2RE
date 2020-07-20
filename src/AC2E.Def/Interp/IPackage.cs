using System;

namespace AC2E.Def {

    public interface IPackage {

        NativeType nativeType => NativeType.UNDEF;
        PackageType packageType => PackageType.UNDEF;

        void write(AC2Writer data, PackageRegistry registry) {
            throw new NotImplementedException("IPackage implementor must override write().");
        }
    }
}
