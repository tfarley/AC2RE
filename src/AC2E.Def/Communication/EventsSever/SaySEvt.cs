using System.IO;

namespace AC2E.Def {

    public class SaySEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Communication__Say;

        // WM_Communication::SendSEvt_Say
        public uint _weenieChatFlags;
        public StringInfo _msg;

        public SaySEvt(BinaryReader data) {
            _weenieChatFlags = data.UnpackUInt32();
            _msg = data.UnpackPackage<StringInfo>();
        }
    }
}
