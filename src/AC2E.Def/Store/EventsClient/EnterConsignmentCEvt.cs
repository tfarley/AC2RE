using System.IO;

namespace AC2E.Def {

    public class EnterConsignmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__EnterConsignment;

        // WM_Store::PostCEvt_Store_EnterConsignment
        public RList<Consignment> _consignments;
        public InstanceId _iidStorekeeper;

        public EnterConsignmentCEvt() {

        }

        public EnterConsignmentCEvt(BinaryReader data) {
            _consignments = data.UnpackPackage<RList<IPackage>>().to<Consignment>();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_consignments);
            data.Pack(_iidStorekeeper);
        }
    }
}
