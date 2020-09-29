namespace AC2E.Def {

    public class TransactResult : IPackage {

        public PackageType packageType => PackageType.TransactResult;

        public uint playerMoneyAdd; // m_uiPlayerMoneyAdd
        public InstanceIdAHash saleErrors; // m_hashSaleErrors
        public InstanceIdAHash buyErrors; // m_hashBuyErrors
        public uint playerMoneySubtract; // m_uiPlayerMoneySubtract
        public ErrorType errorType; // m_et

        public TransactResult(AC2Reader data) {
            playerMoneyAdd = data.ReadUInt32();
            data.ReadPkg<LAHash>(v => saleErrors = new InstanceIdAHash(v));
            data.ReadPkg<LAHash>(v => buyErrors = new InstanceIdAHash(v));
            playerMoneySubtract = data.ReadUInt32();
            errorType = (ErrorType)data.ReadUInt32();
        }
    }
}
