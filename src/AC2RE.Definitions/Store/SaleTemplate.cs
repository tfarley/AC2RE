using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class SaleTemplate : IHeapObject {

    public PackageType packageType => PackageType.SaleTemplate;

    // WLib SaleTemplate
    [Flags]
    public enum Flag : uint {
        None = 0,
        IsTransitive = 1 << 0, // IsTransitive 0x00000001
    }

    public uint ordinal; // m_uiOrdinal
    public DataId productDid; // m_didProduct
    public Dictionary<QuestId, uint> requiredQuests; // m_requiredQuests
    public float cost; // m_fCost
    public SpeciesType race; // m_race
    public DataId tradeDid; // m_didTrade
    public uint level; // m_uiLevel
    public Flag flags; // m_uiFlags

    public SaleTemplate(AC2Reader data) {
        ordinal = data.ReadUInt32();
        productDid = data.ReadDataId();
        data.ReadHO<AAHash>(v => requiredQuests = v.to<QuestId, uint>());
        cost = data.ReadSingle();
        race = data.ReadEnum<SpeciesType>();
        tradeDid = data.ReadDataId();
        level = data.ReadUInt32();
        flags = data.ReadEnum<Flag>();
    }
}
