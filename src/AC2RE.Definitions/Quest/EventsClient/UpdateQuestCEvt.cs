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
        error = (ErrorType)data.UnpackUInt32();
        questInfo = data.UnpackHeapObject<GMQuestInfo>();
        questUpdateType = new(data.UnpackUInt32());
    }

    public void write(AC2Writer data) {
        data.Pack((uint)error);
        data.Pack(questInfo);
        data.Pack(questUpdateType.value);
    }
}
