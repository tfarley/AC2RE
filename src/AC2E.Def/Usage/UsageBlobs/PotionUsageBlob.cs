﻿namespace AC2E.Def {

    public class PotionUsageBlob : UsageBlob {

        public override PackageType packageType => PackageType.PotionUsageBlob;

        public DataId itemAprDid; // m_itemAprID
        public DataId itemVisualDescDid; // m_didItemVDescDID
        public AAHash itemAppKeyHash; // m_itemAppKeyHash

        public PotionUsageBlob() : base() {

        }

        public PotionUsageBlob(AC2Reader data) : base(data) {
            itemAprDid = data.ReadDataId();
            itemVisualDescDid = data.ReadDataId();
            data.ReadPkg<AAHash>(v => itemAppKeyHash = v);
        }

        public override void write(AC2Writer data) {
            base.write(data);
            data.Write(itemAprDid);
            data.Write(itemVisualDescDid);
            data.WritePkg(itemAppKeyHash);
        }
    }
}
