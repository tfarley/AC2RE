namespace AC2E.Def {

    public class GameEventEffect : InstantEffect {

        public override PackageType packageType => PackageType.GameEventEffect;

        public uint gameEvent; // m_gameEvent
        public bool gameEventState; // m_gameEventState

        public GameEventEffect(AC2Reader data) : base(data) {
            gameEvent = data.ReadUInt32();
            gameEventState = data.ReadBoolean();
        }
    }
}
