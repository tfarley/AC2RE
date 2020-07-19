using System.IO;

namespace AC2E.Def {

    public class RequestRaiseSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Skill__RequestRaiseSkill;

        // WM_Skill::SendSEvt_RequestRaiseSkill
        public uint _skillType;

        public RequestRaiseSkillSEvt(BinaryReader data) {
            _skillType = data.UnpackUInt32();
        }
    }
}
