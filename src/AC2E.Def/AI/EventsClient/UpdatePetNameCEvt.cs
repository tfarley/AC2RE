﻿namespace AC2E.Def {

    public class UpdatePetNameCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.AI__UpdatePetName;

        // WM_AI::PostCEvt_AI_UpdatePetName
        public StringInfo _petName;
        public InstanceId _iidPet;

        public UpdatePetNameCEvt() {

        }

        public UpdatePetNameCEvt(AC2Reader data) {
            _petName = data.UnpackPackage<StringInfo>();
            _iidPet = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_petName);
            data.Pack(_iidPet);
        }
    }
}
