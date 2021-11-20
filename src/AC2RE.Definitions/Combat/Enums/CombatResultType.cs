namespace AC2RE.Definitions {

    // Const *_CombatResultType
    public enum CombatResultType : uint {
        Undef = 0, // Undef_CombatResultType

        Failure = 0x40000001, // Failure_CombatResultType

        Miss = 0x40000007, // Miss_CombatResultType
        Hit = 0x40000008, // Hit_CombatResultType
        CriticalHit = 0x40000009, // CriticalHit_CombatResultType

        Evade = 0x4000000B, // Evade_CombatResultType
    }
}
