using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public static class PackageManager {

        private static readonly HashSet<Type> SINGLETON_PACKAGE_TYPES = new HashSet<Type> {
            typeof(SingletonPkg) // TODO: EffectPkg etc. instead of shared class?
        };

        private static Func<BinaryReader, PackageType, PackageRegistry, IPackage> packageFactory;

        public static void init(Func<BinaryReader, PackageType, PackageRegistry, IPackage> packageFactory) {
            PackageManager.packageFactory = packageFactory;
        }

        public static InterpReferenceMeta getReferenceMeta(Type type) {
            InterpReferenceMeta.Flag flags = InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE;
            if (SINGLETON_PACKAGE_TYPES.Contains(type)) {
                flags |= InterpReferenceMeta.Flag.SINGLETON;
            }

            return new InterpReferenceMeta(flags, ReferenceType.HEAPOBJECT);
        }

        public static IPackage read(BinaryReader data, NativeType nativeType, PackageRegistry registry) {
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
                    return new ARHashPkg<IPackage>(data, registry);
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
                    return new LRHashPkg<IPackage>(data, registry);
                case NativeType.POSITION:
                    return new PositionPkg(data);
                case NativeType.RLIST:
                    return new RListPkg<IPackage>(data, registry);
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

        public static IPackage read(BinaryReader data, PackageType packageType, PackageRegistry registry) {
            if (packageFactory == null) {
                throw new InvalidOperationException("Attempted to read a package when the package manager has not been initialized with a factory");
            }

            return packageFactory.Invoke(data, packageType, registry);
        }
    }
}
