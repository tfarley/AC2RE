using System.IO;

namespace AC2E.WLib {

    public class DirectiveTakeAllFromContainerSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__DirectiveTakeAllFromContainer;

        // WM_Inventory::SendSEvt_DirectiveTakeAllFromContainer
        public InvTakeAllDescPkg _iDesc;

        public DirectiveTakeAllFromContainerSEvt(BinaryReader data) {
            _iDesc = data.UnpackPackage<InvTakeAllDescPkg>();
        }
    }
}
