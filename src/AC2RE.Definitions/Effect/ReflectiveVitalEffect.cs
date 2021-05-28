using System;

namespace AC2RE.Definitions {

    public class ReflectiveVitalEffect : Effect {

        public override PackageType packageType => PackageType.ReflectiveVitalEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            CONSTANT_ABSORB_MAGNITUDE = 1 << 0, // 0x00000001, ReflectiveVitalEffect::SetConstantAbsorbMagnitude
            VARIABLE_ABSORB_MAGNITUDE = 1 << 1, // 0x00000002, ReflectiveVitalEffect::SetVariableAbsorbMagnitude
            CONSTANT_REFLECT_MAGNITUDE = 1 << 2, // 0x00000004, ReflectiveVitalEffect::SetConstantReflectMagnitude
            VARIABLE_REFLECT_MAGNITUDE = 1 << 3, // 0x00000008, ReflectiveVitalEffect::SetVariableReflectMagnitude
            REFLECT_TO_HEALTH = 1 << 4, // 0x00000010, ReflectiveVitalEffect::SetReflectToHealth
            REFLECT_TO_VIGOR = 1 << 5, // 0x00000020, ReflectiveVitalEffect::SetReflectToVigor
            CONSTANT_GENERATE_PROBABILITY = 1 << 6, // 0x00000040, ReflectiveVitalEffect::SetConstantGenerateProbability
            VARIABLE_GENERATE_PROBABILITY = 1 << 7, // 0x00000080, ReflectiveVitalEffect::SetVariableGenerateProbability
            HEALTH = 1 << 8, // 0x00000100, ReflectiveVitalEffect::SetHealth
            VIGOR = 1 << 9, // 0x00000200, ReflectiveVitalEffect::SetVigor

            ADDITIVE = 1 << 12, // 0x00001000, ReflectiveVitalEffect::SetAdditive
            MULTIPLICATIVE = 1 << 13, // 0x00002000, ReflectiveVitalEffect::SetMultiplicative
            VITALS_ADJUSTING = 1 << 14, // 0x00004000, ReflectiveVitalEffect::SetVitalsAdjusting
            EFFECT_VITALS_ADJUSTING = 1 << 15, // 0x00008000, ReflectiveVitalEffect::SetEffectVitalsAdjusting
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
}
