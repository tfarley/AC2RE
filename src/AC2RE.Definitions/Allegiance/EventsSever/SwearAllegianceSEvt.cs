namespace AC2RE.Definitions {

    public class SwearAllegianceSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__SwearAllegiance;

        // WM_Allegiance::SendSEvt_SwearAllegiance
        public InstanceId targetId; // _trg

        public SwearAllegianceSEvt(AC2Reader data) {
            targetId = data.UnpackInstanceId();
        }
    }
}
