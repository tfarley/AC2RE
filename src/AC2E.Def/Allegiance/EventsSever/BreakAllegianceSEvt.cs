namespace AC2E.Def {

    public class BreakAllegianceSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__BreakAllegiance;

        // WM_Allegiance::SendSEvt_BreakAllegiance
        public InstanceId targetId; // _trg

        public BreakAllegianceSEvt(AC2Reader data) {
            targetId = data.UnpackInstanceId();
        }
    }
}
