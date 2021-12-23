using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class InstantVitalEffect : Effect {

    public override PackageType packageType => PackageType.InstantVitalEffect;

    // WLib InstantVitalEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,
        IsHealth = 1 << 0, // IsHealth 0x00000001
        IsVigor = 1 << 1, // IsVigor 0x00000002
        IsFocus = 1 << 2, // IsFocus 0x00000004
        IsInitialChangeConstant = 1 << 3, // InitialChangeConstant 0x00000008
        IsInitialChangeRandom = 1 << 4, // InitialChangeRandom 0x00000010
        IsInitialChangeVariable = 1 << 5, // InitialChangeVariable 0x00000020
        IsMultiplicative = 1 << 6, // IsMultiplicative 0x00000040

        IsLowBoundConstant = 1 << 8, // SetLowBoundConstant 0x00000100
        IsLowBoundVariable = 1 << 9, // SetLowBoundVariable 0x00000200
        IsHighBoundConstant = 1 << 10, // SetHighBoundConstant 0x00000400
        IsHighBoundVariable = 1 << 11, // SetHighBoundVariable 0x00000800
    }

    public List<FloatScaleDuple> lowBounds; // m_LowBounds
    public SingletonPkg<Effect> hateComboEffect; // m_effHateCombo
    public List<FloatScaleDuple> changeData; // m_changeData
    public float initialChangeVar; // m_fInitialChangeVar
    public List<FloatScaleDuple> highBounds; // m_HighBounds
    public SingletonPkg<Effect> hateLinkerEffect; // m_effHateLinker
    public Flag instantVitalFlags => (Flag)flags;

    public InstantVitalEffect(AC2Reader data) : base(data) {
        data.ReadPkg<RArray>(v => lowBounds = v.to<FloatScaleDuple>());
        data.ReadPkg<Effect>(v => hateComboEffect = v);
        data.ReadPkg<RArray>(v => changeData = v.to<FloatScaleDuple>());
        initialChangeVar = data.ReadSingle();
        data.ReadPkg<RArray>(v => highBounds = v.to<FloatScaleDuple>());
        data.ReadPkg<Effect>(v => hateLinkerEffect = v);
    }
}
