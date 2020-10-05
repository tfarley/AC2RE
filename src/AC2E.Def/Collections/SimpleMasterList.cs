using System.Collections.Generic;

namespace AC2E.Def {

    // TODO: Make this generic for the ARHash?
    public class SimpleMasterList : IPackage {

        public virtual PackageType packageType => PackageType.SimpleMasterList;

        public Dictionary<uint, IPackage> map; // mMap

        public SimpleMasterList(AC2Reader data) {
            data.ReadPkg<ARHash>(v => map = v);
        }
    }
}
