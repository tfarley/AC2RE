using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum VitalTransferEffectFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        TARGET_HEALTH = 1 << 0, // 0x00000001, VitalTransferEffect::IsTargetHealth
        TARGET_VIGOR = 1 << 1, // 0x00000002, VitalTransferEffect::IsTargetVigor
        CASTER_HEALTH = 1 << 2, // 0x00000004, VitalTransferEffect::IsCasterHealth
        CASTER_VIGOR = 1 << 3, // 0x00000008, VitalTransferEffect::IsCasterVigor
        TARGET_CHANGE_CONSTANT = 1 << 4, // 0x00000010, VitalTransferEffect::TargetChangeConstant
        TARGET_CHANGE_RANDOM = 1 << 5, // 0x00000020, VitalTransferEffect::TargetChangeRandom
        TARGET_CHANGE_VARIABLE = 1 << 6, // 0x00000040, VitalTransferEffect::TargetChangeVariable
        TARGET_CHANGE_MULTIPLICATIVE = 1 << 7, // 0x00000080, VitalTransferEffect::IsTargetChangeMultiplicative
        CASTER_CHANGE_CONSTANT = 1 << 8, // 0x00000100, VitalTransferEffect::CasterChangeConstant
        CASTER_CHANGE_RANDOM = 1 << 9, // 0x00000200, VitalTransferEffect::CasterChangeRandom
        CASTER_CHANGE_VARIABLE = 1 << 10, // 0x00000400, VitalTransferEffect::CasterChangeVariable
    }
}
