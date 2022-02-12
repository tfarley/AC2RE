namespace AC2RE.Definitions;

public class StoryQuestTriggerEffect : Effect {

    public override PackageType packageType => PackageType.StoryQuestTriggerEffect;

    public SceneId sceneId; // m_sceneID

    public StoryQuestTriggerEffect(AC2Reader data) : base(data) {
        sceneId = data.ReadEnum<SceneId>();
    }
}
