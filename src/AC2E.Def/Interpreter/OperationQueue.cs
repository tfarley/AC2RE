﻿namespace AC2E.Def {

    public class OperationQueue : IPackage {

        public PackageType packageType => PackageType.OperationQueue;

        public RList<Operation> operations; // m_operations

        public OperationQueue(AC2Reader data) {
            data.ReadPkg<RList<IPackage>>(v => operations = v.to<Operation>());
        }
    }
}
