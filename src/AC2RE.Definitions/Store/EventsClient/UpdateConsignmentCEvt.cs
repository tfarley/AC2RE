using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class UpdateConsignmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateConsignment;

        // WM_Store::PostCEvt_Store_UpdateConsignment
        public List<Consignment> consignments; // _consignments
        public InstanceId storekeeperId; // _iidStorekeeper

        public UpdateConsignmentCEvt() {

        }

        public UpdateConsignmentCEvt(AC2Reader data) {
            consignments = data.UnpackPackage<RList>().to<Consignment>();
            storekeeperId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(RList.from(consignments));
            data.Pack(storekeeperId);
        }
    }
}
