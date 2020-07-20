﻿namespace AC2E.Def {

    public class DragFromMoneyBagSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Money__DragFromMoneyBag;

        // WM_Money::SendSEvt_DragFromMoneyBag
        public InstanceId _iidToContainer;
        public uint _toSlot;
        public uint _quantity;

        public DragFromMoneyBagSEvt(AC2Reader data) {
            _iidToContainer = data.UnpackInstanceId();
            _toSlot = data.UnpackUInt32();
            _quantity = data.UnpackUInt32();
        }
    }
}
