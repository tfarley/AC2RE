using System.Collections.Generic;

namespace AC2E.Def {

    public class PackageTypes {

        private readonly List<PackageTypeId> packageTypeIds = new List<PackageTypeId>();
        private readonly Dictionary<PackageTypeId, int> packageTypeIdToParentIndex = new Dictionary<PackageTypeId, int>();
        private readonly Dictionary<PackageTypeId, List<PackageTypeId>> packageTypeIdToHierarchyCache = new Dictionary<PackageTypeId, List<PackageTypeId>>();

        public void add(PackageTypeId packageTypeId, int parentIndex) {
            packageTypeIds.Add(packageTypeId);
            packageTypeIdToParentIndex[packageTypeId] = parentIndex;
        }

        public void calculate() {
            foreach (PackageTypeId packageTypeId in packageTypeIds) {
                packageTypeIdToHierarchyCache[packageTypeId] = calculatePackageTypeHierarchy(packageTypeId);
            }
        }

        private List<PackageTypeId> calculatePackageTypeHierarchy(PackageTypeId packageTypeId) {
            List<PackageTypeId> typeHierarchy = new List<PackageTypeId>();
            while (packageTypeIdToParentIndex.TryGetValue(packageTypeId, out int parentIndex) && parentIndex != -1) {
                PackageTypeId parentType = packageTypeIds[parentIndex];

                typeHierarchy.Add(parentType);
                if (packageTypeIdToHierarchyCache.TryGetValue(parentType, out List<PackageTypeId> parentTypeHierarchy)) {
                    typeHierarchy.AddRange(parentTypeHierarchy);
                    break;
                } else {
                    packageTypeId = parentType;
                }
            }
            return typeHierarchy;
        }

        public List<PackageTypeId> getPackageTypeHierarchy(PackageTypeId packageTypeId) {
            return packageTypeIdToHierarchyCache[packageTypeId];
        }
    }
}
