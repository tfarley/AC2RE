﻿namespace AC2E.Def {

    public class TargetLoreFilter : StoreFilter {

        public override PackageType packageType => PackageType.TargetLoreFilter;

        public int minLore; // m_iMinLore
        public int maxLore; // m_iMaxLore

        public TargetLoreFilter(AC2Reader data) : base(data) {
            minLore = data.ReadInt32();
            maxLore = data.ReadInt32();
        }
    }
}
