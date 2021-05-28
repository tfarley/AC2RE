using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ComboEffect : Effect {

        public override PackageType packageType => PackageType.ComboEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            REMOVE_OLD_EFFECT_IF_PRESENT = 1 << 1, // 0x00000001, ComboEffect::SetRemoveOldEffectIfPresent
            REMOVE_ALL_OLD_EFFECTS_IF_PRESENT = 1 << 2, // 0x00000002, ComboEffect::SetRemoveAllOldEffectsIfPresent
            IGNORE_CONSIDERATION = 1 << 3, // 0x00000004, ComboEffect::IsIgnoreConsideration
        }

        public SingletonPkg<Effect> effectToGiveBackIfNotPresent; // m_effToGiveBackIfNotPresent
        public SingletonPkg<Effect> effectToGiveBackIfPresent; // m_effToGiveBackIfPresent
        public SingletonPkg<Effect> effectToAddIfPresent; // m_effToAddIfPresent
        public List<uint> effectPresentByClass; // m_listEffectPresentByClass
        public List<uint> effectPresentByType; // m_listEffectPresentByType
        public int spellcraftAdjustmentIfNotPresent; // m_iSpellcraftAdjustmentIfNotPresent
        public int spellcraftAdjustmentIfPresent; // m_iSpellcraftAdjustmentIfPresent
        public SingletonPkg<Effect> effectToAddIfNotPresent; // m_effToAddIfNotPresent
        public Flag comboFlags => (Flag)flags;

        public ComboEffect(AC2Reader data) : base(data) {
            data.ReadPkg<Effect>(v => effectToGiveBackIfNotPresent = v);
            data.ReadPkg<Effect>(v => effectToGiveBackIfPresent = v);
            data.ReadPkg<Effect>(v => effectToAddIfPresent = v);
            data.ReadPkg<AList>(v => effectPresentByClass = v);
            data.ReadPkg<AList>(v => effectPresentByType = v);
            spellcraftAdjustmentIfNotPresent = data.ReadInt32();
            spellcraftAdjustmentIfPresent = data.ReadInt32();
            data.ReadPkg<Effect>(v => effectToAddIfNotPresent = v);
        }
    }
}
