namespace AC2RE.Definitions;

public class UpdateQuestCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Quest__UpdateQuest_Done;

    // WM_Quest::PostCEvt_UpdateQuest_Done
    public ErrorType error; // _status
    public GMQuestInfo questInfo; // _qInfo
    public QuestUpdateType questUpdateType; // _qut

    public UpdateQuestCEvt() {

    }

    public UpdateQuestCEvt(AC2Reader data) {
        error = data.UnpackEnum<ErrorType>();
        questInfo = data.UnpackHeapObject<GMQuestInfo>();
        questUpdateType = data.UnpackEnum<QuestUpdateType>();
    }

    public void write(AC2Writer data) {
        data.PackEnum(error);
        data.Pack(questInfo);
        data.PackEnum(questUpdateType);
    }
}
