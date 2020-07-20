namespace AC2E.Def {

    public class CloseContainerSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Inventory__CloseContainer;

        // WM_Inventory::SendSEvt_CloseContainer
        public InstanceId _container_iid;

        public CloseContainerSEvt(AC2Reader data) {
            _container_iid = data.UnpackInstanceId();
        }
    }
}
