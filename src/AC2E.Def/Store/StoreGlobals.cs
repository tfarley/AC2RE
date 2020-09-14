namespace AC2E.Def {

    public class StoreGlobals : IPackage {

        public PackageType packageType => PackageType.StoreGlobals;

        public uint nextSaleId; // NextSaleID_StoreGlobals
        public DataId goldDid; // m_index

        public StoreGlobals(AC2Reader data) {
            nextSaleId = data.ReadUInt32();
            goldDid = data.ReadDataId();
        }
    }
}
