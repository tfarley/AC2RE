namespace AC2RE.Definitions;

public class UpdatePetModeCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.AI__UpdatePetMode;

    // WM_AI::PostCEvt_AI_UpdatePetMode
    public uint mode; // _mode
    public InstanceId petId; // _iidPet

    public UpdatePetModeCEvt() {

    }

    public UpdatePetModeCEvt(AC2Reader data) {
        mode = data.UnpackUInt32();
        petId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(mode);
        data.Pack(petId);
    }
}
