namespace AC2E.Def {

    public class UpdateQuestCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Quest__UpdateQuest_Done;

        // WM_Quest::PostCEvt_UpdateQuest_Done
        public uint status; // _status
        public GMQuestInfo questInfo; // _qInfo
        public uint questUpdateType; // _qut // TODO: QuestUpdateType

        public UpdateQuestCEvt() {

        }

        public UpdateQuestCEvt(AC2Reader data) {
            status = data.UnpackUInt32();
            questInfo = data.UnpackPackage<GMQuestInfo>();
            questUpdateType = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(status);
            data.Pack(questInfo);
            data.Pack(questUpdateType);
        }
    }
}
