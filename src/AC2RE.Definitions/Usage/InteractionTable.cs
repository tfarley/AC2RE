using System.Collections.Generic;

namespace AC2RE.Definitions;

public class InteractionTable : IHeapObject {

    public PackageType packageType => PackageType.InteractionTable;

    public List<TargetInteraction> targetInteractions; // m_listTargetInteractions

    public InteractionTable(AC2Reader data) {
        data.ReadHO<RList>(v => targetInteractions = v.to<TargetInteraction>());
    }
}
