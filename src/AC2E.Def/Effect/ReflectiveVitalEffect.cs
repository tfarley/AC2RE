namespace AC2E.Def {

    public class ReflectiveVitalEffect : Effect {

        public override PackageType packageType => PackageType.ReflectiveVitalEffect;

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
            data.ReadSingletonPkg<Effect>(v => effGenerate = v);
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
