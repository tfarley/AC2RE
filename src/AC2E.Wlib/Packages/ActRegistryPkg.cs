using AC2E.Interp;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class ActRegistryPkg : IPackage {

        public PackageType packageType => PackageType.ActRegistry;

        public int m_viewingProtectionEID;
        public ARHashPkg<AListPkg> m_actSceneTable;

        public ActRegistryPkg() {

        }

        public ActRegistryPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            m_viewingProtectionEID = data.ReadInt32();
            data.ReadPkgRef<ARHashPkg<IPackage>>(v => m_actSceneTable = v.to<AListPkg>(), resolvers);
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(m_viewingProtectionEID);
            data.Write(m_actSceneTable, references);
        }
    }
}
