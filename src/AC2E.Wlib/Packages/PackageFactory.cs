using AC2E.Def;
using AC2E.WLib;
using System;
using System.IO;

namespace AC2E {

    public static class PackageFactory {

        public static IPackage read(BinaryReader data, PackageType packageType, PackageRegistry registry) {
            switch (packageType) {
                case PackageType.ActRegistry:
                    return new ActRegistryPkg(data, registry);
                case PackageType.ChannelData:
                    return new ChannelDataPkg(data, registry);
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
                case PackageType.InventProfile:
                    return new InventProfilePkg(data, registry);
                case PackageType.InvEquipDesc:
                    return new InvEquipDescPkg(data, registry);
                case PackageType.InvTransmuteAllDesc:
                    return new InvTransmuteAllDescPkg(data, registry);
                case PackageType.RecipeRecord:
                    return new RecipeRecordPkg(data);
                case PackageType.SkillInfo:
                    return new SkillInfoPkg(data);
                case PackageType.SkillRepository:
                    return new SkillRepositoryPkg(data, registry);
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
