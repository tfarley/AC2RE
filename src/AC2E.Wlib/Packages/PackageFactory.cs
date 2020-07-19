using AC2E.Def;
using AC2E.WLib;
using System;
using System.IO;

namespace AC2E {

    public static class PackageFactory {

        public static IPackage read(BinaryReader data, PackageType packageType, PackageRegistry registry) {
            switch (packageType) {
                case PackageType.AllegianceData:
                    return new AllegianceDataPkg(data, registry);
                case PackageType.AllegianceHierarchy:
                    return new AllegianceHierarchyPkg(data, registry);
                case PackageType.AllegianceNode:
                    return new AllegianceNodePkg(data, registry);
                case PackageType.AllegianceProfile:
                    return new AllegianceProfilePkg(data, registry);
                case PackageType.ActRegistry:
                    return new ActRegistryPkg(data, registry);
                case PackageType.ChannelData:
                    return new ChannelDataPkg(data, registry);
                case PackageType.ConsignerDesc:
                    return new ConsignerDescPkg(data, registry);
                case PackageType.Consignment:
                    return new ConsignmentPkg(data, registry);
                case PackageType.ContainerSegmentDescriptor:
                    return new ContainerSegmentDescriptorPkg(data);
                case PackageType.CraftRegistry:
                    return new CraftRegistryPkg(data, registry);
                case PackageType.CraftSkillRecord:
                    return new CraftSkillRecordPkg(data);
                case PackageType.EffectRecord:
                    return new EffectRecordPkg(data, registry);
                case PackageType.EffectRegistry:
                    return new EffectRegistryPkg(data, registry);
                case PackageType.EquipItemProfile:
                    return new EquipItemProfilePkg(data, registry);
                case PackageType.Fellow:
                    return new FellowPkg(data, registry);
                case PackageType.Fellowship:
                    return new FellowshipPkg(data, registry);
                case PackageType.FellowVitals:
                    return new FellowVitalsPkg(data, registry);
                case PackageType.GameSaleProfile:
                    return new GameSaleProfilePkg(data, registry);
                case PackageType.InventProfile:
                    return new InventProfilePkg(data, registry);
                case PackageType.InvEquipDesc:
                    return new InvEquipDescPkg(data, registry);
                case PackageType.InvMoveDesc:
                    return new InvMoveDescPkg(data, registry);
                case PackageType.InvTakeAllDesc:
                    return new InvTakeAllDescPkg(data, registry);
                case PackageType.InvTransmuteAllDesc:
                    return new InvTransmuteAllDescPkg(data, registry);
                case PackageType.PetData:
                    return new PetDataPkg(data);
                case PackageType.PlayerSaleProfile:
                    return new PlayerSaleProfilePkg(data, registry);
                case PackageType.RecipeRecord:
                    return new RecipeRecordPkg(data);
                case PackageType.ResurrectionRequest:
                    return new ResurrectionRequestPkg(data, registry);
                case PackageType.SaleProfile:
                    return new SaleProfilePkg(data, registry);
                case PackageType.SkillInfo:
                    return new SkillInfoPkg(data);
                case PackageType.SkillRepository:
                    return new SkillRepositoryPkg(data, registry);
                case PackageType.StoreView:
                    return new StoreViewPkg(data, registry);
                case PackageType.Trade:
                    return new TradePkg(data, registry);
                case PackageType.TransactionBlob:
                    return new TransactionBlobPkg(data, registry);
                case PackageType.UsageBlob:
                    return new UsageBlobPkg(data, registry);
                case PackageType.UsageDesc:
                    return new UsageDescPkg(data, registry);
                case PackageType.VisualDescInfo:
                    return new VisualDescInfoPkg(data, registry);
                default:
                    throw new NotImplementedException($"Unhandled read for package type {packageType}");
            }
        }
    }
}
