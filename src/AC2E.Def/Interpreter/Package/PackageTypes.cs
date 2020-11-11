using System.Collections.Generic;

namespace AC2E.Def {

    public class PackageTypes {

        private readonly List<PackageType> packageTypes = new();
        private readonly Dictionary<PackageType, int> packageTypeToParentIndex = new();
        private readonly Dictionary<PackageType, List<PackageType>> packageTypeToHierarchyCache = new();

        public void add(PackageType packageType, int parentIndex) {
            packageTypes.Add(packageType);
            packageTypeToParentIndex[packageType] = parentIndex;
        }

        public void calculate() {
            foreach (PackageType packageType in packageTypes) {
                packageTypeToHierarchyCache[packageType] = calculatePackageTypeHierarchy(packageType);
            }
        }

        private List<PackageType> calculatePackageTypeHierarchy(PackageType packageType) {
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

        public List<PackageType> getPackageTypeHierarchy(PackageType packageType) {
            return packageTypeToHierarchyCache[packageType];
        }
    }
}
