using System.IO;

namespace AC2E.Def {

    public class ReferenceId {

        // ReferenceIDWrapper
        public PackageId id; // m_id
        public uint cachedHashValue; // m_CachedHashValue
        public bool useCache; // m_bUseCache

        public ReferenceId(PackageId id) {
            this.id = id;
        }

        public ReferenceId(BinaryReader data) {
            id = data.ReadPackageId();
            cachedHashValue = data.ReadUInt32();
            useCache = data.ReadUInt32() != 0;
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(registry.get<IPackage>(id), registry);
            data.Write(cachedHashValue);
            data.Write(useCache);
        }
    }
}
