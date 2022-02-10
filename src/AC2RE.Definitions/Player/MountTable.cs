using System.Collections.Generic;

namespace AC2RE.Definitions;

public class MountTable : IHeapObject {

    public PackageType packageType => PackageType.MountTable;

    public Dictionary<uint, Dictionary<uint, IHeapObject>> mountTable; // mMountTable

    public MountTable(AC2Reader data) {
        data.ReadHO<ARHash>(v => mountTable = v.to<uint, Dictionary<uint, IHeapObject>>());
    }
}
