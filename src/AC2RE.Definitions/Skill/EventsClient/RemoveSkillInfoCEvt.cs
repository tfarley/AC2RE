namespace AC2RE.Definitions;

public class RemoveSkillInfoCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__RemoveInfo;

    // WM_Skill::PostCEvt_Skill_RemoveInfo
    public SkillId skillId; // _skillType

    public RemoveSkillInfoCEvt() {

    }

    public RemoveSkillInfoCEvt(AC2Reader data) {
        skillId = (SkillId)data.UnpackUInt32();
    }

    public void write(AC2Writer data) {
        data.Pack((uint)skillId);
    }
}
