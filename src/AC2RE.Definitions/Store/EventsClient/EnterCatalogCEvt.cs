using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EnterCatalogCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Store__EnterCatalog;

    // WM_Store::PostCEvt_Store_EnterCatalog
    public Dictionary<DataId, uint> view; // _view
    public DataId catalogDid; // _didCatalog
    public InstanceId storekeeperId; // _iidStorekeeper

    public EnterCatalogCEvt() {

    }

    public EnterCatalogCEvt(AC2Reader data) {
        view = data.UnpackHeapObject<AAHash>().to<DataId, uint>();
        catalogDid = data.UnpackDataId();
        storekeeperId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(AAHash.from(view));
        data.Pack(catalogDid);
        data.Pack(storekeeperId);
    }
}
