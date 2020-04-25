using System;
using System.IO;

namespace AC2E.Interp {

    public interface IPackage {

        static readonly uint NULL = 0xFFFFFFFF;

        NativeType nativeType { get; }
        PackageType packageType { get; }
        InterpReference reference { get; }

        uint id { get; }
        IPackage[] references { get; }

        void write(BinaryWriter data) {
            throw new NotImplementedException();
        }
    }
}
