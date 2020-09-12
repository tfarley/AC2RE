namespace AC2E.Def {

    public class LogInfo : IPackage {

        public PackageType packageType => PackageType.LogInfo;

        public ARHash<IPackage> broadcastBitmaskHash; // mBroadcastBitmaskHash
        public uint fileBitmask; // mFileBitmask
        public uint broadcastBitmask; // mBroadcastBitmask
        public InstanceIdAHash broadcastIdHash; // mBroadcastIIDHash
        public uint printBitmask; // mPrintBitmask
        public InstanceIdList loggedEntities; // mLoggedEntities

        public LogInfo(AC2Reader data) {
            data.ReadPkg<ARHash<IPackage>>(v => broadcastBitmaskHash = v);
            fileBitmask = data.ReadUInt32();
            broadcastBitmask = data.ReadUInt32();
            data.ReadPkg<LAHash>(v => broadcastIdHash = new InstanceIdAHash(v));
            printBitmask = data.ReadUInt32();
            data.ReadPkg<LList>(v => loggedEntities = new InstanceIdList(v));
        }
    }
}
