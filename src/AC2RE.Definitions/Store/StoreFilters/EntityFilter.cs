using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EntityFilter : IHeapObject {

    public PackageType packageType => PackageType.EntityFilter;

    public List<DataId> entityDids; // m_entityDIDs

    public EntityFilter(AC2Reader data) {
        data.ReadHO<AList>(v => entityDids = v.to<DataId>());
    }
}
