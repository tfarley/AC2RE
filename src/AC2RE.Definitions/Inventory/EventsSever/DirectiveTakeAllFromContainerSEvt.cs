namespace AC2RE.Definitions {

    public class DirectiveTakeAllFromContainerSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveTakeAllFromContainer;

        // WM_Inventory::SendSEvt_DirectiveTakeAllFromContainer
        public InvTakeAllDesc takeAllDesc; // _iDesc

        public DirectiveTakeAllFromContainerSEvt(AC2Reader data) {
            takeAllDesc = data.UnpackPackage<InvTakeAllDesc>();
        }
    }
}
