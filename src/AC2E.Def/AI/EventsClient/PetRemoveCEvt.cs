using System.IO;

namespace AC2E.Def {

    public class PetRemoveCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.AI__PetRemove;

        // WM_AI::PostCEvt_AI_PetRemove
        public InstanceId _iidPet;

        public PetRemoveCEvt() {

        }

        public PetRemoveCEvt(BinaryReader data) {
            _iidPet = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_iidPet);
        }
    }
}
