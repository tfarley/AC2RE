namespace AC2RE.Definitions;

public class DirectiveReorganizeContentsSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveReorganizeContents;

    // WM_Inventory::SendSEvt_DirectiveReorganizeContents
    public InvMoveDesc moveDesc; // _iDesc

    public DirectiveReorganizeContentsSEvt(AC2Reader data) {
        moveDesc = data.UnpackHeapObject<InvMoveDesc>();
    }
}
