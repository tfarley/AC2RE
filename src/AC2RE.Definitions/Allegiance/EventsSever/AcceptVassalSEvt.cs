namespace AC2RE.Definitions {

    public class AcceptVassalSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AcceptVassal;

        // WM_Allegiance::SendSEvt_AcceptVassal
        public InstanceId vassalId; // _vassal

        public AcceptVassalSEvt(AC2Reader data) {
            vassalId = data.UnpackInstanceId();
        }
    }
}
