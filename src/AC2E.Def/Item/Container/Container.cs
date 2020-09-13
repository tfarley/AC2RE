namespace AC2E.Def {

    public class Container : gmCEntity {

        public override PackageType packageType => PackageType.Container;

        public InstanceIdList contents; // mContents
        public InstanceId transactionOnBehalfOfId; // m_transactionOnBehalfOfID
        public RList<IPackage> segments; // mSegments
        public InstanceIdList containers; // mContainers
        public bool hasBeenOpened; // mHasBeenOpened

        public Container(AC2Reader data) : base(data) {
            data.ReadPkg<LList>(v => contents = new InstanceIdList(v));
            transactionOnBehalfOfId = data.ReadInstanceId();
            data.ReadPkg<RList<IPackage>>(v => segments = v);
            data.ReadPkg<LList>(v => containers = new InstanceIdList(v));
            hasBeenOpened = data.ReadBoolean();
        }
    }
}
