using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class CloseContainerDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__CloseContainer_Done;

        // WM_Inventory::PostCEvt_CloseContainer_Done
        public uint _statusIn;
        public InstanceId _containerID;

        public CloseContainerDoneCEvt() {

        }

        public CloseContainerDoneCEvt(BinaryReader data) {
            _statusIn = data.UnpackUInt32();
            _containerID = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_statusIn);
            data.Pack(_containerID);
        }
    }
}
