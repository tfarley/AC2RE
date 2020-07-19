using System.IO;

namespace AC2E.Def {

    public class OpenContainerDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__OpenContainer_Done;

        // WM_Inventory::PostCEvt_OpenContainer_Done
        public uint _statusIn;
        public InstanceIdList _containers;
        public InstanceIdList _contents;
        public InstanceId _containerID;

        public OpenContainerDoneCEvt() {

        }

        public OpenContainerDoneCEvt(BinaryReader data) {
            _statusIn = data.UnpackUInt32();
            _containers = new InstanceIdList(data.UnpackPackage<LList>());
            _contents = new InstanceIdList(data.UnpackPackage<LList>());
            _containerID = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_statusIn);
            data.Pack(_containers);
            data.Pack(_contents);
            data.Pack(_containerID);
        }
    }
}
