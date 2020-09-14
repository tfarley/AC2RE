namespace AC2E.Def {

    public class ItemProfile : IPackage {

        public PackageType packageType => PackageType.ItemProfile;

        public InstanceId itemId; // itemID
        public InstanceId containerId; // containerID
        public uint slot; // slot

        public ItemProfile(AC2Reader data) {
            itemId = data.ReadInstanceId();
            containerId = data.ReadInstanceId();
            slot = data.ReadUInt32();
        }
    }
}
