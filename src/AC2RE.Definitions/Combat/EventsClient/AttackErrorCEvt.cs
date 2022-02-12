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
        skillId = data.UnpackEnum<SkillId>();
        targetId = data.UnpackInstanceId();
        error = data.UnpackEnum<ErrorType>();
    }

    public void write(AC2Writer data) {
        data.PackEnum(skillId);
        data.Pack(targetId);
        data.PackEnum(error);
    }
}
