using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UpdateConsignmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateConsignment;

        // WM_Store::PostCEvt_Store_UpdateConsignment
        public RList<ConsignmentPkg> _consignments;
        public InstanceId _iidStorekeeper;

        public UpdateConsignmentCEvt() {

        }

        public UpdateConsignmentCEvt(BinaryReader data) {
            _consignments = data.UnpackPackage<RList<IPackage>>().to<ConsignmentPkg>();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_consignments);
            data.Pack(_iidStorekeeper);
        }
    }
}
