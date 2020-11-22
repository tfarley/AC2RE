using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ComboEffect : Effect {

        public override PackageType packageType => PackageType.ComboEffect;

        public SingletonPkg<Effect> effectToGiveBackIfNotPresent; // m_effToGiveBackIfNotPresent
        public SingletonPkg<Effect> effectToGiveBackIfPresent; // m_effToGiveBackIfPresent
        public SingletonPkg<Effect> effectToAddIfPresent; // m_effToAddIfPresent
        public List<uint> effectPresentByClass; // m_listEffectPresentByClass
        public List<uint> effectPresentByType; // m_listEffectPresentByType
        public int spellcraftAdjustmentIfNotPresent; // m_iSpellcraftAdjustmentIfNotPresent
        public int spellcraftAdjustmentIfPresent; // m_iSpellcraftAdjustmentIfPresent
        public SingletonPkg<Effect> effectToAddIfNotPresent; // m_effToAddIfNotPresent

        public ComboEffect(AC2Reader data) : base(data) {
            data.ReadSingletonPkg<Effect>(v => effectToGiveBackIfNotPresent = v);
            data.ReadSingletonPkg<Effect>(v => effectToGiveBackIfPresent = v);
            data.ReadSingletonPkg<Effect>(v => effectToAddIfPresent = v);
            data.ReadPkg<AList>(v => effectPresentByClass = v);
            data.ReadPkg<AList>(v => effectPresentByType = v);
            spellcraftAdjustmentIfNotPresent = data.ReadInt32();
            spellcraftAdjustmentIfPresent = data.ReadInt32();
            data.ReadSingletonPkg<Effect>(v => effectToAddIfNotPresent = v);
        }
    }
}
