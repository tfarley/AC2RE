namespace AC2E.Def {

    public class AppearanceProfile : IPackage {

        public PackageType packageType => PackageType.AppearanceProfile;

        public float modifier; // modifier
        public uint appKey; // appKey
        public DataId appFileDid; // appFileDID

        public AppearanceProfile(AC2Reader data) {
            modifier = data.ReadSingle();
            appKey = data.ReadUInt32();
            appFileDid = data.ReadDataId();
        }
    }
}
