using System;

namespace AC2E.Def {

    public static class PackageManager {

        public static InterpReferenceMeta getReferenceMeta(Type type) {
            InterpReferenceMeta.Flag flags = InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE;
            if (type == typeof(SingletonPkg)) {
                flags |= InterpReferenceMeta.Flag.SINGLETON;
            }

            return new InterpReferenceMeta(flags, ReferenceType.HEAPOBJECT);
        }

        public static IPackage read(AC2Reader data, NativeType nativeType) {
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
                    return new ARHash<IPackage>(data);
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
                    return new LRHash<IPackage>(data);
                case NativeType.NRHASH:
                    return new NRHash<IPackage, IPackage>(data);
                case NativeType.POSITION:
                    return new Position(data);
                case NativeType.RLIST:
                    return new RList<IPackage>(data);
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

        public static IPackage read(AC2Reader data, PackageType packageType) {
            switch (packageType) {
                case PackageType.AllegianceData:
                    return new AllegianceData(data);
                case PackageType.AllegianceHierarchy:
                    return new AllegianceHierarchy(data);
                case PackageType.AllegianceNode:
                    return new AllegianceNode(data);
                case PackageType.AllegianceProfile:
                    return new AllegianceProfile(data);
                case PackageType.ActRegistry:
                    return new ActRegistry(data);
                case PackageType.ChannelData:
                    return new ChannelData(data);
                case PackageType.ConsignerDesc:
                    return new ConsignerDesc(data);
                case PackageType.Consignment:
                    return new Consignment(data);
                case PackageType.ContainerSegmentDescriptor:
                    return new ContainerSegmentDescriptor(data);
                case PackageType.CraftRegistry:
                    return new CraftRegistry(data);
                case PackageType.CraftSkillRecord:
                    return new CraftSkillRecord(data);
                case PackageType.EffectRecord:
                    return new EffectRecord(data);
                case PackageType.EffectRegistry:
                    return new EffectRegistry(data);
                case PackageType.EquipItemProfile:
                    return new EquipItemProfile(data);
                case PackageType.Fellow:
                    return new Fellow(data);
                case PackageType.Fellowship:
                    return new Fellowship(data);
                case PackageType.FellowVitals:
                    return new FellowVitals(data);
                case PackageType.GameSaleProfile:
                    return new GameSaleProfile(data);
                case PackageType.InventProfile:
                    return new InventProfile(data);
                case PackageType.InvEquipDesc:
                    return new InvEquipDesc(data);
                case PackageType.InvMoveDesc:
                    return new InvMoveDesc(data);
                case PackageType.InvTakeAllDesc:
                    return new InvTakeAllDesc(data);
                case PackageType.InvTransmuteAllDesc:
                    return new InvTransmuteAllDesc(data);
                case PackageType.PetData:
                    return new PetData(data);
                case PackageType.PlayerSaleProfile:
                    return new PlayerSaleProfile(data);
                case PackageType.RecipeRecord:
                    return new RecipeRecord(data);
                case PackageType.ResurrectionRequest:
                    return new ResurrectionRequest(data);
                case PackageType.SaleProfile:
                    return new SaleProfile(data);
                case PackageType.SkillInfo:
                    return new SkillInfo(data);
                case PackageType.SkillRepository:
                    return new SkillRepository(data);
                case PackageType.StoreView:
                    return new StoreView(data);
                case PackageType.Trade:
                    return new Trade(data);
                case PackageType.TransactionBlob:
                    return new TransactionBlob(data);
                case PackageType.UsageBlob:
                    return new UsageBlob(data);
                case PackageType.UsageDesc:
                    return new UsageDesc(data);
                case PackageType.VisualDescInfo:
                    return new VisualDescInfo(data);
                default:
                    throw new NotImplementedException($"Unhandled read for package type {packageType}");
            }
        }
    }
}
