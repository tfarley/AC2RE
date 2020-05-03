using System;

namespace AC2E.Def.Enums {

    // Enum TransitionState
    [Flags]
    public enum TransitionState : uint {
        INVALID = 0,
        OK = 1,
        COLLIDED = 2,
        ADJUSTED = 3,
        SLID = 4,
    }
}
