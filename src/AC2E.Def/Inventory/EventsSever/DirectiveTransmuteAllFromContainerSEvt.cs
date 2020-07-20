namespace AC2E.Def {

    public class DirectiveTransmuteAllFromContainerSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveTransmuteAllFromContainer;

        // WM_Inventory::SendSEvt_DirectiveTransmuteAllFromContainer
        public InvTransmuteAllDesc _iDesc;

        public DirectiveTransmuteAllFromContainerSEvt(AC2Reader data) {
            _iDesc = data.UnpackPackage<InvTransmuteAllDesc>();
        }
    }
}
