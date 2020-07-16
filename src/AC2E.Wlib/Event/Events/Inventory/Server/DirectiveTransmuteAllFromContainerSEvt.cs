using System.IO;

namespace AC2E.WLib {

    public class DirectiveTransmuteAllFromContainerSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveTransmuteAllFromContainer;

        // WM_Inventory::SendSEvt_DirectiveTransmuteAllFromContainer
        public InvTransmuteAllDescPkg _iDesc;

        public DirectiveTransmuteAllFromContainerSEvt(BinaryReader data) {
            _iDesc = data.UnpackPackage<InvTransmuteAllDescPkg>();
        }
    }
}
