﻿namespace AC2E.Def {

    public class DragToMoneyBagSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Money__DragToMoneyBag;

        // WM_Money::SendSEvt_DragToMoneyBag
        public InstanceId _iidFromContainer;
        public uint _fromSlot;
        public uint _quantity;
        public InstanceId _iidItem;

        public DragToMoneyBagSEvt(AC2Reader data) {
            _iidFromContainer = data.UnpackInstanceId();
            _fromSlot = data.UnpackUInt32();
            _quantity = data.UnpackUInt32();
            _iidItem = data.UnpackInstanceId();
        }
    }
}