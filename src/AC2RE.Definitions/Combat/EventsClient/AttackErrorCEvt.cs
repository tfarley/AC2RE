namespace AC2RE.Definitions;

public class AttackErrorCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__AttackError;

    // WM_Combat::PostCEvt_Combat_AttackError
    public SkillId skillId; // _skill
    public InstanceId targetId; // _iidTarget
    public ErrorType error; // _err

    public AttackErrorCEvt() {

    }

    public AttackErrorCEvt(AC2Reader data) {
        skillId = (SkillId)data.UnpackUInt32();
        targetId = data.UnpackInstanceId();
        error = (ErrorType)data.UnpackUInt32();
    }

    public void write(AC2Writer data) {
        data.Pack((uint)skillId);
        data.Pack(targetId);
        data.Pack((uint)error);
    }
}
