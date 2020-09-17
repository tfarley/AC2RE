namespace AC2E.Def {

    public class AppearanceProfile : IPackage {

        public PackageType packageType => PackageType.AppearanceProfile;

        public float modifier; // modifier
        public AppearanceKey appKey; // appKey
        public DataId aprDid; // appFileDID

        public AppearanceProfile(AC2Reader data) {
            modifier = data.ReadSingle();
            appKey = (AppearanceKey)data.ReadUInt32();
            aprDid = data.ReadDataId();
        }
    }
}
