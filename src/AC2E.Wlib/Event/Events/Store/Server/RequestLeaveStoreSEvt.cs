﻿using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RequestLeaveStoreSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestLeaveStore;

        // WM_Store::SendSEvt_Store_RequestLeaveStore
        public InstanceId _iidStorekeeper;

        public RequestLeaveStoreSEvt(BinaryReader data) {
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
