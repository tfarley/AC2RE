namespace AC2E.Def {

    public class SaleProfile : IPackage {

        public virtual PackageType packageType => PackageType.SaleProfile;

        public DataId m_didProduct;
        public IconDesc m_productIconDesc;
        public InstanceId m_iidProduct;
        public DataId m_productIconDID;
        public StringInfo m_siProductName;
        public float m_fCost;
        public DataId m_didTrade;
        public int m_iMaxStackSize;

        public SaleProfile() {

        }

        public SaleProfile(AC2Reader data) {
            m_didProduct = data.ReadDataId();
            data.ReadPkg<IconDesc>(v => m_productIconDesc = v);
            m_iidProduct = data.ReadInstanceId();
            m_productIconDID = data.ReadDataId();
            data.ReadPkg<StringInfo>(v => m_siProductName = v);
            m_fCost = data.ReadSingle();
            m_didTrade = data.ReadDataId();
            m_iMaxStackSize = data.ReadInt32();
        }

        public virtual void write(AC2Writer data) {
            data.Write(m_didProduct);
            data.WritePkg(m_productIconDesc);
            data.Write(m_iidProduct);
            data.Write(m_productIconDID);
            data.WritePkg(m_siProductName);
            data.Write(m_fCost);
            data.Write(m_didTrade);
            data.Write(m_iMaxStackSize);
        }
    }
}
