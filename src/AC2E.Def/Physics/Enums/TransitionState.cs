using System;

namespace AC2E.Def {

    // Enum TransitionState
    [Flags]
    public enum TransitionState : uint {
        INVALID,
        OK,
        COLLIDED,
        ADJUSTED,
        SLID,
    }
}
