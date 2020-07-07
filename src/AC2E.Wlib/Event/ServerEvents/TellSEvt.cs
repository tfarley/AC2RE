using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class TellSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Communication__Tell;

        // WM_Communication::SendSEvt_Tell
        public uint _weenieChatFlags;
        public StringInfoPkg _msg;
        public StringInfoPkg _tellee;

        public TellSEvt(BinaryReader data) {
            _weenieChatFlags = data.UnpackUInt32();
            _msg = data.UnpackPackage<StringInfoPkg>();
            _tellee = data.UnpackPackage<StringInfoPkg>();
        }
    }
}
