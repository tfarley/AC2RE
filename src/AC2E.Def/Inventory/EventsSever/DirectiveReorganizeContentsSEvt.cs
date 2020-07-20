namespace AC2E.Def {

    public class DirectiveReorganizeContentsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveReorganizeContents;

        // WM_Inventory::SendSEvt_DirectiveReorganizeContents
        public InvMoveDesc _iDesc;

        public DirectiveReorganizeContentsSEvt(AC2Reader data) {
            _iDesc = data.UnpackPackage<InvMoveDesc>();
        }
    }
}
