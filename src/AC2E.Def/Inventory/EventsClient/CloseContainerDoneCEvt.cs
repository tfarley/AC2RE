namespace AC2E.Def {

    public class CloseContainerDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__CloseContainer_Done;

        // WM_Inventory::PostCEvt_CloseContainer_Done
        public uint _statusIn;
        public InstanceId _containerID;

        public CloseContainerDoneCEvt() {

        }

        public CloseContainerDoneCEvt(AC2Reader data) {
            _statusIn = data.UnpackUInt32();
            _containerID = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_statusIn);
            data.Pack(_containerID);
        }
    }
}
