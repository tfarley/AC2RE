namespace AC2E.Def {

    public class UpdateStoryQuestDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Quest__UpdateStoryQuest_Done;

        // WM_Quest::PostCEvt_UpdateStoryQuest_Done
        public ErrorType status; // _status
        public bool addScene; // _bAddScene
        public uint sceneId; // _sceneID

        public UpdateStoryQuestDoneCEvt() {

        }

        public UpdateStoryQuestDoneCEvt(AC2Reader data) {
            status = (ErrorType)data.UnpackUInt32();
            addScene = data.UnpackBoolean();
            sceneId = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack((uint)status);
            data.Pack(addScene);
            data.Pack(sceneId);
        }
    }
}
