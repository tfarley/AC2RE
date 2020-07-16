using System.IO;

namespace AC2E.WLib {

    public class DirectiveReorganizeContentsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveReorganizeContents;

        // WM_Inventory::SendSEvt_DirectiveReorganizeContents
        public InvMoveDescPkg _iDesc;

        public DirectiveReorganizeContentsSEvt(BinaryReader data) {
            _iDesc = data.UnpackPackage<InvMoveDescPkg>();
        }
    }
}
