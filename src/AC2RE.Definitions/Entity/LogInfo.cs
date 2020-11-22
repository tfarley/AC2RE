using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class LogInfo : IPackage {

        public PackageType packageType => PackageType.LogInfo;

        public Dictionary<uint, IPackage> broadcastBitmaskHash; // mBroadcastBitmaskHash
        public uint fileBitmask; // mFileBitmask
        public uint broadcastBitmask; // mBroadcastBitmask
        public Dictionary<InstanceId, uint> broadcastIdHash; // mBroadcastIIDHash
        public uint printBitmask; // mPrintBitmask
        public List<InstanceId> loggedEntities; // mLoggedEntities

        public LogInfo(AC2Reader data) {
            data.ReadPkg<ARHash>(v => broadcastBitmaskHash = v);
            fileBitmask = data.ReadUInt32();
            broadcastBitmask = data.ReadUInt32();
            data.ReadPkg<LAHash>(v => broadcastIdHash = v.to<InstanceId, uint>());
            printBitmask = data.ReadUInt32();
            data.ReadPkg<LList>(v => loggedEntities = v.to<InstanceId>());
        }
    }
}
