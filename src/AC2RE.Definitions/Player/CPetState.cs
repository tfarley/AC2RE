﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CPetState : IPackage {

    public PackageType packageType => PackageType.CPetState;

    public Dictionary<ulong, IPackage> pets; // m_hashPets

    public CPetState(AC2Reader data) {
        data.ReadPkg<LRHash>(v => pets = v);
    }
}
