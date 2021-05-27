using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum VitalOverTimeEffectFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        HEALTH = 1 << 0, // 0x00000001, VitalOverTimeEffect::IsHealth
        VIGOR = 1 << 1, // 0x00000002, VitalOverTimeEffect::IsVigor
        CHANGE_PER_SECOND_CONSTANT = 1 << 2, // 0x00000004, VitalOverTimeEffect::ChangePerSecondConstant
        CHANGE_PER_SECOND_RANDOM = 1 << 3, // 0x00000008, VitalOverTimeEffect::ChangePerSecondRandom
        CHANGE_PER_SECOND_VARIABLE = 1 << 4, // 0x00000010, VitalOverTimeEffect::ChangePerSecondVariable
        DO_AT_LEAST_ONE_POINT_PER_HEARTBEAT = 1 << 5, // 0x00000020, VitalOverTimeEffect::DoAtLeastOnePointPerHeartbeat
        MULTIPLICATIVE = 1 << 6, // 0x00000040, VitalOverTimeEffect::IsMultiplicative
        APPLY_TO_MAX_VITALS = 1 << 7, // 0x00000080, VitalOverTimeEffect::IsApplyToMaxVitals
        LOW_BOUND_CONSTANT = 1 << 8, // 0x00000100, VitalOverTimeEffect::SetLowBoundConstant
        LOW_BOUND_VARIABLE = 1 << 9, // 0x00000200, VitalOverTimeEffect::SetLowBoundVariable
        HIGH_BOUND_CONSTANT = 1 << 10, // 0x00000400, VitalOverTimeEffect::SetHighBoundConstant
        HIGH_BOUND_VARIABLE = 1 << 11, // 0x00000800, VitalOverTimeEffect::SetHighBoundVariable
        FOCUS = 1 << 12, // 0x00001000, VitalOverTimeEffect::IsFocus
    }
}
