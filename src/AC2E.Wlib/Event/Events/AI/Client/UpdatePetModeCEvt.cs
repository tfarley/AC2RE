using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UpdatePetModeCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.AI__UpdatePetMode;

        // WM_AI::PostCEvt_AI_UpdatePetMode
        public uint _mode;
        public InstanceId _iidPet;

        public UpdatePetModeCEvt() {

        }

        public UpdatePetModeCEvt(BinaryReader data) {
            _mode = data.UnpackUInt32();
            _iidPet = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_mode);
            data.Pack(_iidPet);
        }
    }
}
