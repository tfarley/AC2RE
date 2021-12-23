﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class RecipeCostTable : IPackage {

    public PackageType packageType => PackageType.RecipeCostTable;

    public uint maxLevel; // m_maxLevel
    public Dictionary<uint, RecipeCostData> map; // m_map

    public RecipeCostTable(AC2Reader data) {
        maxLevel = data.ReadUInt32();
        data.ReadPkg<ARHash>(v => map = v.to<uint, RecipeCostData>());
    }
}
