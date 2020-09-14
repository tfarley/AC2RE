namespace AC2E.Def {

    public class StoryQuestTriggerEffect : Effect {

        public override PackageType packageType => PackageType.StoryQuestTriggerEffect;

        public uint sceneId; // m_sceneID

        public StoryQuestTriggerEffect(AC2Reader data) : base(data) {
            sceneId = data.ReadUInt32();
        }
    }
}
