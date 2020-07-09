using AC2E.Def;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public static class PackageManager {

        private static readonly Dictionary<PackageId, IPackage> registry = new Dictionary<PackageId, IPackage>();

        private static Func<PackageType, BinaryReader, IPackage> packageFactory;

        public static void init(Func<PackageType, BinaryReader, IPackage> packageFactory) {
            registry.Clear();

            PackageManager.packageFactory = packageFactory;
        }

        public static PackageId add(IPackage package) {
            registry[package.id] = package;
            return package.id;
        }

        public static T get<T>(PackageId packageId) where T : IPackage {
            return (T)registry.GetValueOrDefault(packageId, null);
        }

        public static IPackage read(PackageId packageId, NativeType nativeType, BinaryReader data) {
            IPackage newPackage = readInternal(nativeType, data);
            newPackage.id = packageId;
            add(newPackage);
            return newPackage;
        }

        public static IPackage readInternal(NativeType nativeType, BinaryReader data) {
            switch (nativeType) {
                case NativeType.AAHASH:
                    return new AAHashPkg(data);
                case NativeType.AAMULTIHASH:
                    return new AAMultiHashPkg(data);
                case NativeType.AHASHSET:
                    return new AHashSetPkg(data);
                case NativeType.ALIST:
                    return new AListPkg(data);
                case NativeType.APPINFOHASH:
                    return new AppInfoHashPkg(data);
                case NativeType.ARHASH:
                    return new ARHashPkg(data);
                case NativeType.GAMEPLAYOPTIONSPROFILE:
                    return new GameplayOptionsProfilePkg(data);
                case NativeType.GMQUESTINFOLIST:
                    return new GMQuestInfoListPkg(data);
                case NativeType.GMQUESTINFO:
                    return new GMQuestInfoPkg(data);
                case NativeType.LLIST:
                    return new LListPkg(data);
                case NativeType.LRHASH:
                    return new LRHashPkg(data);
                case NativeType.RLIST:
                    return new RListPkg(data);
                case NativeType.SHORTCUTINFO:
                    return new ShortcutInfoPkg(data);
                case NativeType.STRINGINFO:
                    return new StringInfoPkg(data);
                case NativeType.UISAVELOCATIONS:
                    return new UISaveLocationsPkg(data);
                case NativeType.VECTOR:
                    return new VectorPkg(data);
                case NativeType.VISUALDESC:
                    return new VisualDescPkg(data);
                default:
                    throw new NotImplementedException($"Unhandled read for native package type {nativeType}");
            }
        }

        public static IPackage read(PackageId packageId, PackageType nativeType, BinaryReader data) {
            if (packageFactory == null) {
                throw new InvalidOperationException("Attempted to read a package when the package manager has not been initialized with a factory");
            }

            IPackage newPackage = packageFactory.Invoke(nativeType, data);
            newPackage.id = packageId;
            add(newPackage);
            return newPackage;
        }
    }
}
