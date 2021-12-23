namespace AC2RE.Definitions;

public class RequestPetModeSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Player__RequestPetMode;

    // WM_Player::SendSEvt_RequestPetAttack
    public uint mode; // _mode
    public InstanceId petId; // _iidPet

    public RequestPetModeSEvt(AC2Reader data) {
        mode = data.UnpackUInt32();
        petId = data.UnpackInstanceId();
    }
}
