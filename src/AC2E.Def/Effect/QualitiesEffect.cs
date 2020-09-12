namespace AC2E.Def {

    public class QualitiesEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.QualitiesEffect;

        public float minChange; // m_fMinChange
        public float maxChange; // m_fMaxChange

        public QualitiesEffect(AC2Reader data) : base(data) {
            minChange = data.ReadSingle();
            maxChange = data.ReadSingle();
        }
    }
}
