using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class DoSayCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__CDoSay;

        // WM_Communication::PostCEvt_CDoSay
        public uint _weenieChatFlags;
        public StringInfo _msg;

        public DoSayCEvt() {

        }

        public DoSayCEvt(BinaryReader data) {
            _weenieChatFlags = data.UnpackUInt32();
            _msg = data.UnpackPackage<StringInfo>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_weenieChatFlags);
            data.Pack(_msg);
        }
    }
}
