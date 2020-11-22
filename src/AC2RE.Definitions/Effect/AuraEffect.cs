using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class AuraEffect : Effect {

        public override PackageType packageType => PackageType.AuraEffect;

        public int maxRangeSqr; // m_fMaxRangeSqr
        public List<SingletonPkg<Effect>> effects; // m_listEffect
        public uint auraFlags; // m_uiAuraFlags

        public AuraEffect(AC2Reader data) : base(data) {
            maxRangeSqr = data.ReadInt32();
            data.ReadPkg<RList>(v => effects = v.to(SingletonPkg<Effect>.cast));
            auraFlags = data.ReadUInt32();
        }
    }
}
