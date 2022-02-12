using System.Collections.Generic;

namespace AC2RE.Definitions;

public class OpenContainerDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__OpenContainer_Done;

    // WM_Inventory::PostCEvt_OpenContainer_Done
    public ErrorType status; // _statusIn
    public List<InstanceId> containerIds; // _containers
    public List<InstanceId> contentIds; // _contents
    public InstanceId containerId; // _containerID

    public OpenContainerDoneCEvt() {

    }

    public OpenContainerDoneCEvt(AC2Reader data) {
        status = data.UnpackEnum<ErrorType>();
        containerIds = data.UnpackHeapObject<LList>().to<InstanceId>();
        contentIds = data.UnpackHeapObject<LList>().to<InstanceId>();
        containerId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.PackEnum(status);
        data.Pack(LList.from(containerIds));
        data.Pack(LList.from(contentIds));
        data.Pack(containerId);
    }
}
