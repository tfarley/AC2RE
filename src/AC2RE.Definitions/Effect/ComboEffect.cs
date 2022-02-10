using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ComboEffect : Effect {

    public override PackageType packageType => PackageType.ComboEffect;

    // WLib ComboEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,
        IsRemoveOldEffectIfPresent = 1 << 0, // SetRemoveOldEffectIfPresent 0x00000001
        IsRemoveAllOldEffectsIfPresent = 1 << 1, // SetRemoveAllOldEffectsIfPresent 0x00000002
        IsIgnoreConsideration = 1 << 2, // IsIgnoreConsideration 0x00000004
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
        data.ReadHO<Effect>(v => effectToGiveBackIfNotPresent = v);
        data.ReadHO<Effect>(v => effectToGiveBackIfPresent = v);
        data.ReadHO<Effect>(v => effectToAddIfPresent = v);
        data.ReadHO<AList>(v => effectPresentByClass = v);
        data.ReadHO<AList>(v => effectPresentByType = v);
        spellcraftAdjustmentIfNotPresent = data.ReadInt32();
        spellcraftAdjustmentIfPresent = data.ReadInt32();
        data.ReadHO<Effect>(v => effectToAddIfNotPresent = v);
    }
}
