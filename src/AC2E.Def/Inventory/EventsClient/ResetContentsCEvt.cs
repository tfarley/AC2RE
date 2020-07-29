namespace AC2E.Def {

    public class ResetContentsCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__Inventory_ResetContents;

        // WM_Inventory::PostCEvt_Inventory_ResetContents
        public RList<ContainerSegmentDescriptor> containerSegments; // _containerSegments
        public InstanceIdList containerIds; // _containers
        public InstanceIdList contentIds; // _contents

        public ResetContentsCEvt() {

        }

        public ResetContentsCEvt(AC2Reader data) {
            containerSegments = data.UnpackPackage<RList<IPackage>>().to<ContainerSegmentDescriptor>();
            containerIds = new InstanceIdList(data.UnpackPackage<LList>());
            contentIds = new InstanceIdList(data.UnpackPackage<LList>());
        }

        public void write(AC2Writer data) {
            data.Pack(containerSegments);
            data.Pack(containerIds);
            data.Pack(contentIds);
        }
    }
}
