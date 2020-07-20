namespace AC2E.Def {

    public class RequestCancelQuestSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Quest__RequestCancelQuest;

        // WM_Quest::SendSEvt_RequestCancelQuest
        public uint _questID;

        public RequestCancelQuestSEvt(AC2Reader data) {
            _questID = data.UnpackUInt32();
        }
    }
}
