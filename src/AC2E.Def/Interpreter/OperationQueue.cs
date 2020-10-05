using System.Collections.Generic;

namespace AC2E.Def {

    public class OperationQueue : IPackage {

        public PackageType packageType => PackageType.OperationQueue;

        public List<Operation> operations; // m_operations

        public OperationQueue(AC2Reader data) {
            data.ReadPkg<RList>(v => operations = v.to<Operation>());
        }
    }
}
