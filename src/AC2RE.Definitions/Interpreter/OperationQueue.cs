using System.Collections.Generic;

namespace AC2RE.Definitions;

public class OperationQueue : IHeapObject {

    public PackageType packageType => PackageType.OperationQueue;

    public List<Operation> operations; // m_operations

    public OperationQueue(AC2Reader data) {
        data.ReadHO<RList>(v => operations = v.to<Operation>());
    }
}
