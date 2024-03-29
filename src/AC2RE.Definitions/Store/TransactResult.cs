﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class TransactResult : IHeapObject {

    public PackageType packageType => PackageType.TransactResult;

    public uint playerMoneyAdd; // m_uiPlayerMoneyAdd
    public Dictionary<InstanceId, uint> saleErrors; // m_hashSaleErrors
    public Dictionary<InstanceId, uint> buyErrors; // m_hashBuyErrors
    public uint playerMoneySubtract; // m_uiPlayerMoneySubtract
    public ErrorType error; // m_et

    public TransactResult(AC2Reader data) {
        playerMoneyAdd = data.ReadUInt32();
        data.ReadHO<LAHash>(v => saleErrors = v.to<InstanceId, uint>());
        data.ReadHO<LAHash>(v => buyErrors = v.to<InstanceId, uint>());
        playerMoneySubtract = data.ReadUInt32();
        error = data.ReadEnum<ErrorType>();
    }
}
