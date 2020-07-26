namespace AC2E.Def {

    public class CloseContainerDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__CloseContainer_Done;

        // WM_Inventory::PostCEvt_CloseContainer_Done
        public uint status; // _statusIn
        public InstanceId containerId; // _containerID

        public CloseContainerDoneCEvt() {

        }

        public CloseContainerDoneCEvt(AC2Reader data) {
            status = data.UnpackUInt32();
            containerId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(status);
            data.Pack(containerId);
        }
    }
}
