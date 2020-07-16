using System.IO;

namespace AC2E.WLib {

    public class DirectiveMoveItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveMoveItem;

        // WM_Inventory::SendSEvt_DirectiveMoveItem
        public InvMoveDescPkg _iDesc;

        public DirectiveMoveItemSEvt(BinaryReader data) {
            _iDesc = data.UnpackPackage<InvMoveDescPkg>();
        }
    }
}
