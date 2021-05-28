using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class SaleTemplate : IPackage {

        public PackageType packageType => PackageType.SaleTemplate;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            TRANSITIVE = 1 << 0, // 0x00000001, SaleTemplate::IsTransitive
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
            data.ReadPkg<AAHash>(v => requiredQuests = v.to<QuestId, uint>());
            cost = data.ReadSingle();
            race = (SpeciesType)data.ReadUInt32();
            tradeDid = data.ReadDataId();
            level = data.ReadUInt32();
            flags = (Flag)data.ReadUInt32();
        }
    }
}
