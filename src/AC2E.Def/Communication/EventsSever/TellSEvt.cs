using System.IO;

namespace AC2E.Def {

    public class TellSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Communication__Tell;

        // WM_Communication::SendSEvt_Tell
        public uint _weenieChatFlags;
        public StringInfo _msg;
        public StringInfo _tellee;

        public TellSEvt(BinaryReader data) {
            _weenieChatFlags = data.UnpackUInt32();
            _msg = data.UnpackPackage<StringInfo>();
            _tellee = data.UnpackPackage<StringInfo>();
        }
    }
}
