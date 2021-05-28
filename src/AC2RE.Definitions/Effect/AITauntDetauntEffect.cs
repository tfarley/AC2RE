using System;

namespace AC2RE.Definitions {

    public class AITauntDetauntEffect : Effect {

        public override PackageType packageType => PackageType.AITauntDetauntEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            TAUNT_ADJUSTMENT = 1 << 0, // 0x00000001, AITauntDetauntEffect::SetTauntAdjustment
            TAUNT_ADJUSTMENT_AS_FRACTION_OF_MONSTER_HEALTH = 1 << 1, // 0x00000002, AITauntDetauntEffect::SetTauntAdjustmentAsFractionOfMonsterHealth
            // NOTE: AITauntDetauntEffect::SetDetauntAdjustment == 3, possibly a bug
            DETAUNT_ADJUSTMENT_AS_FRACTION_OF_MONSTER_HEALTH = 1 << 2, // 0x00000004, AITauntDetauntEffect::SetDetauntAdjustmentAsFractionOfMonsterHealth
        }

        public int minSpellcraft; // m_nMinSpellcraft
        public int maxSpellcraft; // m_nMaxSpellcraft
        public Flag aiTauntDetauntFlags; // m_flags
        public float minFractionalMod; // m_fMinFractionalMod
        public float maxFractionalMod; // m_fMaxFractionalMod
        public int maxRawMod; // m_nMaxRawMod
        public int minRawMod; // m_nMinRawMod

        public AITauntDetauntEffect(AC2Reader data) : base(data) {
            minSpellcraft = data.ReadInt32();
            maxSpellcraft = data.ReadInt32();
            aiTauntDetauntFlags = (Flag)data.ReadUInt32();
            minFractionalMod = data.ReadSingle();
            maxFractionalMod = data.ReadSingle();
            maxRawMod = data.ReadInt32();
            minRawMod = data.ReadInt32();
        }
    }
}
