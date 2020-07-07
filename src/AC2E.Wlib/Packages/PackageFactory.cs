using AC2E.Interp;
using System;
using System.IO;

namespace AC2E {

    public static class PackageFactory {

        public static IPackage read(PackageType packageType, BinaryReader data) {
            switch (packageType) {
                default:
                    throw new NotImplementedException($"Unhandled read for package type {packageType}");
            }
        }
    }
}
