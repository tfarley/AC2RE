using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

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
                    return new AAHash(data);
                case NativeType.AAMULTIHASH:
                    return new AAMultiHash(data);
                case NativeType.AHASHSET:
                    return new AHashSet(data);
                case NativeType.ALHASH:
                    return new ALHash(data);
                case NativeType.ALIST:
                    return new AList(data);
                case NativeType.APPINFOHASH:
                    return new AppInfoHash(data);
                case NativeType.ARHASH:
                    return new ARHash<IPackage>(data, registry);
                case NativeType.EXAMINATIONPROFILE:
                    return new ExaminationProfile(data);
                case NativeType.EXAMINATIONREQUEST:
                    return new ExaminationRequest(data);
                case NativeType.GAMEPLAYOPTIONSPROFILE:
                    return new GameplayOptionsProfile(data);
                case NativeType.GMKEYFRAME:
                    return new GMKeyframe(data);
                case NativeType.GMQUESTINFOLIST:
                    return new GMQuestInfoList(data);
                case NativeType.GMQUESTINFO:
                    return new GMQuestInfo(data);
                case NativeType.GMSCENEINFO:
                    return new GMSceneInfo(data);
                case NativeType.GMSCENEINFOLIST:
                    return new GMSceneInfoList(data);
                case NativeType.ICONDESC:
                    return new IconDesc(data);
                case NativeType.LAHASH:
                    return new LAHash(data);
                case NativeType.LAHASHSET:
                    return new LAHashSet(data);
                case NativeType.LLIST:
                    return new LList(data);
                case NativeType.LRHASH:
                    return new LRHash<IPackage>(data, registry);
                case NativeType.NRHASH:
                    return new NRHash<IPackage, IPackage>(data, registry);
                case NativeType.POSITION:
                    return new Position(data);
                case NativeType.RLIST:
                    return new RList<IPackage>(data, registry);
                case NativeType.SELECTIONINFO:
                    return new SelectionInfo(data);
                case NativeType.SHORTCUTINFO:
                    return new ShortcutInfo(data);
                case NativeType.STRINGINFO:
                    return new StringInfo(data);
                case NativeType.WPSTRING:
                    return new WPString(data);
                case NativeType.UISAVELOCATIONS:
                    return new UISaveLocations(data);
                case NativeType.VECTOR:
                    return data.ReadVector();
                case NativeType.VISUALDESC:
                    return new VisualDesc(data);
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
