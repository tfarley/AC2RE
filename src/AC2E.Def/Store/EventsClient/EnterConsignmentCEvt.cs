namespace AC2E.Def {

    public class EnterConsignmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__EnterConsignment;

        // WM_Store::PostCEvt_Store_EnterConsignment
        public RList<Consignment> consignments; // _consignments
        public InstanceId storekeeperId; // _iidStorekeeper

        public EnterConsignmentCEvt() {

        }

        public EnterConsignmentCEvt(AC2Reader data) {
            consignments = data.UnpackPackage<RList<IPackage>>().to<Consignment>();
            storekeeperId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(consignments);
            data.Pack(storekeeperId);
        }
    }
}
