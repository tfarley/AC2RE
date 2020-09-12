namespace AC2E.Def {

    // TODO: Make this generic for the ARHash?
    public class MasterList : IPackage {

        public PackageType packageType => PackageType.MasterList;

        public uint emapperID; // mEmapperID
        public DataIdArray subDids; // mSubDataIDs
        public ARHash<IPackage> map; // mMap

        public MasterList(AC2Reader data) {
            emapperID = data.ReadUInt32();
            data.ReadPkg<AArray>(v => subDids = new DataIdArray(v));
            data.ReadPkg<ARHash<IPackage>>(v => map = v);
        }
    }
}
