namespace AC2RE.Definitions;

public class DirectiveUnequipItemSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveUnEquipItem;

    // WM_Inventory::SendSEvt_DirectiveUnEquipItem
    public InvEquipDesc equipDesc; // _eDesc

    public DirectiveUnequipItemSEvt(AC2Reader data) {
        equipDesc = data.UnpackPackage<InvEquipDesc>();
    }
}
