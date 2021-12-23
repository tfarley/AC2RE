namespace AC2RE.Definitions;

public class DirectiveEquipItemSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveEquipItem;

    // WM_Inventory::SendSEvt_DirectiveEquipItem
    public InvEquipDesc equipDesc; // _eDesc

    public DirectiveEquipItemSEvt(AC2Reader data) {
        equipDesc = data.UnpackPackage<InvEquipDesc>();
    }
}
