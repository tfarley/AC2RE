using System.Collections.Generic;

namespace AC2E.Def {

    public class EnterConsignmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__EnterConsignment;

        // WM_Store::PostCEvt_Store_EnterConsignment
        public List<Consignment> consignments; // _consignments
        public InstanceId storekeeperId; // _iidStorekeeper

        public EnterConsignmentCEvt() {

        }

        public EnterConsignmentCEvt(AC2Reader data) {
            consignments = data.UnpackPackage<RList>().to<Consignment>();
            storekeeperId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(RList.from(consignments));
            data.Pack(storekeeperId);
        }
    }
}
