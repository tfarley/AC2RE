namespace AC2E.Def {

    public class DisplayDeathMessageCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__DisplayDeathMsg;

        // WM_Death::PostCEvt_DeathSystem_DisplayDeathMsg
        public StringInfo _siLastAttacker;

        public DisplayDeathMessageCEvt() {

        }

        public DisplayDeathMessageCEvt(AC2Reader data) {
            _siLastAttacker = data.UnpackPackage<StringInfo>();
        }

        public void write(AC2Writer data) {
            data.Pack(_siLastAttacker);
        }
    }
}
