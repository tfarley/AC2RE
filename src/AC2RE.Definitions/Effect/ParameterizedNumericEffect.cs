using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ParameterizedNumericEffect : Effect {

        public override PackageType packageType => PackageType.ParameterizedNumericEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            CONSTANT_MAGNITUDE = 1 << 0, // 0x00000001, ParameterizedNumericEffect::IsConstantMagnitude
            VARIABLE_MAGNITUDE = 1 << 1, // 0x00000002, ParameterizedNumericEffect::IsVariableMagnitude
            ADDITIVE = 1 << 2, // 0x00000004, ParameterizedNumericEffect::IsAdditive
            MULTIPLICATIVE = 1 << 3, // 0x00000008, ParameterizedNumericEffect::IsMultiplicative
            OVERRIDING = 1 << 4, // 0x00000010, ParameterizedNumericEffect::IsOverriding

            CLASS_PRIORITY_IS_MAGNITUDE = 1 << 17, // 0x00020000, ParameterizedNumericEffect::SetClassPriorityIsMagnitude
            CLASS_PRIORITY_IS_INVERSE_OF_MAGNITUDE = 1 << 18, // 0x00040000, ParameterizedNumericEffect::SetClassPriorityIsInverseOfMagnitude
        }

        public float numericVariance; // m_fVariance
        public List<FloatScaleDuple> magData; // m_magData
        public Flag parameterizedNumericFlags => (Flag)flags;

        public ParameterizedNumericEffect(AC2Reader data) : base(data) {
            numericVariance = data.ReadSingle();
            data.ReadPkg<RArray>(v => magData = v.to<FloatScaleDuple>());
        }
    }
}
