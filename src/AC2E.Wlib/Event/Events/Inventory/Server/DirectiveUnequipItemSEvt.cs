using System.IO;

namespace AC2E.WLib {

    public class DirectiveUnequipItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveUnEquipItem;

        // WM_Inventory::SendSEvt_DirectiveUnEquipItem
        public InvEquipDescPkg _eDesc;

        public DirectiveUnequipItemSEvt(BinaryReader data) {
            _eDesc = data.UnpackPackage<InvEquipDescPkg>();
        }
    }
}
