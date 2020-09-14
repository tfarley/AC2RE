namespace AC2E.Def {

    public class LevelTable : IPackage {

        public PackageType packageType => PackageType.LevelTable;

        public RArray<LevelData> map; // mMap
        public uint maxLevel; // mMaxLevel

        public LevelTable(AC2Reader data) {
            data.ReadPkg<RArray<IPackage>>(v => map = v.to<LevelData>());
            maxLevel = data.ReadUInt32();
        }
    }
}
