namespace AC2E.Def {

    public class DirectiveMoveItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveMoveItem;

        // WM_Inventory::SendSEvt_DirectiveMoveItem
        public InvMoveDesc _iDesc;

        public DirectiveMoveItemSEvt(AC2Reader data) {
            _iDesc = data.UnpackPackage<InvMoveDesc>();
        }
    }
}
