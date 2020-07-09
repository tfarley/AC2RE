using AC2E.Interp;
using AC2E.WLib;
using System;
using System.IO;

namespace AC2E {

    public static class PackageFactory {

        public static IPackage read(PackageType packageType, BinaryReader data) {
            switch (packageType) {
                case PackageType.ActRegistry:
                    return new ActRegistryPkg(data);
                case PackageType.ContainerSegmentDescriptor:
                    return new ContainerSegmentDescriptorPkg(data);
                case PackageType.EffectRecord:
                    return new EffectRecordPkg(data);
                case PackageType.EffectRegistry:
                    return new EffectRegistryPkg(data);
                case PackageType.InventProfile:
                    return new InventProfilePkg(data);
                case PackageType.SkillInfo:
                    return new SkillInfoPkg(data);
                case PackageType.SkillRepository:
                    return new SkillRepositoryPkg(data);
                case PackageType.VisualDescInfo:
                    return new VisualDescInfoPkg(data);
                default:
                    throw new NotImplementedException($"Unhandled read for package type {packageType}");
            }
        }
    }
}
