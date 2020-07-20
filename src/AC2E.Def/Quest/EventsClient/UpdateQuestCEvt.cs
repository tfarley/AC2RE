namespace AC2E.Def {

    public class UpdateQuestCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Quest__UpdateQuest_Done;

        // WM_Quest::PostCEvt_UpdateQuest_Done
        public uint _status;
        public GMQuestInfo _qInfo;
        public uint _qut; // TODO: QuestUpdateType

        public UpdateQuestCEvt() {

        }

        public UpdateQuestCEvt(AC2Reader data) {
            _status = data.UnpackUInt32();
            _qInfo = data.UnpackPackage<GMQuestInfo>();
            _qut = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_status);
            data.Pack(_qInfo);
            data.Pack(_qut);
        }
    }
}
