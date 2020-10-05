using System.Collections.Generic;

namespace AC2E.Def {

    public class LevelTable : IPackage {

        public PackageType packageType => PackageType.LevelTable;

        public List<LevelData> map; // mMap
        public uint maxLevel; // mMaxLevel

        public LevelTable(AC2Reader data) {
            data.ReadPkg<RArray>(v => map = v.to<LevelData>());
            maxLevel = data.ReadUInt32();
        }
    }
}
