using System;
using System.Collections.Generic;

namespace AC2E.Def {

    public class PackageRegistry {

        public struct PackageMeta {

            public PackageId id;
            public InterpReferenceMeta referenceMeta;
            public IPackage package;
        }

        private readonly Dictionary<IPackage, PackageMeta> registryByPackage = new Dictionary<IPackage, PackageMeta>();
        private readonly Dictionary<PackageId, PackageMeta> registryById = new Dictionary<PackageId, PackageMeta>();
        private uint packageIdCounter;

        private readonly List<Action> resolvers = new List<Action>();
        public readonly List<IPackage> references = new List<IPackage>();

        public bool contains(IPackage package) {
            return registryByPackage.ContainsKey(package);
        }

        public bool contains(PackageId packageId) {
            return registryById.ContainsKey(packageId);
        }

        public PackageMeta register(IPackage package) {
            PackageId packageId = new PackageId(packageIdCounter);
            packageIdCounter++;

            return register(packageId, package);
        }

        public PackageMeta register(PackageId packageId, IPackage package) {
            return register(packageId, package, PackageManager.getReferenceMeta(package.GetType()));
        }

        public PackageMeta register(PackageId packageId, IPackage package, InterpReferenceMeta referenceMeta) {
            PackageMeta meta = new PackageMeta {
                id = packageId,
                referenceMeta = referenceMeta,
                package = package,
            };
            registryByPackage[package] = meta;
            registryById[packageId] = meta;
            return meta;
        }

        public PackageId getId(IPackage package) {
            if (package == null) {
                return PackageId.NULL;
            }

            return getMeta(package).id;
        }

        public T get<T>(PackageId packageId) where T : IPackage {
            if (packageId.id == PackageId.NULL.id) {
                return default;
            }

            return (T)getMeta(packageId).package;
        }

        public PackageMeta getMeta(IPackage package) {
            if (package == null) {
                return default;
            }

            if (!registryByPackage.TryGetValue(package, out PackageMeta meta)) {
                meta = register(package);
            }
            return meta;
        }

        public PackageMeta getMeta(PackageId packageId) {
            if (packageId.id == PackageId.NULL.id) {
                return default;
            }

            if (registryById.TryGetValue(packageId, out PackageMeta meta)) {
                return meta;
            }
            throw new KeyNotFoundException();
        }

        public void addResolver(Action resolver) {
            resolvers.Add(resolver);
        }

        public void executeResolvers() {
            // Reverse resolvers to ensure we convert the nested packages before the parents
            resolvers.Reverse();
            foreach (Action resolver in resolvers) {
                resolver.Invoke();
            }
            resolvers.Clear();
        }
    }
}
