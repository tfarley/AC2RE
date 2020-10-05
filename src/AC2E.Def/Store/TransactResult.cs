using System.Collections.Generic;

namespace AC2E.Def {

    public class TransactResult : IPackage {

        public PackageType packageType => PackageType.TransactResult;

        public uint playerMoneyAdd; // m_uiPlayerMoneyAdd
        public Dictionary<InstanceId, uint> saleErrors; // m_hashSaleErrors
        public Dictionary<InstanceId, uint> buyErrors; // m_hashBuyErrors
        public uint playerMoneySubtract; // m_uiPlayerMoneySubtract
        public ErrorType errorType; // m_et

        public TransactResult(AC2Reader data) {
            playerMoneyAdd = data.ReadUInt32();
            data.ReadPkg<LAHash>(v => saleErrors = v.to<InstanceId, uint>());
            data.ReadPkg<LAHash>(v => buyErrors = v.to<InstanceId, uint>());
            playerMoneySubtract = data.ReadUInt32();
            errorType = (ErrorType)data.ReadUInt32();
        }
    }
}
