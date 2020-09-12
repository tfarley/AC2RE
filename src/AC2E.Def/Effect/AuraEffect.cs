namespace AC2E.Def {

    public class AuraEffect : Effect {

        public override PackageType packageType => PackageType.AuraEffect;

        public int maxRangeSqr; // m_fMaxRangeSqr
        public RList<Effect> effects; // m_listEffect
        public uint auraFlags; // m_uiAuraFlags

        public AuraEffect(AC2Reader data) : base(data) {
            maxRangeSqr = data.ReadInt32();
            data.ReadPkg<RList<IPackage>>(v => effects = v.to<Effect>());
            auraFlags = data.ReadUInt32();
        }
    }
}
