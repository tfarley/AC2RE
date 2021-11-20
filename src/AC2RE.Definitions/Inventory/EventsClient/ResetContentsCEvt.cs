using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ResetContentsCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__ResetContents;

        // WM_Inventory::PostCEvt_Inventory_ResetContents
        public List<ContainerSegmentDescriptor> containerSegments; // _containerSegments
        public List<InstanceId> containerIds; // _containers
        public List<InstanceId> contentIds; // _contents

        public ResetContentsCEvt() {

        }

        public ResetContentsCEvt(AC2Reader data) {
            containerSegments = data.UnpackPackage<RList>().to<ContainerSegmentDescriptor>();
            containerIds = data.UnpackPackage<LList>().to<InstanceId>();
            contentIds = data.UnpackPackage<LList>().to<InstanceId>();
        }

        public void write(AC2Writer data) {
            data.Pack(RList.from(containerSegments));
            data.Pack(LList.from(containerIds));
            data.Pack(LList.from(contentIds));
        }
    }
}
