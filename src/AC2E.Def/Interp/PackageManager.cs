using System;
using System.Collections.Generic;

namespace AC2E.Def {

    public static class PackageManager {

        private static readonly HashSet<Type> SINGLETON_PACKAGE_TYPES = new HashSet<Type> {
            typeof(SingletonPkg) // TODO: EffectPkg etc. instead of shared class?
        };

        public static InterpReferenceMeta getReferenceMeta(Type type) {
            InterpReferenceMeta.Flag flags = InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE;
            if (SINGLETON_PACKAGE_TYPES.Contains(type)) {
                flags |= InterpReferenceMeta.Flag.SINGLETON;
            }

            return new InterpReferenceMeta(flags, ReferenceType.HEAPOBJECT);
        }

        public static IPackage read(AC2Reader data, NativeType nativeType, PackageRegistry registry) {
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

        public static IPackage read(AC2Reader data, PackageType packageType, PackageRegistry registry) {
            switch (packageType) {
                case PackageType.AllegianceData:
                    return new AllegianceData(data, registry);
                case PackageType.AllegianceHierarchy:
                    return new AllegianceHierarchy(data, registry);
                case PackageType.AllegianceNode:
                    return new AllegianceNode(data, registry);
                case PackageType.AllegianceProfile:
                    return new AllegianceProfile(data, registry);
                case PackageType.ActRegistry:
                    return new ActRegistry(data, registry);
                case PackageType.ChannelData:
                    return new ChannelData(data, registry);
                case PackageType.ConsignerDesc:
                    return new ConsignerDesc(data, registry);
                case PackageType.Consignment:
                    return new Consignment(data, registry);
                case PackageType.ContainerSegmentDescriptor:
                    return new ContainerSegmentDescriptor(data);
                case PackageType.CraftRegistry:
                    return new CraftRegistry(data, registry);
                case PackageType.CraftSkillRecord:
                    return new CraftSkillRecord(data);
                case PackageType.EffectRecord:
                    return new EffectRecord(data, registry);
                case PackageType.EffectRegistry:
                    return new EffectRegistry(data, registry);
                case PackageType.EquipItemProfile:
                    return new EquipItemProfile(data, registry);
                case PackageType.Fellow:
                    return new Fellow(data, registry);
                case PackageType.Fellowship:
                    return new Fellowship(data, registry);
                case PackageType.FellowVitals:
                    return new FellowVitals(data, registry);
                case PackageType.GameSaleProfile:
                    return new GameSaleProfile(data, registry);
                case PackageType.InventProfile:
                    return new InventProfile(data, registry);
                case PackageType.InvEquipDesc:
                    return new InvEquipDesc(data, registry);
                case PackageType.InvMoveDesc:
                    return new InvMoveDesc(data, registry);
                case PackageType.InvTakeAllDesc:
                    return new InvTakeAllDesc(data, registry);
                case PackageType.InvTransmuteAllDesc:
                    return new InvTransmuteAllDesc(data, registry);
                case PackageType.PetData:
                    return new PetData(data);
                case PackageType.PlayerSaleProfile:
                    return new PlayerSaleProfile(data, registry);
                case PackageType.RecipeRecord:
                    return new RecipeRecord(data);
                case PackageType.ResurrectionRequest:
                    return new ResurrectionRequest(data, registry);
                case PackageType.SaleProfile:
                    return new SaleProfile(data, registry);
                case PackageType.SkillInfo:
                    return new SkillInfo(data);
                case PackageType.SkillRepository:
                    return new SkillRepository(data, registry);
                case PackageType.StoreView:
                    return new StoreView(data, registry);
                case PackageType.Trade:
                    return new Trade(data, registry);
                case PackageType.TransactionBlob:
                    return new TransactionBlob(data, registry);
                case PackageType.UsageBlob:
                    return new UsageBlob(data, registry);
                case PackageType.UsageDesc:
                    return new UsageDesc(data, registry);
                case PackageType.VisualDescInfo:
                    return new VisualDescInfo(data, registry);
                default:
                    throw new NotImplementedException($"Unhandled read for package type {packageType}");
            }
        }
    }
}
