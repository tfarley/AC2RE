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

        public SaleProfile(AC2Reader data, PackageRegistry registry) {
            m_didProduct = data.ReadDataId();
            data.ReadPkgRef<IconDesc>(v => m_productIconDesc = v, registry);
            m_iidProduct = data.ReadInstanceId();
            m_productIconDID = data.ReadDataId();
            data.ReadPkgRef<StringInfo>(v => m_siProductName = v, registry);
            m_fCost = data.ReadSingle();
            m_didTrade = data.ReadDataId();
            m_iMaxStackSize = data.ReadInt32();
        }

        public virtual void write(AC2Writer data, PackageRegistry registry) {
            data.Write(m_didProduct);
            data.Write(m_productIconDesc, registry);
            data.Write(m_iidProduct);
            data.Write(m_productIconDID);
            data.Write(m_siProductName, registry);
            data.Write(m_fCost);
            data.Write(m_didTrade);
            data.Write(m_iMaxStackSize);
        }
    }
}
