namespace AC2E.Def {

    public class DirectiveTakeAllFromContainerSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveTakeAllFromContainer;

        // WM_Inventory::SendSEvt_DirectiveTakeAllFromContainer
        public InvTakeAllDesc _iDesc;

        public DirectiveTakeAllFromContainerSEvt(AC2Reader data) {
            _iDesc = data.UnpackPackage<InvTakeAllDesc>();
        }
    }
}
