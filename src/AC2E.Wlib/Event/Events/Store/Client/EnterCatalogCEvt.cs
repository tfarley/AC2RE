﻿using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class EnterCatalogCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__EnterCatalog;

        // WM_Store::PostCEvt_Store_EnterCatalog
        public AAHash _view;
        public DataId _didCatalog;
        public InstanceId _iidStorekeeper;

        public EnterCatalogCEvt() {

        }

        public EnterCatalogCEvt(BinaryReader data) {
            _view = data.UnpackPackage<AAHash>();
            _didCatalog = data.UnpackDataId();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_view);
            data.Pack(_didCatalog);
            data.Pack(_iidStorekeeper);
        }
    }
}
