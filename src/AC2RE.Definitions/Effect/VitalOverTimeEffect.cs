using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class VitalOverTimeEffect : Effect {

    public override PackageType packageType => PackageType.VitalOverTimeEffect;

    // WLib VitalOverTimeEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,
        IsHealth = 1 << 0, // IsHealth 0x00000001
        IsVigor = 1 << 1, // IsVigor 0x00000002
        IsChangePerSecondConstant = 1 << 2, // ChangePerSecondConstant 0x00000004
        IsChangePerSecondRandom = 1 << 3, // ChangePerSecondRandom 0x00000008
        IsChangePerSecondVariable = 1 << 4, // ChangePerSecondVariable 0x00000010
        DoAtLeastOnePointPerHeartbeat = 1 << 5, // DoAtLeastOnePointPerHeartbeat 0x00000020
        IsMultiplicative = 1 << 6, // IsMultiplicative 0x00000040
        IsApplyToMaxVitals = 1 << 7, // IsApplyToMaxVitals 0x00000080
        IsLowBoundConstant = 1 << 8, // SetLowBoundConstant 0x00000100
        IsLowBoundVariable = 1 << 9, // SetLowBoundVariable 0x00000200
        IsHighBoundConstant = 1 << 10, // SetHighBoundConstant 0x00000400
        IsHighBoundVariable = 1 << 11, // SetHighBoundVariable 0x00000800
        IsFocus = 1 << 12, // IsFocus 0x00001000
    }

    public float changePerSecVar; // m_fChangePerSecVar
    public List<FloatScaleDuple> lowBounds; // m_LowBounds
    public List<FloatScaleDuple> changeData; // m_changeData
    public List<FloatScaleDuple> highBounds; // m_HighBounds
    public List<uint> tickFx; // m_TickFX
    public Flag vitalOverTimeFlags => (Flag)flags;

    public VitalOverTimeEffect(AC2Reader data) : base(data) {
        changePerSecVar = data.ReadSingle();
        data.ReadHO<RArray>(v => lowBounds = v.to<FloatScaleDuple>());
        data.ReadHO<RArray>(v => changeData = v.to<FloatScaleDuple>());
        data.ReadHO<RArray>(v => highBounds = v.to<FloatScaleDuple>());
        data.ReadHO<AArray>(v => tickFx = v);
    }
}
