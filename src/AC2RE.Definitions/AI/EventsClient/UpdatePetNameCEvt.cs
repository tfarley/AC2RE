﻿namespace AC2RE.Definitions;

public class UpdatePetNameCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.AI__UpdatePetName;

    // WM_AI::PostCEvt_AI_UpdatePetName
    public StringInfo petName; // petName
    public InstanceId petId; // _iidPet

    public UpdatePetNameCEvt() {

    }

    public UpdatePetNameCEvt(AC2Reader data) {
        petName = data.UnpackHeapObject<StringInfo>();
        petId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(petName);
        data.Pack(petId);
    }
}
