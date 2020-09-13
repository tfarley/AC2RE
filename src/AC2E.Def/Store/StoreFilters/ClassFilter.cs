﻿namespace AC2E.Def {

    public class ClassFilter : StoreFilter {

        public override PackageType packageType => PackageType.ClassFilter;

        public AList classPids; // m_classPIDs

        public ClassFilter(AC2Reader data) : base(data) {
            data.ReadPkg<AList>(v => classPids = v);
        }
    }
}
