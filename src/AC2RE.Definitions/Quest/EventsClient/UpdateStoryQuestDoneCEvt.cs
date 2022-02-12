namespace AC2RE.Definitions;

public class UpdateStoryQuestDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Quest__UpdateStoryQuest_Done;

    // WM_Quest::PostCEvt_UpdateStoryQuest_Done
    public ErrorType status; // _status
    public bool addScene; // _bAddScene
    public SceneId sceneId; // _sceneID

    public UpdateStoryQuestDoneCEvt() {

    }

    public UpdateStoryQuestDoneCEvt(AC2Reader data) {
        status = data.UnpackEnum<ErrorType>();
        addScene = data.UnpackBoolean();
        sceneId = data.UnpackEnum<SceneId>();
    }

    public void write(AC2Writer data) {
        data.PackEnum(status);
        data.Pack(addScene);
        data.PackEnum(sceneId);
    }
}
