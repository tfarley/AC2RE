using System.Collections.Generic;

namespace AC2E.Def {

    // TODO: Make this generic for the ARHash?
    public class MasterList : IPackage {

        public virtual PackageType packageType => PackageType.MasterList;

        public uint emapperID; // mEmapperID
        public List<DataId> subDids; // mSubDataIDs
        public Dictionary<uint, IPackage> map; // mMap

        public MasterList(AC2Reader data) {
            emapperID = data.ReadUInt32();
            data.ReadPkg<AArray>(v => subDids = v.to<DataId>());
            data.ReadPkg<ARHash>(v => map = v);
        }
    }
}
