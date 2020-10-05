﻿using System.Collections.Generic;

namespace AC2E.Def {

    public class InteractionSystem : IPackage {

        public PackageType packageType => PackageType.InteractionSystem;

        public Dictionary<uint, uint> errorPriorityHash; // m_errorPriorityHash

        public InteractionSystem(AC2Reader data) {
            data.ReadPkg<AAHash>(v => errorPriorityHash = v);
        }
    }
}
