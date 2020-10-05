using System.Collections.Generic;

namespace AC2E.Def {

    public class MasterDIDList : IPackage {

        public PackageType packageType => PackageType.MasterDIDList;

        public uint emapperID; // mEmapperID
        public Dictionary<uint, uint> map; // mMap

        public MasterDIDList(AC2Reader data) {
            emapperID = data.ReadUInt32();
            data.ReadPkg<AAHash>(v => map = v);
        }
    }
}
