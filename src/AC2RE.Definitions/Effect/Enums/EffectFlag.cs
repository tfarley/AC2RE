using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum EffectFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        FORCE_CONSTANT_MAGNITUDE = 1 << 20, // 0x00100000, Effect::IsForceConstantMagnitude
        FORCE_VARIABLE_MAGNITUDE = 1 << 21, // 0x00200000, Effect::IsForceVariableMagnitude
    }
}
