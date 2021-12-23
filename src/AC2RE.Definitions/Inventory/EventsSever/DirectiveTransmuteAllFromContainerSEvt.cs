namespace AC2RE.Definitions;

public class DirectiveTransmuteAllFromContainerSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveTransmuteAllFromContainer;

    // WM_Inventory::SendSEvt_DirectiveTransmuteAllFromContainer
    public InvTransmuteAllDesc transmuteDesc; // _iDesc

    public DirectiveTransmuteAllFromContainerSEvt(AC2Reader data) {
        transmuteDesc = data.UnpackPackage<InvTransmuteAllDesc>();
    }
}
