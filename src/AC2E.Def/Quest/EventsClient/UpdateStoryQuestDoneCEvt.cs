namespace AC2E.Def {

    public class UpdateStoryQuestDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Quest__UpdateStoryQuest_Done;

        // WM_Quest::PostCEvt_UpdateStoryQuest_Done
        public uint _status;
        public bool _bAddScene;
        public uint _sceneID;

        public UpdateStoryQuestDoneCEvt() {

        }

        public UpdateStoryQuestDoneCEvt(AC2Reader data) {
            _status = data.UnpackUInt32();
            _bAddScene = data.UnpackBoolean();
            _sceneID = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_status);
            data.Pack(_bAddScene);
            data.Pack(_sceneID);
        }
    }
}
