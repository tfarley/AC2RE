using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum ParameterizedNumericEffectFlag : uint {
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
}
