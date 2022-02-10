using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CAdmin : CPlayer {

    public override PackageType packageType => PackageType.CAdmin;

    public Dictionary<uint, IHeapObject> logHash; // m_hashLog

    public CAdmin(AC2Reader data) : base(data) {
        data.ReadHO<ARHash>(v => logHash = v);
    }
}
