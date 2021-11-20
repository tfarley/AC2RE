using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ReflectiveEffect : Effect {

        public override PackageType packageType => PackageType.ReflectiveEffect;

        // WLib ReflectiveEffect
        [Flags]
        public new enum Flag : uint {
            None = 0,
            IsConstantAbsorbProbability = 1 << 0, // SetConstantAbsorbProbability 0x00000001
            IsVariableAbsorbProbability = 1 << 1, // SetVariableAbsorbProbability 0x00000002
            IsConstantReflectProbability = 1 << 2, // SetConstantReflectProbability 0x00000004
            IsVariableReflectProbability = 1 << 3, // SetVariableReflectProbability 0x00000008
            IsConstantGenerateProbability = 1 << 4, // SetConstantGenerateProbability 0x00000010
            IsVariableGenerateProbability = 1 << 5, // SetVariableGenerateProbability 0x00000020
        }

        public float reflectProbEnd; // m_fReflectProbEnd
        public List<uint> reflectClasses; // m_clsReflectList
        public float reflectSpellcraftBegin; // m_fReflectSpellcraftBegin
        public float absorbProbEnd; // m_fAbsorbProbEnd
        public float absorbSpellcraftEnd; // m_fAbsorbSpellcraftEnd
        public List<uint> absorbClasses; // m_clsAbsorbList
        public float generateVar; // m_fGenerateVar
        public float absorbProbBegin; // m_fAbsorbProbBegin
        public float reflectSpellcraftEnd; // m_fReflectSpellcraftEnd
        public float reflectVar; // m_fReflectVar
        public List<uint> generateClasses; // m_clsGenerateList
        public float generateSpellcraftEnd; // m_fGenerateSpellcraftEnd
        public SingletonPkg<Effect> generateEffect; // m_effGenerate
        public float absorbVar; // m_fAbsorbVar
        public float generateSpellcraftBegin; // m_fGenerateSpellcraftBegin
        public float generateProbBegin; // m_fGenerateProbBegin
        public float reflectProbBegin; // m_fReflectProbBegin
        public float generateProbEnd; // m_fGenerateProbEnd
        public float absorbSpellcraftBegin; // m_fAbsorbSpellcraftBegin
        public Flag reflectiveFlags => (Flag)flags;

        public ReflectiveEffect(AC2Reader data) : base(data) {
            reflectProbEnd = data.ReadSingle();
            data.ReadPkg<AList>(v => reflectClasses = v);
            reflectSpellcraftBegin = data.ReadSingle();
            absorbProbEnd = data.ReadSingle();
            absorbSpellcraftEnd = data.ReadSingle();
            data.ReadPkg<AList>(v => absorbClasses = v);
            generateVar = data.ReadSingle();
            absorbProbBegin = data.ReadSingle();
            reflectSpellcraftEnd = data.ReadSingle();
            reflectVar = data.ReadSingle();
            data.ReadPkg<AList>(v => generateClasses = v);
            generateSpellcraftEnd = data.ReadSingle();
            data.ReadPkg<Effect>(v => generateEffect = v);
            absorbVar = data.ReadSingle();
            generateSpellcraftBegin = data.ReadSingle();
            generateProbBegin = data.ReadSingle();
            reflectProbBegin = data.ReadSingle();
            generateProbEnd = data.ReadSingle();
            absorbSpellcraftBegin = data.ReadSingle();
        }
    }
}
