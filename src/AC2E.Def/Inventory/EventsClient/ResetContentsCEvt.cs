using System.IO;

namespace AC2E.Def {

    public class ResetContentsCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__Inventory_ResetContents;

        // WM_Inventory::PostCEvt_Inventory_ResetContents
        public RList<ContainerSegmentDescriptor> _containerSegments;
        public InstanceIdList _containers;
        public InstanceIdList _contents;

        public ResetContentsCEvt() {

        }

        public ResetContentsCEvt(BinaryReader data) {
            _containerSegments = data.UnpackPackage<RList<IPackage>>().to<ContainerSegmentDescriptor>();
            _containers = new InstanceIdList(data.UnpackPackage<LList>());
            _contents = new InstanceIdList(data.UnpackPackage<LList>());
        }

        public void write(BinaryWriter data) {
            data.Pack(_containerSegments);
            data.Pack(_containers);
            data.Pack(_contents);
        }
    }
}
