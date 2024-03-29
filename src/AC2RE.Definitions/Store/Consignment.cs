﻿using System;

namespace AC2RE.Definitions;

public class Consignment : IHeapObject {

    public PackageType packageType => PackageType.Consignment;

    // WLib Consignment
    [Flags]
    public enum Flag : uint {
        None = 0,
        IsExpired = 1 << 0, // IsExpired 0x00000001
        IsRemoved = 1 << 1, // IsRemoved 0x00000002
        IsDirty = 1 << 2, // IsDirty 0x00000004
    }

    public PlayerSaleProfile saleProfile; // m_profile
    public InstanceId ownerId; // m_iidOwner
    public uint saleId; // m_saleID
    public int quantityOffered; // m_quantityOffered
    public int quantitySold; // m_quantitySold
    public double enteredTime; // m_ttTimeEntered
    public Flag flags; // m_uiFlags

    public Consignment() {

    }

    public Consignment(AC2Reader data) {
        data.ReadHO<PlayerSaleProfile>(v => saleProfile = v);
        ownerId = data.ReadInstanceId();
        saleId = data.ReadUInt32();
        quantityOffered = data.ReadInt32();
        quantitySold = data.ReadInt32();
        enteredTime = data.ReadDouble();
        flags = data.ReadEnum<Flag>();
    }

    public void write(AC2Writer data) {
        data.WriteHO(saleProfile);
        data.Write(ownerId);
        data.Write(saleId);
        data.Write(quantityOffered);
        data.Write(quantitySold);
        data.Write(enteredTime);
        data.WriteEnum(flags);
    }
}
