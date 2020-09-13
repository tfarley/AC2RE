namespace AC2E.Def {

    public class ComboEffect : InstantEffect {

        public override PackageType packageType => PackageType.ComboEffect;

        public SingletonPkg<Effect> effectToGiveBackIfNotPresent; // m_effToGiveBackIfNotPresent
        public SingletonPkg<Effect> effectToGiveBackIfPresent; // m_effToGiveBackIfPresent
        public SingletonPkg<Effect> effectToAddIfPresent; // m_effToAddIfPresent
        public AList effectPresentByClass; // m_listEffectPresentByClass
        public AList effectPresentByType; // m_listEffectPresentByType
        public int spellcraftAdjustmentIfNotPresent; // m_iSpellcraftAdjustmentIfNotPresent
        public int spellcraftAdjustmentIfPresent; // m_iSpellcraftAdjustmentIfPresent
        public SingletonPkg<Effect> effectToAddIfNotPresent; // m_effToAddIfNotPresent

        public ComboEffect(AC2Reader data) : base(data) {
            data.ReadSingletonPkg(v => effectToGiveBackIfNotPresent = v.to<Effect>());
            data.ReadSingletonPkg(v => effectToGiveBackIfPresent = v.to<Effect>());
            data.ReadSingletonPkg(v => effectToAddIfPresent = v.to<Effect>());
            data.ReadPkg<AList>(v => effectPresentByClass = v);
            data.ReadPkg<AList>(v => effectPresentByType = v);
            spellcraftAdjustmentIfNotPresent = data.ReadInt32();
            spellcraftAdjustmentIfPresent = data.ReadInt32();
            data.ReadSingletonPkg(v => effectToAddIfNotPresent = v.to<Effect>());
        }
    }
}
