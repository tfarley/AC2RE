namespace AC2E.Def {

    public class SwearAllegianceSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__SwearAllegiance;

        // WM_Allegiance::SendSEvt_SwearAllegiance
        public InstanceId _trg;

        public SwearAllegianceSEvt(AC2Reader data) {
            _trg = data.UnpackInstanceId();
        }
    }
}
