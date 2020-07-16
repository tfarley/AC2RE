using System.IO;

namespace AC2E.WLib {

    public class DirectiveEquipItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveEquipItem;

        // WM_Inventory::SendSEvt_DirectiveEquipItem
        public InvEquipDescPkg _eDesc;

        public DirectiveEquipItemSEvt(BinaryReader data) {
            _eDesc = data.UnpackPackage<InvEquipDescPkg>();
        }
    }
}
