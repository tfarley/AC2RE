using System.IO;

namespace AC2E.WLib {

    public class RequestTrainSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Skill__RequestTrainSkill;

        // WM_Skill::SendSEvt_RequestTrainSkill
        public uint _skillType;

        public RequestTrainSkillSEvt(BinaryReader data) {
            _skillType = data.UnpackUInt32();
        }
    }
}
