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
            _bAddScene = data.UnpackUInt32() != 0;
            _sceneID = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_status);
            data.Pack(_bAddScene ? (uint)1 : (uint)0);
            data.Pack(_sceneID);
        }
    }
}
