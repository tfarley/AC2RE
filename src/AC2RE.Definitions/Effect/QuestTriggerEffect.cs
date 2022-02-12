namespace AC2RE.Definitions;

public class QuestTriggerEffect : Effect {

    public override PackageType packageType => PackageType.QuestTriggerEffect;

    public QuestUpdateType questUpdateType; // m_questUpdateType
    public QuestId questId; // m_questID

    public QuestTriggerEffect(AC2Reader data) : base(data) {
        questUpdateType = data.ReadEnum<QuestUpdateType>();
        questId = data.ReadEnum<QuestId>();
    }
}
