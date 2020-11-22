using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class MountTable : IPackage {

        public PackageType packageType => PackageType.MountTable;

        public Dictionary<uint, Dictionary<uint, IPackage>> mountTable; // mMountTable

        public MountTable(AC2Reader data) {
            data.ReadPkg<ARHash>(v => mountTable = v.to<uint, Dictionary<uint, IPackage>>());
        }
    }
}
