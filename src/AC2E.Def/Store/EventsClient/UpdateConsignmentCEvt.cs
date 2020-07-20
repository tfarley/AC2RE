namespace AC2E.Def {

    public class UpdateConsignmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateConsignment;

        // WM_Store::PostCEvt_Store_UpdateConsignment
        public RList<Consignment> _consignments;
        public InstanceId _iidStorekeeper;

        public UpdateConsignmentCEvt() {

        }

        public UpdateConsignmentCEvt(AC2Reader data) {
            _consignments = data.UnpackPackage<RList<IPackage>>().to<Consignment>();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_consignments);
            data.Pack(_iidStorekeeper);
        }
    }
}
