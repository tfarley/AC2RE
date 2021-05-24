namespace AC2RE.Definitions {

    // Const *_CombatResultType
    public enum CombatResultType : uint {
        UNDEF = 0,

        FAILURE = 0x40000001,

        MISS = 0x40000007,
        HIT = 0x40000008,
        CRITICAL_HIT = 0x40000009,

        EVADE = 0x4000000B,
    }
}
