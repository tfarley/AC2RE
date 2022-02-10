using System.Collections.Generic;

namespace AC2RE.Definitions;

// TODO: Make this generic for the ARHash?
public class SimpleMasterList : IHeapObject {

    public virtual PackageType packageType => PackageType.SimpleMasterList;

    public Dictionary<uint, IHeapObject> map; // mMap

    public SimpleMasterList(AC2Reader data) {
        data.ReadHO<ARHash>(v => map = v);
    }
}
