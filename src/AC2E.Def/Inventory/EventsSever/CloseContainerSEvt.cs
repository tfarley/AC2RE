namespace AC2E.Def {

    public class CloseContainerSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__CloseContainer;

        // WM_Inventory::SendSEvt_CloseContainer
        public InstanceId containerId; // _container_iid

        public CloseContainerSEvt(AC2Reader data) {
            containerId = data.UnpackInstanceId();
        }
    }
}
