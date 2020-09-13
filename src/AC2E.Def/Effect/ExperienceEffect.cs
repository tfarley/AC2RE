namespace AC2E.Def {

    public class ExperienceEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.ExperienceEffect;

        public int challengeLevel; // m_chalLvl

        public ExperienceEffect(AC2Reader data) : base(data) {
            challengeLevel = data.ReadInt32();
        }
    }
}
