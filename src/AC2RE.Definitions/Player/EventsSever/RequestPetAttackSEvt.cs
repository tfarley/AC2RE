namespace AC2RE.Definitions;

public class RequestPetAttackSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Player__RequestPetAttack;

    // WM_Player::SendSEvt_RequestPetAttack
    public InstanceId targetId; // _targetID

    public RequestPetAttackSEvt(AC2Reader data) {
        targetId = data.UnpackInstanceId();
    }
}
