using System.Collections.Generic;

namespace AC2RE.Definitions;

// TODO: Make this generic for the ARHash?
public class MasterList : IHeapObject {

    public virtual PackageType packageType => PackageType.MasterList;

    public EnumId enumId; // mEmapperID
    public List<DataId> subDids; // mSubDataIDs
    public Dictionary<uint, SingletonPkg<IHeapObject>> map; // mMap

    public MasterList(AC2Reader data) {
        enumId = data.ReadEnumId();
        data.ReadHO<AArray>(v => subDids = v.to<DataId>());
        data.ReadHO<ARHash>(v => map = v.to<uint, SingletonPkg<IHeapObject>>());
    }
}
