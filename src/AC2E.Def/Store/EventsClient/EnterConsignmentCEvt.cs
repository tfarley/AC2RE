namespace AC2E.Def {

    public class EnterConsignmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__EnterConsignment;

        // WM_Store::PostCEvt_Store_EnterConsignment
        public RList<Consignment> _consignments;
        public InstanceId _iidStorekeeper;

        public EnterConsignmentCEvt() {

        }

        public EnterConsignmentCEvt(AC2Reader data) {
            _consignments = data.UnpackPackage<RList<IPackage>>().to<Consignment>();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_consignments);
            data.Pack(_iidStorekeeper);
        }
    }
}
