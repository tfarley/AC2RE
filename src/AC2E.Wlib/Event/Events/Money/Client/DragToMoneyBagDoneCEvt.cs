using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class DragToMoneyBagDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Money__DragToMoneyBag_Done;

        // WM_Money::PostCEvt_DragToMoneyBag_Done
        public InstanceId _iidFromContainer;
        public uint _fromSlot;
        public InstanceId _iidItem;

        public DragToMoneyBagDoneCEvt() {

        }

        public DragToMoneyBagDoneCEvt(BinaryReader data) {
            _iidFromContainer = data.UnpackInstanceId();
            _fromSlot = data.UnpackUInt32();
            _iidItem = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_iidFromContainer);
            data.Pack(_fromSlot);
            data.Pack(_iidItem);
        }
    }
}
