﻿using System.Collections.Generic;

namespace AC2E.Def {

    public class EnterCatalogCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__EnterCatalog;

        // WM_Store::PostCEvt_Store_EnterCatalog
        public Dictionary<uint, uint> view; // _view
        public DataId catalogDid; // _didCatalog
        public InstanceId storekeeperId; // _iidStorekeeper

        public EnterCatalogCEvt() {

        }

        public EnterCatalogCEvt(AC2Reader data) {
            view = data.UnpackPackage<AAHash>();
            catalogDid = data.UnpackDataId();
            storekeeperId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(AAHash.from(view));
            data.Pack(catalogDid);
            data.Pack(storekeeperId);
        }
    }
}
