namespace AC2E.Def {

    public class AcceptVassalSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AcceptVassal;

        // WM_Allegiance::SendSEvt_AcceptVassal
        public InstanceId _vassal;

        public AcceptVassalSEvt(AC2Reader data) {
            _vassal = data.UnpackInstanceId();
        }
    }
}
