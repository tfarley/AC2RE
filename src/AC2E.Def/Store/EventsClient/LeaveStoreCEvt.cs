﻿namespace AC2E.Def {

    public class LeaveStoreCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__LeaveStore;

        // WM_Store::PostCEvt_Store_LeaveStore
        public InstanceId _iidStorekeeper;

        public LeaveStoreCEvt() {

        }

        public LeaveStoreCEvt(AC2Reader data) {
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_iidStorekeeper);
        }
    }
}