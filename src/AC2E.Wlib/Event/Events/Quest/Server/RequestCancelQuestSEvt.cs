using System.IO;

namespace AC2E.WLib {

    public class RequestCancelQuestSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Quest__RequestCancelQuest;

        // WM_Quest::SendSEvt_RequestCancelQuest
        public uint _questID;

        public RequestCancelQuestSEvt(BinaryReader data) {
            _questID = data.UnpackUInt32();
        }
    }
}
