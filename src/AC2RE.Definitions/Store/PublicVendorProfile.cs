﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class PublicVendorProfile : IHeapObject {

    public PackageType packageType => PackageType.PublicVendorProfile;

    public bool purchases; // m_bPurchases
    public uint maxBuyValue; // m_uiMaxBuyValue
    public float sellMult; // m_fSellMult
    public InstanceId resellId; // m_iidResell
    public List<IHeapObject> invNames; // m_listInvNames
    public bool purchasesMagic; // m_bPurchasesMagic
    public InstanceId vendorId; // m_iidVendor
    public uint minBuyValue; // m_uiMinBuyValue
    public List<InstanceId> invIds; // m_listInvID
    public float buyMult; // m_fBuyMult

    public PublicVendorProfile(AC2Reader data) {
        purchases = data.ReadBoolean();
        maxBuyValue = data.ReadUInt32();
        sellMult = data.ReadSingle();
        resellId = data.ReadInstanceId();
        data.ReadHO<RList>(v => invNames = v);
        purchasesMagic = data.ReadBoolean();
        vendorId = data.ReadInstanceId();
        minBuyValue = data.ReadUInt32();
        data.ReadHO<LList>(v => invIds = v.to<InstanceId>());
        buyMult = data.ReadSingle();
    }
}
