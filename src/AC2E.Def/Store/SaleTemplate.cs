using System.Collections.Generic;

namespace AC2E.Def {

    public class SaleTemplate : IPackage {

        public PackageType packageType => PackageType.SaleTemplate;

        public uint ordinal; // m_uiOrdinal
        public DataId productDid; // m_didProduct
        public Dictionary<uint, uint> requiredQuests; // m_requiredQuests
        public float cost; // m_fCost
        public SpeciesType race; // m_race
        public DataId tradeDid; // m_didTrade
        public uint level; // m_uiLevel
        public uint flags; // m_uiFlags

        public SaleTemplate(AC2Reader data) {
            ordinal = data.ReadUInt32();
            productDid = data.ReadDataId();
            data.ReadPkg<AAHash>(v => requiredQuests = v);
            cost = data.ReadSingle();
            race = (SpeciesType)data.ReadUInt32();
            tradeDid = data.ReadDataId();
            level = data.ReadUInt32();
            flags = data.ReadUInt32();
        }
    }
}
