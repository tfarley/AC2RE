namespace AC2RE.Definitions;

public class PetAddCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.AI__PetAdd;

    // WM_AI::PostCEvt_AI_PetAdd
    public PetData petData; // _petData
    public DataId iconDid; // _icon
    public StringInfo name; // _name
    public InstanceId petId; // _iidPet

    public PetAddCEvt() {

    }

    public PetAddCEvt(AC2Reader data) {
        petData = data.UnpackPackage<PetData>();
        iconDid = data.UnpackDataId();
        name = data.UnpackPackage<StringInfo>();
        petId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(petData);
        data.Pack(iconDid);
        data.Pack(name);
        data.Pack(petId);
    }
}
