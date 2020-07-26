namespace AC2E.Def {

    public class PetRemoveCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.AI__PetRemove;

        // WM_AI::PostCEvt_AI_PetRemove
        public InstanceId petId; // _iidPet

        public PetRemoveCEvt() {

        }

        public PetRemoveCEvt(AC2Reader data) {
            petId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(petId);
        }
    }
}
