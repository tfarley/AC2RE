using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum InstantVitalEffectFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        HEALTH = 1 << 0, // 0x00000001, InstantVitalEffect::IsHealth
        VIGOR = 1 << 1, // 0x00000002, InstantVitalEffect::IsVigor
        FOCUS = 1 << 2, // 0x00000004, InstantVitalEffect::IsFocus
        INITIAL_CHANGE_CONSTANT = 1 << 3, // 0x00000008, InstantVitalEffect::InitialChangeConstant
        INITIAL_CHANGE_RANDOM = 1 << 4, // 0x00000010, InstantVitalEffect::InitialChangeRandom
        INITIAL_CHANGE_VARIABLE = 1 << 5, // 0x00000020, InstantVitalEffect::InitialChangeVariable
        MULTIPLICATIVE = 1 << 6, // 0x00000040, InstantVitalEffect::IsMultiplicative

        LOW_BOUND_CONSTANT = 1 << 8, // 0x00000100, InstantVitalEffect::SetLowBoundConstant
        LOW_BOUND_VARIABLE = 1 << 9, // 0x00000200, InstantVitalEffect::SetLowBoundVariable
        HIGH_BOUND_CONSTANT = 1 << 10, // 0x00000400, InstantVitalEffect::SetHighBoundConstant
        HIGH_BOUND_VARIABLE = 1 << 11, // 0x00000800, InstantVitalEffect::SetHighBoundVariable
    }
}
