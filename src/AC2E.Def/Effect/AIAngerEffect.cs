namespace AC2E.Def {

    public class AIAngerEffect : Effect {

        public override PackageType packageType => PackageType.AIAngerEffect;

        public int angerLevel; // m_angerLevel

        public AIAngerEffect(AC2Reader data) : base(data) {
            angerLevel = data.ReadInt32();
        }
    }
}
