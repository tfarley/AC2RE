namespace AC2RE.Definitions;

public class UpdateSkillInfoCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__UpdateInfo;

    // WM_Skill::PostCEvt_Skill_UpdateInfo
    public SkillInfo skillInfo; // _info

    public UpdateSkillInfoCEvt() {

    }

    public UpdateSkillInfoCEvt(AC2Reader data) {
        skillInfo = data.UnpackPackage<SkillInfo>();
    }

    public void write(AC2Writer data) {
        data.Pack(skillInfo);
    }
}
