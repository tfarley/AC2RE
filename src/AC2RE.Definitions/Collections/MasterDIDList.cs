using System.Collections.Generic;

namespace AC2RE.Definitions;

public class MasterDIDList : IHeapObject {

    public PackageType packageType => PackageType.MasterDIDList;

    public EnumId emapperId; // mEmapperID
    public Dictionary<uint, DataId> map; // mMap

    public MasterDIDList(AC2Reader data) {
        emapperId = data.ReadEnumId();
        data.ReadHO<AAHash>(v => map = v.to<uint, DataId>());
    }
}
