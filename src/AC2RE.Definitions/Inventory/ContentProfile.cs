namespace AC2RE.Definitions {

    public class ContentProfile : IPackage {

        public PackageType packageType => PackageType.ContentProfile;

        public InstanceId id; // iid
        public bool isContainer; // is_container

        public ContentProfile(AC2Reader data) {
            id = data.ReadInstanceId();
            isContainer = data.ReadBoolean();
        }
    }
}
