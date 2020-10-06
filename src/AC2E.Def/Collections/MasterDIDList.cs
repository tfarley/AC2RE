using System.Collections.Generic;

namespace AC2E.Def {

    public class MasterDIDList : IPackage {

        public PackageType packageType => PackageType.MasterDIDList;

        public EnumId emapperId; // mEmapperID
        public Dictionary<uint, DataId> map; // mMap

        public MasterDIDList(AC2Reader data) {
            emapperId = data.ReadEnumId();
            data.ReadPkg<AAHash>(v => map = v.to<uint, DataId>());
        }
    }
}
