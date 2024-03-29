﻿namespace AC2RE.Definitions;

public class UpdateStoreCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateStore;

    // WM_Store::PostCEvt_Store_UpdateStore
    public StoreView view; // _view
    public InstanceId storekeeperId; // _iidStorekeeper

    public UpdateStoreCEvt() {

    }

    public UpdateStoreCEvt(AC2Reader data) {
        view = data.UnpackHeapObject<StoreView>();
        storekeeperId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(view);
        data.Pack(storekeeperId);
    }
}
