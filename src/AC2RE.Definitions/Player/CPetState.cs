using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CPetState : IHeapObject {

    public PackageType packageType => PackageType.CPetState;

    public Dictionary<ulong, IHeapObject> pets; // m_hashPets

    public CPetState(AC2Reader data) {
        data.ReadHO<LRHash>(v => pets = v);
    }
}
