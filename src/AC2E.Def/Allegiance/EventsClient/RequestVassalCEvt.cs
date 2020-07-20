namespace AC2E.Def {

    public class RequestVassalCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__RequestVassal;

        // WM_Allegiance::PostCEvt_RequestVassal
        public StringInfo _vassal_name;
        public InstanceId _vassal;

        public RequestVassalCEvt() {

        }

        public RequestVassalCEvt(AC2Reader data) {
            _vassal_name = data.UnpackPackage<StringInfo>();
            _vassal = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_vassal_name);
            data.Pack(_vassal);
        }
    }
}
