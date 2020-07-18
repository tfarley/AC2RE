using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class DragFromMoneyBagDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Money__DragFromMoneyBag_Done;

        // WM_Money::PostCEvt_DragFromMoneyBag_Done
        public InstanceId _iidToContainer;

        public DragFromMoneyBagDoneCEvt() {

        }

        public DragFromMoneyBagDoneCEvt(BinaryReader data) {
            _iidToContainer = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_iidToContainer);
        }
    }
}
