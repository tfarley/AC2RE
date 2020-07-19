using System.IO;

namespace AC2E.Def {

    public class DisplayMessageCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__DisplayMessage;

        // WM_Player::PostCEvt_DisplayMessage
        public bool _topmost;
        public StringInfo _msg;

        public DisplayMessageCEvt() {

        }

        public DisplayMessageCEvt(BinaryReader data) {
            _topmost = data.UnpackUInt32() != 0;
            _msg = data.UnpackPackage<StringInfo>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_topmost ? (uint)1 : (uint)0);
            data.Pack(_msg);
        }
    }
}
