namespace AC2E.Def {

    public class OpenContainerDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__OpenContainer_Done;

        // WM_Inventory::PostCEvt_OpenContainer_Done
        public uint status; // _statusIn
        public InstanceIdList containerIds; // _containers
        public InstanceIdList contentsIds; // _contents
        public InstanceId containerId; // _containerID

        public OpenContainerDoneCEvt() {

        }

        public OpenContainerDoneCEvt(AC2Reader data) {
            status = data.UnpackUInt32();
            containerIds = new InstanceIdList(data.UnpackPackage<LList>());
            contentsIds = new InstanceIdList(data.UnpackPackage<LList>());
            containerId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(status);
            data.Pack(containerIds);
            data.Pack(contentsIds);
            data.Pack(containerId);
        }
    }
}
