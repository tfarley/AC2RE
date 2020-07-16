using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class ExecuteCraftDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__ExecuteCraft_Done;

        // WM_Craft::PostCEvt_ExecuteCraft_Done
        public bool _bNotifyUI;
        public uint _err;
        public DataId _didRecipe;

        public ExecuteCraftDoneCEvt() {

        }

        public ExecuteCraftDoneCEvt(BinaryReader data) {
            _bNotifyUI = data.UnpackUInt32() != 0;
            _err = data.UnpackUInt32();
            _didRecipe = data.UnpackDataId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_bNotifyUI ? (uint)1 : (uint)0);
            data.Pack(_err);
            data.Pack(_didRecipe);
        }
    }
}
