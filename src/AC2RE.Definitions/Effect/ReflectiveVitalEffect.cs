using System;

namespace AC2RE.Definitions;

public class ReflectiveVitalEffect : Effect {

    public override PackageType packageType => PackageType.ReflectiveVitalEffect;

    // WLib ReflectiveVitalEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,
        IsConstantAbsorbMagnitude = 1 << 0, // SetConstantAbsorbMagnitude 0x00000001
        IsVariableAbsorbMagnitude = 1 << 1, // SetVariableAbsorbMagnitude 0x00000002
        IsConstantReflectMagnitude = 1 << 2, // SetConstantReflectMagnitude 0x00000004
        IsVariableReflectMagnitude = 1 << 3, // SetVariableReflectMagnitude 0x00000008
        IsReflectToHealth = 1 << 4, // SetReflectToHealth 0x00000010
        IsReflectToVigor = 1 << 5, // SetReflectToVigor 0x00000020
        IsConstantGenerateProbability = 1 << 6, // SetConstantGenerateProbability 0x00000040
        IsVariableGenerateProbability = 1 << 7, // SetVariableGenerateProbability 0x00000080
        IsHealth = 1 << 8, // SetHealth 0x00000100
        IsVigor = 1 << 9, // SetVigor 0x00000200

        IsAdditive = 1 << 12, // SetAdditive 0x00001000
        IsMultiplicative = 1 << 13, // SetMultiplicative 0x00002000
        IsVitalsAdjusting = 1 << 14, // SetVitalsAdjusting 0x00004000
        IsEffectVitalsAdjusting = 1 << 15, // SetEffectVitalsAdjusting 0x00008000
    }

    public float reflectSpellcraftBegin; // m_fReflectSpellcraftBegin
    public float absorbSpellcraftEnd; // m_fAbsorbSpellcraftEnd
    public float reflectMagEnd; // m_fReflectMagEnd
    public float reflectMagBegin; // m_fReflectMagBegin
    public float generateVar; // m_fGenerateVar
    public float absorbProbBegin; // m_fAbsorbProbBegin
    public float reflectSpellcraftEnd; // m_fReflectSpellcraftEnd
    public float absorbMagEnd; // m_fAbsorbMagEnd
    public float reflectVar; // m_fReflectVar
    public float generateSpellcraftEnd; // m_fGenerateSpellcraftEnd
    public SingletonPkg<Effect> effGenerate; // m_effGenerate
    public float reflectPKMod; // m_fReflectPKMod
    public float absorbVar; // m_fAbsorbVar
    public float generateSpellcraftBegin; // m_fGenerateSpellcraftBegin
    public float generateProbBegin; // m_fGenerateProbBegin
    public float reflectProbBegin; // m_fReflectProbBegin
    public float generateProbEnd; // m_fGenerateProbEnd
    public float absorbSpellcraftBegin; // m_fAbsorbSpellcraftBegin
    public float absorbMagBegin; // m_fAbsorbMagBegin
    public Flag reflectiveVitalFlags => (Flag)flags;

    public ReflectiveVitalEffect(AC2Reader data) : base(data) {
        reflectSpellcraftBegin = data.ReadSingle();
        absorbSpellcraftEnd = data.ReadSingle();
        reflectMagEnd = data.ReadSingle();
        reflectMagBegin = data.ReadSingle();
        generateVar = data.ReadSingle();
        absorbProbBegin = data.ReadSingle();
        reflectSpellcraftEnd = data.ReadSingle();
        absorbMagEnd = data.ReadSingle();
        reflectVar = data.ReadSingle();
        generateSpellcraftEnd = data.ReadSingle();
        data.ReadPkg<Effect>(v => effGenerate = v);
        reflectPKMod = data.ReadSingle();
        absorbVar = data.ReadSingle();
        generateSpellcraftBegin = data.ReadSingle();
        generateProbBegin = data.ReadSingle();
        reflectProbBegin = data.ReadSingle();
        generateProbEnd = data.ReadSingle();
        absorbSpellcraftBegin = data.ReadSingle();
        absorbMagBegin = data.ReadSingle();
    }
}
