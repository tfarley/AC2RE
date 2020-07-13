using AC2E.Def;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public static class PackageManager {

        public struct PackageMeta {

            public PackageId id;
            public InterpReferenceMeta referenceMeta;
            public IPackage package;
        }

        private static readonly HashSet<Type> SINGLETON_PACKAGE_TYPES = new HashSet<Type> {
            typeof(SingletonPkg) // TODO: EffectPkg etc. instead of shared class?
        };

        private static readonly Dictionary<IPackage, PackageMeta> registryByPackage = new Dictionary<IPackage, PackageMeta>();
        private static readonly Dictionary<PackageId, PackageMeta> registryById = new Dictionary<PackageId, PackageMeta>();
        private static uint packageIdCounter;

        private static Func<PackageType, BinaryReader, IPackage> packageFactory;

        public static void init(Func<PackageType, BinaryReader, IPackage> packageFactory) {
            registryByPackage.Clear();
            registryById.Clear();

            PackageManager.packageFactory = packageFactory;
        }

        public static bool contains(IPackage package) {
            return registryByPackage.ContainsKey(package);
        }

        public static bool contains(PackageId packageId) {
            return registryById.ContainsKey(packageId);
        }

        public static PackageMeta register(IPackage package) {
            PackageId packageId = new PackageId(packageIdCounter);
            packageIdCounter++;

            return register(packageId, package);
        }

        public static PackageMeta register(PackageId packageId, IPackage package) {
            return register(packageId, package, getReferenceMeta(package.GetType()));
        }

        public static void convert<T>(PackageId packageId, Func<T, IPackage> converter) where T : IPackage {
            if (packageId.id == PackageId.NULL.id) {
                return;
            }

            PackageMeta newMeta = registryById[packageId];
            T oldPackage = (T)newMeta.package;
            IPackage newPackage = converter.Invoke(oldPackage);
            newMeta.package = newPackage;

            registryById[packageId] = newMeta;
            registryByPackage.Remove(oldPackage);
            registryByPackage[newPackage] = newMeta;
        }

        public static PackageMeta register(PackageId packageId, IPackage package, InterpReferenceMeta referenceMeta) {
            PackageMeta meta = new PackageMeta {
                id = packageId,
                referenceMeta = referenceMeta,
                package = package,
            };
            registryByPackage[package] = meta;
            registryById[packageId] = meta;
            return meta;
        }

        public static PackageId getId(IPackage package) {
            if (package == null) {
                return PackageId.NULL;
            }

            return getMeta(package).id;
        }

        public static T get<T>(PackageId packageId) where T : IPackage {
            if (packageId.id == PackageId.NULL.id) {
                return default;
            }

            return (T)getMeta(packageId).package;
        }

        public static PackageMeta getMeta(IPackage package) {
            if (package == null) {
                return default;
            }

            if (!registryByPackage.TryGetValue(package, out PackageMeta meta)) {
                meta = register(package);
            }
            return meta;
        }

        public static PackageMeta getMeta(PackageId packageId) {
            if (packageId.id == PackageId.NULL.id) {
                return default;
            }

            if (registryById.TryGetValue(packageId, out PackageMeta meta)) {
                return meta;
            }
            throw new KeyNotFoundException();
        }

        private static InterpReferenceMeta getReferenceMeta(Type type) {
            InterpReferenceMeta.Flag flags = InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE;
            if (SINGLETON_PACKAGE_TYPES.Contains(type)) {
                flags |= InterpReferenceMeta.Flag.SINGLETON;
            }

            return new InterpReferenceMeta(flags, ReferenceType.HEAPOBJECT);
        }

        public static IPackage read(NativeType nativeType, BinaryReader data) {
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
                    return new ARHashPkg<IPackage>(data);
                case NativeType.EXAMINATIONPROFILE:
                    return new ExaminationProfilePkg(data);
                case NativeType.EXAMINATIONREQUEST:
                    return new ExaminationRequestPkg(data);
                case NativeType.GAMEPLAYOPTIONSPROFILE:
                    return new GameplayOptionsProfilePkg(data);
                case NativeType.GMQUESTINFOLIST:
                    return new GMQuestInfoListPkg(data);
                case NativeType.GMQUESTINFO:
                    return new GMQuestInfoPkg(data);
                case NativeType.LLIST:
                    return new LListPkg(data);
                case NativeType.LRHASH:
                    return new LRHashPkg<IPackage>(data);
                case NativeType.POSITION:
                    return new PositionPkg(data);
                case NativeType.RLIST:
                    return new RListPkg<IPackage>(data);
                case NativeType.SHORTCUTINFO:
                    return new ShortcutInfoPkg(data);
                case NativeType.STRINGINFO:
                    return new StringInfoPkg(data);
                case NativeType.WPSTRING:
                    return new StringPkg(data);
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

        public static IPackage read(PackageType nativeType, BinaryReader data) {
            if (packageFactory == null) {
                throw new InvalidOperationException("Attempted to read a package when the package manager has not been initialized with a factory");
            }

            return packageFactory.Invoke(nativeType, data);
        }
    }
}
