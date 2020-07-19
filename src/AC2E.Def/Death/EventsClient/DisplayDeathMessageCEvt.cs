using System.IO;

namespace AC2E.Def {

    public class DisplayDeathMessageCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__DisplayDeathMsg;

        // WM_Death::PostCEvt_DeathSystem_DisplayDeathMsg
        public StringInfo _siLastAttacker;

        public DisplayDeathMessageCEvt() {

        }

        public DisplayDeathMessageCEvt(BinaryReader data) {
            _siLastAttacker = data.UnpackPackage<StringInfo>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_siLastAttacker);
        }
    }
}
