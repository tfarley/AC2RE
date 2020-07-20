namespace AC2E.Def {

    public class DirectiveUnequipItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveUnEquipItem;

        // WM_Inventory::SendSEvt_DirectiveUnEquipItem
        public InvEquipDesc _eDesc;

        public DirectiveUnequipItemSEvt(AC2Reader data) {
            _eDesc = data.UnpackPackage<InvEquipDesc>();
        }
    }
}
