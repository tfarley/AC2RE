using System.Collections.Generic;

namespace AC2RE.Definitions;

public static class PackageTypes {

    private static readonly DataId CLIENT_WLIB_DID = new(0x56000005);

    private static readonly List<PackageType> packageTypes = new();
    private static readonly Dictionary<PackageType, int> packageTypeToParentIndex = new();
    private static readonly Dictionary<PackageType, List<PackageType>> packageTypeToHierarchyCache = new();
    private static bool loaded;

    public static void loadPackageTypes(DatReader datReader) {
        if (!loaded) {
            using (AC2Reader data = datReader.getFileReader(CLIENT_WLIB_DID)) {
                WLib wlib = new(data);
                foreach (ByteStream.ExportData export in wlib.byteStream.exports) {
                    add(export.args.packageType, export.args.parentIndex);
                }
                foreach (PackageType packageType in packageTypes) {
                    packageTypeToHierarchyCache[packageType] = calculatePackageTypeHierarchy(packageType);
                }
            }
            loaded = true;
        }
    }

    public static bool isPackageType(PackageType packageType, PackageType comparePackageType) {
        if (packageType == comparePackageType) {
            return true;
        }

        List<PackageType> packageTypeHierarchy = getPackageTypeHierarchy(packageType);
        foreach (PackageType inheritedPackageType in packageTypeHierarchy) {
            if (inheritedPackageType == comparePackageType) {
                return true;
            }
        }

        return false;
    }

    public static List<PackageType> getPackageTypeHierarchy(PackageType packageType) {
        return packageTypeToHierarchyCache[packageType];
    }

    private static void add(PackageType packageType, int parentIndex) {
        packageTypes.Add(packageType);
        packageTypeToParentIndex[packageType] = parentIndex;
    }

    private static List<PackageType> calculatePackageTypeHierarchy(PackageType packageType) {
        List<PackageType> typeHierarchy = new();
        while (packageTypeToParentIndex.TryGetValue(packageType, out int parentIndex) && parentIndex != -1) {
            PackageType parentType = packageTypes[parentIndex];

            typeHierarchy.Add(parentType);
            if (packageTypeToHierarchyCache.TryGetValue(parentType, out List<PackageType> parentTypeHierarchy)) {
                typeHierarchy.AddRange(parentTypeHierarchy);
                break;
            } else {
                packageType = parentType;
            }
        }
        return typeHierarchy;
    }
}
