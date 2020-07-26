namespace AC2E.Def {

    public class UpdateConsignmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateConsignment;

        // WM_Store::PostCEvt_Store_UpdateConsignment
        public RList<Consignment> consignments; // _consignments
        public InstanceId storekeeperId; // _iidStorekeeper

        public UpdateConsignmentCEvt() {

        }

        public UpdateConsignmentCEvt(AC2Reader data) {
            consignments = data.UnpackPackage<RList<IPackage>>().to<Consignment>();
            storekeeperId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(consignments);
            data.Pack(storekeeperId);
        }
    }
}
