using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ParameterizedNumericEffect : Effect {

    public override PackageType packageType => PackageType.ParameterizedNumericEffect;

    // WLib ParameterizedNumericEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,
        IsConstantMagnitude = 1 << 0, // IsConstantMagnitude 0x00000001
        IsVariableMagnitude = 1 << 1, // IsVariableMagnitude 0x00000002
        IsAdditive = 1 << 2, // IsAdditive 0x00000004
        IsMultiplicative = 1 << 3, // IsMultiplicative 0x00000008
        IsOverriding = 1 << 4, // IsOverriding 0x00000010

        ClassPriorityIsMagnitude = 1 << 17, // SetClassPriorityIsMagnitude 0x00020000
        ClassPriorityIsInverseOfMagnitude = 1 << 18, // SetClassPriorityIsInverseOfMagnitude 0x00040000
    }

    public float numericVariance; // m_fVariance
    public List<FloatScaleDuple> magData; // m_magData
    public Flag parameterizedNumericFlags => (Flag)flags;

    public ParameterizedNumericEffect(AC2Reader data) : base(data) {
        numericVariance = data.ReadSingle();
        data.ReadHO<RArray>(v => magData = v.to<FloatScaleDuple>());
    }
}
