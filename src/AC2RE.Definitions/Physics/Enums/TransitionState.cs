using System;

namespace AC2RE.Definitions {

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
