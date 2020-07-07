using AC2E.Interp;
using System;
using System.IO;

namespace AC2E {

    public static class NativePackageFactory {

        public static IPackage read(NativeType nativeType, BinaryReader data) {
            switch (nativeType) {
                case NativeType.AAHASH:
                    return new AAHashPkg(data);
                case NativeType.ALIST:
                    return new AListPkg(data);
                case NativeType.LLIST:
                    return new LListPkg(data);
                case NativeType.STRINGINFO:
                    return new StringInfoPkg(data);
                case NativeType.VECTOR:
                    return new VectorPkg(data);
                case NativeType.VISUALDESC:
                    return new VisualDescPkg(data);
                default:
                    throw new NotImplementedException($"Unhandled read for native package type {nativeType}");
            }
        }
    }
}
