﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ClassFilter : IPackage {

    public PackageType packageType => PackageType.ClassFilter;

    public List<uint> classPackageIds; // m_classPIDs

    public ClassFilter(AC2Reader data) {
        data.ReadPkg<AList>(v => classPackageIds = v);
    }
}
