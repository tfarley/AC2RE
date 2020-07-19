using System.IO;

namespace AC2E.Def {

    public class DirectiveEquipItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveEquipItem;

        // WM_Inventory::SendSEvt_DirectiveEquipItem
        public InvEquipDesc _eDesc;

        public DirectiveEquipItemSEvt(BinaryReader data) {
            _eDesc = data.UnpackPackage<InvEquipDesc>();
        }
    }
}
