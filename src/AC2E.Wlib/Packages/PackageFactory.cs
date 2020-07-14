using AC2E.Interp;
using AC2E.WLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E {

    public static class PackageFactory {

        public static IPackage read(PackageType packageType, BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            switch (packageType) {
                case PackageType.ActRegistry:
                    return new ActRegistryPkg(data, resolvers);
                case PackageType.ChannelData:
                    return new ChannelDataPkg(data, resolvers);
                case PackageType.ContainerSegmentDescriptor:
                    return new ContainerSegmentDescriptorPkg(data);
                case PackageType.CraftRegistry:
                    return new CraftRegistryPkg(data, resolvers);
                case PackageType.CraftSkillRecord:
                    return new CraftSkillRecordPkg(data);
                case PackageType.EffectRecord:
                    return new EffectRecordPkg(data, resolvers);
                case PackageType.EffectRegistry:
                    return new EffectRegistryPkg(data, resolvers);
                case PackageType.InventProfile:
                    return new InventProfilePkg(data, resolvers);
                case PackageType.RecipeRecord:
                    return new RecipeRecordPkg(data);
                case PackageType.SkillInfo:
                    return new SkillInfoPkg(data);
                case PackageType.SkillRepository:
                    return new SkillRepositoryPkg(data, resolvers);
                case PackageType.UsageBlob:
                    return new UsageBlobPkg(data, resolvers);
                case PackageType.UsageDesc:
                    return new UsageDescPkg(data, resolvers);
                case PackageType.VisualDescInfo:
                    return new VisualDescInfoPkg(data, resolvers);
                default:
                    throw new NotImplementedException($"Unhandled read for package type {packageType}");
            }
        }
    }
}
