using System.Collections.Generic;

namespace AC2RE.Definitions;

public class UpdateCatalogCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateCatalog;

    // WM_Store::PostCEvt_Store_UpdateCatalog
    public Dictionary<DataId, uint> view; // _view
    public DataId catalogDid; // _didCatalog
    public InstanceId storekeeperId; // _iidStorekeeper

    public UpdateCatalogCEvt() {

    }

    public UpdateCatalogCEvt(AC2Reader data) {
        view = data.UnpackPackage<AAHash>().to<DataId, uint>();
        catalogDid = data.UnpackDataId();
        storekeeperId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(AAHash.from(view));
        data.Pack(catalogDid);
        data.Pack(storekeeperId);
    }
}
