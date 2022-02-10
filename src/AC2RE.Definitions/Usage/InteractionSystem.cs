using System.Collections.Generic;

namespace AC2RE.Definitions;

public class InteractionSystem : IHeapObject {

    public PackageType packageType => PackageType.InteractionSystem;

    public Dictionary<uint, uint> errorPriorityHash; // m_errorPriorityHash

    public InteractionSystem(AC2Reader data) {
        data.ReadHO<AAHash>(v => errorPriorityHash = v);
    }
}
