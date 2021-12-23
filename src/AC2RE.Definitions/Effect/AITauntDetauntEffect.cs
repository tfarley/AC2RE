using System;

namespace AC2RE.Definitions;

public class AITauntDetauntEffect : Effect {

    public override PackageType packageType => PackageType.AITauntDetauntEffect;

    // WLib AITauntDetauntEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,
        HasTauntAdjustment = 1 << 0, // SetTauntAdjustment 0x00000001
        HasAdjustmentAsFractionOfMonsterHealth = 1 << 1, // SetTauntAdjustmentAsFractionOfMonsterHealth 0x00000002
        // NOTE: AITauntDetauntEffect::SetDetauntAdjustment == 3, possibly a bug in WLib itself?
        HasDetauntAdjustmentAsFractionOfMonsterHealth = 1 << 2, // SetDetauntAdjustmentAsFractionOfMonsterHealth 0x00000004
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
