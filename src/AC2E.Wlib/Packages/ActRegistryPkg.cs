using AC2E.Interp;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class ActRegistryPkg : IPackage {

        public PackageType packageType => PackageType.ActRegistry;

        public int m_viewingProtectionEID;
        public PkgRef<ARHashPkg<AListPkg>> m_actSceneTable;

        public ActRegistryPkg() {

        }

        public ActRegistryPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            m_viewingProtectionEID = data.ReadInt32();
            m_actSceneTable = data.ReadPkgRef<ARHashPkg<IPackage>, ARHashPkg<AListPkg>>(resolvers, v => v.to<AListPkg>());
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(m_viewingProtectionEID);
            data.Write(m_actSceneTable, references);
        }
    }
}
