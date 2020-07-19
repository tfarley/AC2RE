using System.IO;

namespace AC2E.Def {

    public class RemoveSkillInfoCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__RemoveInfo;

        // WM_Skill::PostCEvt_Skill_RemoveInfo
        public uint _skillType;

        public RemoveSkillInfoCEvt() {

        }

        public RemoveSkillInfoCEvt(BinaryReader data) {
            _skillType = data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(_skillType);
        }
    }
}
