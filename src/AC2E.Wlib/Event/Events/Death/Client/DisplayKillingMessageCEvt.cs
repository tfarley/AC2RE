using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class DisplayKillingMessageCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__DisplayKillingMsg;

        // WM_Death::PostCEvt_DeathSystem_DisplayKillingMsg
        public StringInfo _siDefender;

        public DisplayKillingMessageCEvt() {

        }

        public DisplayKillingMessageCEvt(BinaryReader data) {
            _siDefender = data.UnpackPackage<StringInfo>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_siDefender);
        }
    }
}
