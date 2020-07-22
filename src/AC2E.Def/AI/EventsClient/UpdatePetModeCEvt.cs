﻿namespace AC2E.Def {

    public class UpdatePetModeCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.AI__UpdatePetMode;

        // WM_AI::PostCEvt_AI_UpdatePetMode
        public uint _mode;
        public InstanceId _iidPet;

        public UpdatePetModeCEvt() {

        }

        public UpdatePetModeCEvt(AC2Reader data) {
            _mode = data.UnpackUInt32();
            _iidPet = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_mode);
            data.Pack(_iidPet);
        }
    }
}