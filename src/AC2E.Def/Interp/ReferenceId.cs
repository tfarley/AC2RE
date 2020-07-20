﻿namespace AC2E.Def {

    public class ReferenceId {

        // ReferenceIDWrapper
        public PackageId id; // m_id
        public uint cachedHashValue; // m_CachedHashValue
        public bool useCache; // m_bUseCache

        public ReferenceId(PackageId id) {
            this.id = id;
        }

        public ReferenceId(AC2Reader data) {
            id = data.ReadPackageId();
            cachedHashValue = data.ReadUInt32();
            useCache = data.ReadBoolean();
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(registry.get<IPackage>(id), registry);
            data.Write(cachedHashValue);
            data.Write(useCache);
        }
    }
}
