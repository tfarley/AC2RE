﻿namespace AC2E.Def {

    public class Operation : IPackage {

        public PackageType packageType => PackageType.Operation;

        public float operand; // m_operand

        public Operation(AC2Reader data) {
            operand = data.ReadSingle();
        }
    }
}
