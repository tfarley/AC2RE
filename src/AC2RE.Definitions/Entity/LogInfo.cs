using System.Collections.Generic;

namespace AC2RE.Definitions;

public class LogInfo : IHeapObject {

    public PackageType packageType => PackageType.LogInfo;

    public Dictionary<uint, IHeapObject> broadcastBitmaskHash; // mBroadcastBitmaskHash
    public uint fileBitmask; // mFileBitmask
    public uint broadcastBitmask; // mBroadcastBitmask
    public Dictionary<InstanceId, uint> broadcastIdHash; // mBroadcastIIDHash
    public uint printBitmask; // mPrintBitmask
    public List<InstanceId> loggedEntities; // mLoggedEntities

    public LogInfo(AC2Reader data) {
        data.ReadHO<ARHash>(v => broadcastBitmaskHash = v);
        fileBitmask = data.ReadUInt32();
        broadcastBitmask = data.ReadUInt32();
        data.ReadHO<LAHash>(v => broadcastIdHash = v.to<InstanceId, uint>());
        printBitmask = data.ReadUInt32();
        data.ReadHO<LList>(v => loggedEntities = v.to<InstanceId>());
    }
}
