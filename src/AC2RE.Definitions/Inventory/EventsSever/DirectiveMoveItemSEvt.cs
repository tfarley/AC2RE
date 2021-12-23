namespace AC2RE.Definitions;

public class DirectiveMoveItemSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveMoveItem;

    // WM_Inventory::SendSEvt_DirectiveMoveItem
    public InvMoveDesc moveDesc; // _iDesc

    public DirectiveMoveItemSEvt(AC2Reader data) {
        moveDesc = data.UnpackPackage<InvMoveDesc>();
    }
}
