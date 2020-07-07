using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class DoSayCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__CDoSay;

        // WM_Communication::PostCEvt_CDoSay
        public uint _weenieChatFlags;
        public StringInfoPkg _msg;

        public DoSayCEvt() {

        }

        public DoSayCEvt(BinaryReader data) {
            _weenieChatFlags = data.UnpackUInt32();
            _msg = data.UnpackPackage<StringInfoPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_weenieChatFlags);
            data.Pack(_msg);
        }
    }
}
