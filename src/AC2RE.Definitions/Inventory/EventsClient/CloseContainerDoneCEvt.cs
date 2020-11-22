namespace AC2RE.Definitions {

    public class CloseContainerDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__CloseContainer_Done;

        // WM_Inventory::PostCEvt_CloseContainer_Done
        public ErrorType error; // _statusIn
        public InstanceId containerId; // _containerID

        public CloseContainerDoneCEvt() {

        }

        public CloseContainerDoneCEvt(AC2Reader data) {
            error = (ErrorType)data.UnpackUInt32();
            containerId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack((uint)error);
            data.Pack(containerId);
        }
    }
}
