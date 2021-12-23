namespace AC2RE.Definitions;

public class RequestCancelQuestSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Quest__RequestCancelQuest;

    // WM_Quest::SendSEvt_RequestCancelQuest
    public QuestId questId; // _questID

    public RequestCancelQuestSEvt(AC2Reader data) {
        questId = (QuestId)data.UnpackUInt32();
    }
}
