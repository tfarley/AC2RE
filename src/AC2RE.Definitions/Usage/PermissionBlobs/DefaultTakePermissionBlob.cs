namespace AC2RE.Definitions;

public class DefaultTakePermissionBlob : IHeapObject {

    public PackageType packageType => PackageType.DefaultTakePermissionBlob;

    public QuestStatus requiredQuestStatus; // m_requiredQuestStatus
    public QuestId requiredQuest; // m_requiredQuest

    public DefaultTakePermissionBlob(AC2Reader data) {
        requiredQuestStatus = data.ReadEnum<QuestStatus>();
        requiredQuest = data.ReadEnum<QuestId>();
    }
}
